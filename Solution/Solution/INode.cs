using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solver.Solution.Enums;

namespace Solver.Solution
{
    public interface INode
    {
        int RowPosition { get; }
        int ColPosition { get; }
        string Content { get; }
        NodeState State { get; set; }
        NodeContentState ContentState { get; set; }
        NodeVisitedState VisitedState { get; set; }
    }
}
