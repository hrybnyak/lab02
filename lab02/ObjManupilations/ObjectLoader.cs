using ObjLoader.Loader.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.ObjManupilations
{
    public class ObjectLoader : IObjectLoader
    {
        public LoadResult Load(string filename)
        {
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            using (var streamReader = new StreamReader(filename))
            {
                return objLoader.Load(streamReader.BaseStream);
            }
        }
    }
}
