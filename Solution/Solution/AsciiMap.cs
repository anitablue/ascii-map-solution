using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solver.Solution;
using Solver.Solution.Enums;

namespace Solution
{
    /// <summary>
    /// Implementaion of <see cref="IAsciiMap"/>.
    /// Implements all necessary information for ascii map model
    /// </summary>
    public class AsciiMap : IAsciiMap
    {
        Node _source = null;
        Node _destination = null;
        Node[,] _asciiMap = null;

        int _width = 0;
        int _height = 0;

        //Indicates the total width of the ascii map. Used by the solver during its initialization.
        public int Width
        {
            get { return _width; }
        }

        //Indicates the total height of the ascii map. Used by the solver during its initialization.
        public int Height
        {
            get { return _height; }
        }

        //Indicates the start point of the ascii map
        public INode Start
        {
            get { return _source; }
        }

        //Indicate the end point of the maze
        public INode Finish
        {
            get { return _destination; }
        }

        public AsciiMap( string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(string.Format("File {0} not found", filePath), "imagePath");
            InitializeAsciiMap( filePath );
        }

        /// <summary>
        /// Gets all acii map nodes.
        /// </summary>
        public IEnumerator<INode> GetNodes()
        {
            for (int i = 0; i < _height; i++)
            for (int j = 0; j < _width; j++)
                yield return _asciiMap[i, j];
        }

        /// <summary>
        /// Function that finds the ascii map path using the solver.
        /// </summary>
        public void Solve(ISolver solver, Action<Results> solvedResultCallback)
        {
            if (solver == null)
                throw new ArgumentNullException("Solver cannot be null", "solver");
            if (solvedResultCallback == null)
                throw new ArgumentNullException("Please provide a callback action", "solvedResultCallback");
            //calls solver's solve method.
            solver.Solve(this, (solvedPath) =>
            {
                if (solvedPath == null)
                    solvedResultCallback(new Results());//return a empty path if the solver could not find the ascii map pah.
                else
                    solvedResultCallback(solvedPath);
            });
        }

        /// <summary>
        /// Scans the file and stores each element as <see cref="INode"/>.
        /// </summary>
        private void InitializeAsciiMap( string filePath )
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(string.Format("{0} not found", filePath));

            string[] lines = File.ReadAllLines(filePath);

            _width = lines[0].Replace(" ", "").Length;
            _height = lines.Length;

            _asciiMap = new Node[_height, _width];
            for (int y = 0; y < _height; y++)
            {
                string line = lines[y].Replace(" ", "");

                for (int x = 0; x < _width; x++)
                {
                    if (line[x] == '#') 
                    {
                        _asciiMap[y, x] = new Node(y, x)
                        {
                            State = NodeState.Blocked, ContentState = NodeContentState.StartChar,
                            VisitedState = NodeVisitedState.Unvisited, Content = String.Empty
                        };
                        continue;
                    }
                    else if (line[x] == '-') 
                    {
                        _asciiMap[y, x] = new Node(y, x)
                        {
                            State = NodeState.Open, ContentState = NodeContentState.HorizontalChar,
                            VisitedState = NodeVisitedState.Unvisited, Content = line[x].ToString()
                        };
                    }
                    else if (line[x] == '|') 
                    {
                        _asciiMap[y, x] = new Node(y, x)
                        {
                            State = NodeState.Open, ContentState = NodeContentState.VerticalChar,
                            VisitedState = NodeVisitedState.Unvisited, Content = line[x].ToString()
                        };
                    }
                    else if (line[x] == '+') 
                    {
                        _asciiMap[y, x] = new Node(y, x)
                        {
                            State = NodeState.Open, ContentState = NodeContentState.PlusChar,
                            VisitedState = NodeVisitedState.Unvisited, Content = "+"
                        };
                    }
                    else if (line[x] == '@') 
                    {
                        _asciiMap[y, x] = new Node(y, x)
                        {
                            State = NodeState.Open, ContentState = NodeContentState.StartChar,
                            VisitedState = NodeVisitedState.Unvisited, Content = line[x].ToString()
                        };

                        if (_source == null)
                        {
                            _source = new Node(y, x)
                            {
                                State = NodeState.Open,
                                ContentState = NodeContentState.StartChar,
                                VisitedState = NodeVisitedState.Unvisited,
                                Content = line[x].ToString()
                            };
                        }
                    }
                    else if (line[x] == 'x') 
                    {
                        _asciiMap[y, x] = new Node(y, x)
                        {
                            State = NodeState.Open, ContentState = NodeContentState.Finished,
                            VisitedState = NodeVisitedState.Unvisited, Content = line[x].ToString()
                        };

                        if (_destination == null)
                        {
                            _destination = new Node(y, x)
                            {
                                State = NodeState.Open,
                                ContentState = NodeContentState.Finished,
                                VisitedState = NodeVisitedState.Unvisited,
                                Content = line[x].ToString()
                            };
                        }
                    }
                    else if (Char.IsLetter((line[x]))) 
                    {
                        _asciiMap[y, x] = new Node(y, x)
                        {
                            State = NodeState.Open, ContentState = NodeContentState.LetterChar,
                            VisitedState = NodeVisitedState.Unvisited, Content = line[x].ToString()
                        };
                    }
                    else
                    {
                        //throw new Exception(string.Format("Exception!"));
                    }

                }
            }

            for (int y = 0; y < _height; ++y)
            {
                for (int x = 0; x < _width; ++x)
                {
                    Console.Write(_asciiMap[y, x].Content.PadLeft(5, ' '));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
