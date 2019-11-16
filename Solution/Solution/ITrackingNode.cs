using Solver.Solution.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver.Solution
{
    public interface ITrackingNode
    {
        int RowPosition { get; }
        int ColPosition { get; }
        string Content { get; }
        NodeState State { get; set; } 
        NodeContentState ContentState { get; set; }
        NodeVisitedState VisitedState { get; set; }
        Direction Direction { get; set; }
        ITrackingNode Predecessor { get; set; }

    }
}
