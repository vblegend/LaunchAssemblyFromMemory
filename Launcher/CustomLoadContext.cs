using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Launcher
{
    internal class CustomLoadContext : AssemblyLoadContext
    {
        private List<String> RuntimeResolvingDirs = new List<string>();
        public List<String> CustomResolvingDirs = new List<string>();

        public CustomLoadContext() : base(true)
        {
            this.InitRuntimeDirs();
            this.ResolvingUnmanagedDll += CustomLoadContext_ResolvingUnmanagedDll;
            this.Resolving += CustomLoadContext_Resolving;
            AssemblyLoadContext.Default.Resolving += CustomLoadContext_Resolving;
        }

        private void InitRuntimeDirs()
        {
            var DotNetRuntimeDirectory = Path.GetDirectoryName(typeof(int).Assembly.Location)!;
            var dotNetRuntimeDirectoryWithoutVersion = Path.GetDirectoryName(DotNetRuntimeDirectory)!;
            var version = DotNetRuntimeDirectory.Substring(dotNetRuntimeDirectoryWithoutVersion.Length + 1);
            var AspNetRuntimeDirectory = Path.Combine(Path.GetDirectoryName(dotNetRuntimeDirectoryWithoutVersion)!, "Microsoft.AspNetCore.App", version);
            var DesktopRuntimeDirectory = Path.Combine(Path.GetDirectoryName(dotNetRuntimeDirectoryWithoutVersion)!, "Microsoft.WindowsDesktop.App", version);
            this.RuntimeResolvingDirs.Add(DotNetRuntimeDirectory);
            this.RuntimeResolvingDirs.Add(AspNetRuntimeDirectory);
            this.RuntimeResolvingDirs.Add(DesktopRuntimeDirectory);
        }


        public Object RunExec(Stream stream, String[] args)
        {
            var _Assembly = this.LoadFromStream(stream);
            if (_Assembly != null && _Assembly.EntryPoint != null)
            {
                return _Assembly.EntryPoint.Invoke(null, new object[] { args })!;
            }
            return null!;
        }


       


        private Assembly? CustomLoadContext_Resolving(AssemblyLoadContext context, AssemblyName assemblyName)
        {
            if (assemblyName.Name == null) return null;
            var myCIintl = assemblyName.CultureInfo; 
            var name = assemblyName.Name.Split(',')[0];
            var dll_name = name + ".dll";
            Console.WriteLine($"Resolving {dll_name}");
            var dirs = new List<string>();
            if (this.CustomResolvingDirs != null)
            {
                dirs.AddRange(this.CustomResolvingDirs);
            }
            dirs.AddRange(this.RuntimeResolvingDirs);
            foreach (var dir in dirs)
            {
                var file = Path.Combine(dir, dll_name);
                if (File.Exists(file))
                {
                    Console.WriteLine($"Loading {file}");
                    using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        return context.LoadFromStream(fs);
                    }
                }
            }
            return null;
        }











        private IntPtr CustomLoadContext_ResolvingUnmanagedDll(Assembly assembly, string dllName)
        {
            Console.WriteLine($"Loading {dllName}");
            var dirs = new List<string>();
            if (this.CustomResolvingDirs != null)
            {
                dirs.AddRange(this.CustomResolvingDirs);
            }
            dirs.AddRange(this.RuntimeResolvingDirs);
            foreach (var dir in dirs)
            {
                var file = Path.Combine(dir, dllName);
                if (File.Exists(file))
                {
                    return this.LoadUnmanagedDllFromPath(dllName);
                }
            }
            return IntPtr.Zero;
        }


    }

}
