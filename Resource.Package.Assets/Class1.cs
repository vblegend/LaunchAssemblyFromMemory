

namespace Resource.Package.Assets
{
    public static class Class1
    {




        public static void Test()
        {


            AssetFileStream.Create("12345.asset", "123");


            var data = File.ReadAllBytes(@"G:\热血传奇\Data\progress.bmp");



            var file = AssetFileStream.Open2("12345.asset", "123");






            for (int i = 0; i < 5; i++)
            {
                file.Write(data);
            }


            var node = file.Read(3);

            Console.WriteLine(node);

            file.Close();




        }



    }
}