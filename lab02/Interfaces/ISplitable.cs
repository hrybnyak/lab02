using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Interfaces
{
    public interface ISplitable<T>
    {
        bool Split(int depth, out T left, out T rigth, out T middle);
    }
}
