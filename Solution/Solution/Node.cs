using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solver.Solution.Enums;

namespace Solver.Solution
{
    public enum NodeState : int
    {
        Open = 0,
        Blocked = 1
    }

    /// <summary>
    /// Implementaion of <see cref="INode"/>.
    /// 
    /// </summary>
    public sealed class Node : INode
    {
        const int DEFAULT_POSITION = 0;
        int _rowPosition, _colPosition = DEFAULT_POSITION;
        NodeState _state = NodeState.Open;
        public NodeContentState _contentState = NodeContentState.StartChar;
        public NodeVisitedState _visitedState = NodeVisitedState.Unvisited;

        string _content;

        public int RowPosition
        {
            get { return _rowPosition; }
        }

        public int ColPosition
        {
            get { return _colPosition; }
        }

        public NodeState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        public NodeContentState ContentState
        {
            get
            {
                return _contentState;
            }
            set
            {
                _contentState = value;
            }
        }
        public NodeVisitedState VisitedState
        {
            get
            {
                return _visitedState;
            }
            set
            {
                _visitedState = value;
            }
        }

        public Node(int row, int col)
        {
            _rowPosition = row;
            _colPosition = col;
        }
        public Node(int row, int col, NodeState nodeState, NodeContentState nodeContenState, NodeVisitedState nodeVisitedState, string content)
        {
            _rowPosition = row;
            _colPosition = col;
            _state = nodeState;
            _contentState = nodeContenState;
            _visitedState = nodeVisitedState;
            _content = content;
        }
    }
}
