using ObjLoader.Loader.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.ObjManupilations
{
    public interface IObjectLoader
    {
        LoadResult Load(string filename);
    }
}
