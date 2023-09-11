using Resource.Package.Assets;
using Resource.Package.Assets.Common;
using System.Diagnostics;

namespace Package.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void opend_Click(object sender, EventArgs e)
        {

        }

        private void opend_Click_1(object sender, EventArgs e)
        {
            var file = AssetFileStream.Open2("hum.asset", "123");
            var node = file.Read(75);
            Console.WriteLine(node);
            File.WriteAllBytes(@"C:\Users\liuya\Desktop\123.bmp", node.Data);
            file.Close();

        }

        private void batch_import_Click(object sender, EventArgs e)
        {
            AssetFileStream.Create("12345.asset", "123", CompressionOption.MustCompressed);
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
            var file = AssetFileStream.Open2("12345.asset", "123");
            file.BatchImport(blocks);
            file.Close();
            sw.Stop();
            MessageBox.Show($"用时{sw.ElapsedMilliseconds}ms");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var file = AssetFileStream.Open2("12345.asset", "123");
            var data = File.ReadAllBytes(@"G:\热血传奇\Data\progress.bmp");
            for (int i = 0; i < 5; i++)
            {
                file.Write(data);
            }
            file.Close();
        }
    }
}