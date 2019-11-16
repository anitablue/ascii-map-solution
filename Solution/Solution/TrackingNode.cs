using Solver.Solution;
using Solver.Solution.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    public class TrackingNode 
    {
        public int RowPosition { get; set; }
        public int ColPosition { get; set; }
        public NodeState State { get; set; }
        public int Distance { get; set; }
        //Pointer to the previous node.
        public TrackingNode Predecessor { get; set; }
        public NodeContentState ContentState { get; set; }
        public NodeVisitedState VisitedState { get; set; }
        public TrackingNode(int row, int col)
        {
                RowPosition = row;
                ColPosition = col;
        }
        public TrackingNode(int row, int col, string content)
        {
            RowPosition = row;
            ColPosition = col;
            Content = content;
        }
        public string Content { get; set; }
        public Direction Direction { get; set; }
        public bool VisitedMoreTimes { get; set; }
    }
}
