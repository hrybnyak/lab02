using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.CommandExecutor
{
    public class ArgumentListParser : IArgumentsListParser
    {
        public const string SourceArgument = "--source=";
        public const string OutputArgument = "--output=";

        public void ExtractArguments(string[] args, out string source, out string output)
        {
            if (args.Count() < 2)
            {
                throw new ArgumentException("Invalid number of arguments");
            }

            var hasSource = args.SingleOrDefault(s => s.Contains(SourceArgument));
            if (hasSource != null)
            {
                source = hasSource.Replace(SourceArgument, string.Empty);
            }
            else
            {
                throw new ArgumentException("There is no source in argument list");
            }

            var hasOutput = args.SingleOrDefault(s => s.Contains(OutputArgument));
            if (hasOutput != null)
            {
                output = hasSource.Replace(OutputArgument, string.Empty);
            }
            else
            {
                throw new ArgumentException("There is no output in argument list");
            }
        }
    }
}
