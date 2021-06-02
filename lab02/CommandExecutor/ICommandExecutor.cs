using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.CommandExecutor
{
    public interface ICommandExecutor
    {
        void ExecuteCommand(string[] args);
    }
}
