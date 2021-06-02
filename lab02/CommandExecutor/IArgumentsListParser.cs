using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.CommandExecutor
{
    public interface IArgumentsListParser
    {
        void ExtractArguments(string[] args, out string source, out string output);
    }
}
