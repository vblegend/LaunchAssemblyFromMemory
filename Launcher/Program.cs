// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Reflection;
using System.Runtime.Loader;

namespace Launcher
{


    public class Program
    {

        public static void Main()
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;
            Assembly assembly = null;
            var filename = @"..\net6.0-windows\TestWinForms.dll";
            //Console.CancelKeyPress += delegate {
            //    // call methods to clean up
            //    Console.WriteLine("exiting");
            //};
        

            while (true)
            {
                var line = Console.ReadLine();
                switch (line)
                {
                    case "exit":
                        {
                            return;
                        }
                    default:
                        {
                            new Thread(() =>
                            {
                                try
                                {
                                    var customLoader = new CustomLoadContext();
                                    customLoader.CustomResolvingDirs = new List<string>()
                                    {
                                        Path.GetDirectoryName(filename)!
                                    };
                                    using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                                    {
                                        assembly = customLoader.LoadFromStream(fs);
                                    }
                                    assembly.EntryPoint!.Invoke(null, new object[] { new string[] { "123456", "qwertyui" } });
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                finally
                                {
                                    Console.WriteLine("Application Exit!");
                                }
                            }).Start();
                            break;
                        }
                }
            }





        }




    }


}