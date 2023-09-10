﻿using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Package.Assets.Secure
{
    internal static class ZLib
    {


        public static Byte[] Compress(Byte[] data)
        {
            using (var ms = new MemoryStream())
            {
                using (Stream s = new ZLibStream(ms, CompressionMode.Compress, true))
                {
                    s.Write(data);
                }
                return ms.ToArray();
            }
        }




        public static Byte[] Decompress(Byte[] data,Int32 rawSize)
        {
            using (var ms = new MemoryStream(data))
            {
                using (ZLibStream s = new ZLibStream(ms, CompressionMode.Decompress, true))
                {
                    var buffer = new byte[rawSize];
                    var len = s.ReadAll(buffer);
                    return buffer;
                }
            }
        }

        public static int ReadAll(this ZLibStream stream, Span<byte> buffer)
        {
            var totalAmountRead = 0;
            while (stream.Read(buffer) is int read and > 0)
            {
                totalAmountRead += read;
                buffer = buffer[read..];
            }
            return totalAmountRead;
        }


    }
}
