using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver.Solution.Enums
{
    public enum NodeContentState : int
    {
        StartChar = 0,
        HorizontalChar = 1,
        VerticalChar = 2, 
        PlusChar = 3,
        LetterChar = 4,
        Blocked = 5, 
        Finished = 6
    }
}
