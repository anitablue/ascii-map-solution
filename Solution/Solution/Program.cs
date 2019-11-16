using Solver.Solution;
using Solver.Solution.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (args.Length != 1)//input from command line
            //{
            //    Console.WriteLine("Invalid number of arguments");
            //    PrintUsage();
            //    return;
            //}
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map1.txt");
            //Console.WriteLine("Insert file path: ");
            //filePath = Console.ReadLine();
            //string filePath = args[0];

            List<string> list = new List<string>();
            List<TrackingNode> listOfLetters = new List<TrackingNode>();

            try
            {
                IAsciiMap asciiMap = new AsciiMap( filePath );
                ISolver tracingSolver = new Solution.Solver();
                
                asciiMap.Solve(tracingSolver, (solvedPath) => //callback defines action to perform after the asciimap is solved. 
                {
                    var path = String.Join("", solvedPath.Path.Select( t => t.Content).ToArray());
                    var letters = String.Join("", solvedPath.Letters.Select(x => x).ToArray());

                    Console.WriteLine();
                    Console.WriteLine("Completed:");
                    Console.WriteLine("=========");
                    Console.WriteLine("Letters {0}", letters);
                    Console.WriteLine("Path as characters {0}", path);
                    Console.WriteLine("=========");
                });

            }
            catch (Exception e)//generic error handling. Specific errors are handled inside the main code.
            {
                Console.WriteLine();
                Console.WriteLine("ERROR:");
                Console.WriteLine("======");
                Console.WriteLine("{0}", e.Message);
            }
        }
        /// <summary>
        /// Prints the usage of the tool.
        /// </summary>

        static void PrintUsage()
        {
            Console.WriteLine("Usage");
            Console.WriteLine("=====");
            Console.WriteLine("asci map source.[txt]");
            Console.WriteLine();
        }
    }
}
