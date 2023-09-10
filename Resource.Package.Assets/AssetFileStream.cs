using Resource.Package.Assets.Common;
using Resource.Package.Assets.Secure;
using System.IO.Compression;
using System.Text;


namespace Resource.Package.Assets
{
    internal class AssetFileStream
    {
        private static readonly UInt64 MAGIC = 1234567;
        private FileHeader header;
        private Byte[] password;
        private List<FileInfomation> Infomations = new List<FileInfomation>();
        private FileStream fileStream;


        public static void Create(String filename, String password, CompressionOption compressionOption = CompressionOption.NeverCompress)
        {
            var pwd = Encoding.UTF8.GetBytes(password);

            var meta = new FileHeader();
            meta.CompressOption = compressionOption;
            meta.Magic = MAGIC;
            meta.Version = new byte[] { 1, 0, 0 };
            meta.TableDataSize = 0;
            meta.TableDataAddr = 24;
            meta.NumberOfFiles = 0;
            using (var file = File.Open(filename, FileMode.Create))
            {
                using (var writer = new BinaryWriter(file))
                {
                    writer.Write(meta.Magic);
                    writer.Write(meta.Version);
                    writer.Write((Byte)meta.CompressOption);
                    writer.Write(meta.NumberOfFiles);
                    writer.Write(meta.TableDataAddr);
                    writer.Write(meta.TableDataSize);
                    var tab = AES.Encrypt(new Byte[0], pwd);
                    writer.Write(tab);
                    writer.Seek(20, SeekOrigin.Begin);
                    writer.Write(tab.Length);
                }
            };
        }


        public static AssetFileStream Open2(String filename, String password)
        {
            var stream = new AssetFileStream();
            stream.Open(filename, password);
            return stream;
        }



        public void Open(String filename, String password)
        {
            header.Version = new byte[3];
            this.password = Encoding.UTF8.GetBytes(password);
            this.fileStream = File.Open(filename, FileMode.Open);
            using (var reader = new BinaryReader(fileStream, Encoding.UTF8, true))
            {
                header.Magic = reader.ReadUInt64();
                if (header.Magic != MAGIC)
                {
                    throw new Exception("无效的文件格式");
                }
                reader.Read(header.Version);
                header.CompressOption = (CompressionOption)reader.ReadByte();
                header.NumberOfFiles = reader.ReadInt32();
                header.TableDataAddr = reader.ReadInt32();
                header.TableDataSize = reader.ReadInt32();
                this.ReadIndex(reader);
            }
        }



        public DataBlock Read(Int32 Index)
        {

            var info = this.Infomations[Index];
            using (var reader = new BinaryReader(fileStream, Encoding.UTF8, true))
            {
                var node = new DataBlock();
                var data = new Byte[info.lpSize];
                reader.BaseStream.Position = info.lpData;
                reader.Read(data);
                if (info.lpRawSize != info.lpSize)
                {
                    // 解密 data
                    data = ZLib.Decompress(data, info.lpRawSize);
                }
                node.OffsetX = info.OffsetX;
                node.OffsetY = info.OffsetY;
                node.Data = data;
                return node;
            }
        }

        public void Replace(Int32 index, DataBlock data)
        {

        }




        public Int32 Write(DataBlock data)
        {
            var info = new FileInfomation();
            Byte[] outData = data.Data;
            if (header.CompressOption == CompressionOption.MuchPossibleCompress || header.CompressOption == CompressionOption.MustCompressed)
            {
                outData = ZLib.Compress(data.Data);
                if (outData.Length > data.Data.Length && header.CompressOption == CompressionOption.MuchPossibleCompress)
                {
                    outData = data.Data;
                }
            }
            info.OffsetX = data.OffsetX;
            info.OffsetY = data.OffsetY;
            info.lpSize = outData.Length;
            info.lpRawSize = data.Data.Length;
            return this.Write(info,outData);
        }



        private Int32 Write(FileInfomation info, Byte[] data)
        {
            this.Infomations.Add(info);
            info.lpData = header.TableDataAddr;
            header.TableDataAddr = info.lpData + info.lpSize;
            var number = header.NumberOfFiles++;
            using (var writer = new BinaryWriter(fileStream, Encoding.UTF8, true))
            {
                writer.Seek(info.lpData, SeekOrigin.Begin);
                writer.Write(data);
                this.WriteIndex(writer);
                return number;
            }
        }

        public Int32 Write(Byte[] data)
        {
            return this.Write(new DataBlock() { Data = data });
        }

        public void Close()
        {
            fileStream.Close();
            fileStream.Dispose();
        }


        private void ReadIndex(BinaryReader reader)
        {
            Byte[] raw;
            var data = new Byte[header.TableDataSize];
            reader.BaseStream.Position = header.TableDataAddr;
            reader.Read(data);

            try
            {
                raw = AES.Decrypt(data, this.password);
            }
            catch (Exception)
            {
                throw new Exception("无效的密码");
            }
            using (var ms = new MemoryStream(raw))
            {
                this.Infomations = new List<FileInfomation>();
                using (var msReader = new BinaryReader(ms, Encoding.UTF8, true))
                {
                    for (int i = 0; i < header.NumberOfFiles; i++)
                    {
                        var info = new FileInfomation();
                        info.OffsetX = msReader.ReadInt32();
                        info.OffsetY = msReader.ReadInt32();
                        info.lpSize = msReader.ReadInt32();
                        info.lpRawSize = msReader.ReadByte();
                        info.lpData = msReader.ReadInt32();
                        this.Infomations.Add(info);
                    }
                }
            }
        }







        private void WriteIndex(BinaryWriter writer)
        {
            using (var ms = new MemoryStream())
            {
                using (var msWriter = new BinaryWriter(ms, Encoding.UTF8, true))
                {
                    for (int i = 0; i < header.NumberOfFiles; i++)
                    {
                        msWriter.Write(Infomations[i].OffsetX);
                        msWriter.Write(Infomations[i].OffsetY);
                        msWriter.Write(Infomations[i].lpSize);
                        msWriter.Write(Infomations[i].lpRawSize);
                        msWriter.Write(Infomations[i].lpData);
                    }
                }
                var tab = AES.Encrypt(ms.ToArray(), this.password);
                header.TableDataSize = tab.Length;
                writer.Seek(12, SeekOrigin.Begin);
                writer.Write(header.NumberOfFiles);
                writer.Write(header.TableDataAddr);
                writer.Write(header.TableDataSize);
                writer.Seek(header.TableDataAddr, SeekOrigin.Begin);
                writer.Write(tab);
            }
        }










    }
}
