﻿
// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Launcher
{


    public class P
    {
        [DllImport("aasdsdd.dll")]
        public static extern bool SASDD(int frequency, int duration);
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, aaaa");
            Console.WriteLine(JsonConvert.SerializeObject(args));
            Console.WriteLine("Hello, World!");
            //SASDD(1, 2);
        }


    }


}