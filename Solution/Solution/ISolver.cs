using Solver.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    /// <summary>
    /// Represents a interface for solver.
    /// </summary>
    public interface ISolver
    {
        void Solve(IAsciiMap asciiMap, Action<Results> solvedResultCallback);
    }
}
