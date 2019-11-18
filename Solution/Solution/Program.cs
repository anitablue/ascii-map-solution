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
            /*if (args.Length != 1)//input from command line
            {
                Console.WriteLine("Invalid number of arguments");
                PrintUsage();
                return;
            }
            Console.WriteLine("Insert file path: ");
            filePath = Console.ReadLine();
            string filePath = args[0];*/


            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath1 = Path.Combine(baseDir, "Maps", "Map1.txt");
            var filePath2 = Path.Combine(baseDir, "Maps", "Map2.txt");
            var filePath3 = Path.Combine(baseDir, "Maps", "Map3.txt");

            // Folder, where a file is created.  
            // Make sure to change this folder to your own folder  
            string folder = @"D:\";
            // Filename  
            string fileName1 = "Map1Results.txt";
            string fullPath1 = folder + fileName1;
            string fileName2 = "Map2Results.txt";
            string fullPath2 = folder + fileName2;
            string fileName3 = "Map3Results.txt";
            string fullPath3 = folder + fileName3;

            List<OutputFile> listOfFilesPath = new List<OutputFile>();
            listOfFilesPath.Add(new OutputFile (filePath1, folder + fileName1));
            listOfFilesPath.Add(new OutputFile(filePath2, folder + fileName2));
            listOfFilesPath.Add(new OutputFile(filePath3, folder + fileName3));

            foreach (var filePath in listOfFilesPath)
            {
                FindMapPath( filePath.InputFilePath, filePath.OutputFilePath);
            }
        }

        static void FindMapPath( string inputFilePath, string outputFilePath)
        {
            List<string> list = new List<string>();
            List<TrackingNode> listOfLetters = new List<TrackingNode>();
            try
            {
                Console.WriteLine("");
                Console.WriteLine("Read file");
                Console.WriteLine("==============");
                IAsciiMap asciiMap = new AsciiMap(inputFilePath);
                ISolver tracingSolver = new Solution.Solver();

                asciiMap.Solve(tracingSolver, (solvedPath) => //callback defines action to perform after the asciimap is solved. 
                {
                    var path = String.Join("", solvedPath.Path.Select(t => t.Content).ToArray());
                    var letters = String.Join("", solvedPath.Letters.Select(x => x).ToArray());
                    Console.WriteLine("");
                    Console.WriteLine("Completed:");
                    Console.WriteLine("=========");
                    Console.WriteLine("Letters {0}", letters);
                    Console.WriteLine("Path as characters {0}", path);
                    Console.WriteLine("=========");

                    // Write file using StreamWriter  
                    using (StreamWriter writer = new StreamWriter(outputFilePath))
                    {
                        writer.WriteLine("Completed:");
                        writer.WriteLine("=========");
                        writer.WriteLine("Letters {0}", letters);
                        writer.WriteLine("Path as characters {0}", path);
                        writer.WriteLine("=========");
                    }
                    // Read a file  
                    string readText = File.ReadAllText(outputFilePath);
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
