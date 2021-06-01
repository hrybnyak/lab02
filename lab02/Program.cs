using Aspose.ThreeD;
using ObjLoader.Loader.Loaders;
using System;
using System.Drawing;
using System.IO;

namespace lab02
{
    class Program
    {
        static void Main(string[] args)
        {
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            using (var fileStream = new FileStream(@"D:\School\lab02\lab02\Mickey Mouse.obj", FileMode.Open))
            {
                var result = objLoader.Load(fileStream);
            }


        }
    }
}
