using Solver.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    public interface IAsciiMap
    {
        int Height { get; }
        int Width { get; }
        INode Start { get; } 
        INode Finish { get; }
        void Solve(ISolver solver, Action<Results> solvedResultCallback);
        IEnumerator<INode> GetNodes();
    }
}
