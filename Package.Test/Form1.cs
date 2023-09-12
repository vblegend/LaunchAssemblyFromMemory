using Resource.Package.Assets;
using Resource.Package.Assets.Common;
using System.Diagnostics;
using System.Text;

namespace Package.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

 
        private void button2_Click(object sender, EventArgs e)
        {
            var file = AssetFileStream.Open("hum.asset", "123");
            var block = new DataBlock();
            block.OffsetX = 123;
            block.OffsetY = -123;
            block.Data = File.ReadAllBytes(@"E:\Pictures\texture.png");
            file.Replace(7, block);
            file.Close();
        }

        private void opend_Click_2(object sender, EventArgs e)
        {
            var file = AssetFileStream.Open("hum.asset", "123");
            var node = file.Read(7);
            File.WriteAllBytes("123.bmp", node.Data);
            file.Close();
        }



        private void batch_import_Click_1(object sender, EventArgs e)
        {
            var dirname = @"C:\Users\liuya\Desktop\000\";
            List<DataBlock> blocks = new List<DataBlock>();
            var files = Directory.EnumerateFiles(dirname, "*.bmp", SearchOption.AllDirectories);
            foreach (var item in files)
            {
                var block = new DataBlock();
                block.Data = File.ReadAllBytes(item);
                var filename = Path.GetFileNameWithoutExtension(item);
                var pname = Path.Combine(dirname, $"Placements\\{filename}.txt");
                var placements = File.ReadAllLines(pname);
                block.OffsetX = Int32.Parse(placements[0]);
                block.OffsetY = Int32.Parse(placements[1]);
                blocks.Add(block);
            }

            var sw = Stopwatch.StartNew();
            using (var  file = AssetFileStream.Create("12345.asset", "123", CompressionOption.MustCompressed))
            {
                file.BatchImport(blocks);
            }
            sw.Stop();
            MessageBox.Show($"”√ ±{sw.ElapsedMilliseconds}ms");

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var file = AssetFileStream.Open("12345.asset", "123");
            var data = File.ReadAllBytes(@"G:\Data\progress.bmp");
            for (int i = 0; i < 5; i++)
            {
                file.Write(data);
            }
            file.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var file = AssetFileStream.Open("hum.asset", "123");

            file.ChangePassword("");

            file.Close();

        }

    }
}