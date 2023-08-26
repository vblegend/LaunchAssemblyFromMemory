using System.Runtime.CompilerServices;

namespace TestWinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            new SharpDXDemo().Setup();
        }






        private static Int64 Id { get; set; }

        [ModuleInitializer]
        internal static void LazyInitializer()
        {
            Console.WriteLine($"Static Id = {Id}; Id++;");
            Id++;
            Console.WriteLine("Module.Initializer");
        }
    }
}