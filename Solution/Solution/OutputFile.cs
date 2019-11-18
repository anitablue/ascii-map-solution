using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    public class OutputFile
    {
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }
        public OutputFile( string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }
    }
}
