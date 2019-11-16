using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Solver.Solution;
using Solver.Solution.Enums;

namespace Solution
{
    public class Solver : ISolver
    {
        /// <summary>
        /// Ascii map width
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Ascii map Height
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Tracking nodes
        /// </summary>
        TrackingNode[,] _trackingNodes = null;

        /// <summary>
        /// Implementatin of the <see cref="ISolver"/>'s Solve() method.
        /// </summary>
        public void Solve(IAsciiMap asciiMap, Action<Results> solvedResultCallback)
        {
            IList<TrackingNode> listOfChars = new List<TrackingNode>();
            List<string> listOfLetters = new List<string>();
            Results results = new Results();

            this.Width = asciiMap.Width;
            this.Height = asciiMap.Height;

            InitializeSolver(asciiMap);
            
            TrackingNode startNode = GetTrackingNode(asciiMap.Start);//Start 
            startNode.Predecessor = null;
            startNode.VisitedState = NodeVisitedState.Visited;
            listOfChars.Add(startNode);
            
            startNode.Direction = GetStartDirection( startNode);
            int count = CountStartDirections(asciiMap.Start);

            if( startNode.Direction.Equals ( Direction.Null ) || count <= 0 || count > 1)// no nodes conected
            {
                
                throw new Exception(string.Format("Ascii map is invalid"));
            }

            TrackingNode currentNode = startNode;
            TrackingNode nextNode = null;

            while (true)
            {
                //start node
                if (currentNode.Content.Equals("@"))
                {
                    nextNode = NextTrackingNode(currentNode, currentNode.Direction);

                    if (nextNode == null)
                    {

                        throw new Exception(string.Format("Ascii map is invalid"));
                    }
                    else
                    {
                        nextNode.Direction = currentNode.Direction;
                        currentNode = nextNode;
                        listOfChars.Add(currentNode);
                    }
                }
               else if (IsGoal(currentNode, asciiMap))
                {
                    results.Path = listOfChars.ToList();
                    results.Letters = listOfLetters.ToList();
                    solvedResultCallback(results);
                    return;
                }
                else
                {
                    if (currentNode.ContentState != NodeContentState.PlusChar)
                    {
                        nextNode = NextTrackingNode(currentNode, currentNode.Direction);
                        if (nextNode != null) {
                            if (!IsValid(nextNode.RowPosition, nextNode.ColPosition) && !IsWall(nextNode))
                            {
                                if (nextNode.VisitedState != NodeVisitedState.Visited)
                                {
                                    nextNode.Direction = currentNode.Direction;
                                    nextNode.Predecessor = currentNode;
                                    currentNode = nextNode;
                                    currentNode.VisitedState = NodeVisitedState.Visited;
                                    listOfChars.Add(currentNode);
                                    if(currentNode.ContentState == NodeContentState.LetterChar)
                                    {
                                        listOfLetters.Add(currentNode.Content);
                                    }
                                }
                                else
                                {
                                    nextNode.Direction = currentNode.Direction;
                                    nextNode.Predecessor = currentNode;
                                    currentNode = nextNode;
                                    currentNode.VisitedState = NodeVisitedState.Visited;
                                    currentNode.VisitedMoreTimes = true;
                                    listOfChars.Add(currentNode);//node is visited two times 
                                }
                            }
                            else
                            {
                                Direction newDirection = GetNextDirection(currentNode);
                                nextNode = NextTrackingNode(currentNode, newDirection);
                                count = CountNextDirections(currentNode);

                                if (newDirection == Direction.Null || nextNode == null || count <= 0 || count > 1)
                                {

                                    throw new Exception(string.Format("Ascii map is invalid"));
                                }

                                nextNode.Direction = newDirection;
                                nextNode.Predecessor = currentNode;
                                currentNode = nextNode;
                                currentNode.VisitedState = NodeVisitedState.Visited;
                                listOfChars.Add(currentNode);

                                if (currentNode.ContentState == NodeContentState.LetterChar)
                                {
                                    listOfLetters.Add(currentNode.Content);
                                }
                            }
                        }
                        else
                        {
                            Direction newDirection = GetNextDirection(currentNode);
                            nextNode = NextTrackingNode(currentNode, newDirection);
                            count = CountNextDirections(currentNode);

                            if (newDirection.Equals( Direction.Null ) || nextNode == null || count <= 0 || count > 1)
                            {

                                throw new Exception(string.Format("Ascii map is invalid"));
                            }

                            nextNode.Direction = newDirection;
                            nextNode.Predecessor = currentNode;
                            currentNode = nextNode;
                            currentNode.VisitedState = NodeVisitedState.Visited;
                            listOfChars.Add(currentNode);
                            if (currentNode.ContentState == NodeContentState.LetterChar)
                            {
                                listOfLetters.Add(currentNode.Content);
                            }
                        }
                    }
                    else
                    {
                        Direction newDirection = GetNextDirection( currentNode );
                        nextNode = NextTrackingNode(currentNode, newDirection);
                        count = CountNextDirections(currentNode);

                        if (newDirection == Direction.Null || nextNode == null || count <= 0 || count > 1)
                        {

                            throw new Exception(string.Format("Ascii map is invalid"));
                        }

                        nextNode.Direction = newDirection;
                        nextNode.Predecessor = currentNode;
                        currentNode = nextNode;
                        currentNode.VisitedState = NodeVisitedState.Visited;
                        listOfChars.Add(currentNode);
                        if (currentNode.ContentState == NodeContentState.LetterChar)
                        {
                            listOfLetters.Add(currentNode.Content);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// InitializeSolver
        /// </summary>
        /// <param name="asciiMap"></param>
        private void InitializeSolver(IAsciiMap asciiMap)
        {
            _trackingNodes = new TrackingNode[asciiMap.Height, asciiMap.Width];
            IEnumerator<INode> asciiNodes = asciiMap.GetNodes();
            while (asciiNodes.MoveNext())
            {
                INode asciiNode = asciiNodes.Current;
                _trackingNodes[asciiNode.RowPosition, asciiNode.ColPosition] = new TrackingNode(asciiNode.RowPosition, asciiNode.ColPosition)
                {
                    State = asciiNode.State,
                    ContentState = asciiNode.ContentState,
                    VisitedState = asciiNode.VisitedState,
                    Content = asciiNode.Content,
                    VisitedMoreTimes = false
                };
            }
        }

        /// <summary>
        /// Conversion function.
        /// </summary>
        private TrackingNode GetTrackingNode(INode node)
        {
            return _trackingNodes[node.RowPosition, node.ColPosition]; 
        }

        /// <summary>
        /// Is node invalid
        /// </summary>
        /// <param name="rowPosition"></param>
        /// <param name="colPosition"></param>
        /// <returns></returns>
        public bool IsValid(int rowPosition, int colPosition)
        {
            return (rowPosition < 0 || rowPosition >= this.Height || colPosition < 0 || colPosition >= this.Width);
        }
        /// <summary>
        /// Is node blocked
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool IsWall(TrackingNode node)
        {
            return node.State == NodeState.Blocked;
        }

        /// <summary>
        /// Get start direction
        /// </summary>
        /// <param name="startNode"></param>
        /// <returns></returns>
        public Direction GetStartDirection(TrackingNode startNode)
        {
            TrackingNode nextNodeLeft, nextNodeRight, nextNodeUp, nextNodeDown = null;
            bool isValid = false;

            nextNodeUp = new TrackingNode(startNode.RowPosition - 1, startNode.ColPosition);
            isValid = IsValid(nextNodeUp.RowPosition, nextNodeUp.ColPosition);

            if (!isValid)
            {
                if (!IsWall(_trackingNodes[nextNodeUp.RowPosition, nextNodeUp.ColPosition]))
                {
                    return Direction.Up;
                }
            }

            nextNodeDown = new TrackingNode(startNode.RowPosition + 1, startNode.ColPosition);
            isValid = IsValid(nextNodeDown.RowPosition, nextNodeDown.ColPosition);
            var node = _trackingNodes[nextNodeDown.RowPosition, nextNodeDown.ColPosition];
            if (!isValid)
            {
                if (!IsWall(_trackingNodes[nextNodeDown.RowPosition, nextNodeDown.ColPosition]))
                {
                    return Direction.Down;
                }
            }

            nextNodeRight = new TrackingNode(startNode.RowPosition, startNode.ColPosition + 1);
            isValid = IsValid(nextNodeRight.RowPosition, nextNodeRight.ColPosition);

            if (!isValid)
            {
                if (!IsWall(_trackingNodes[nextNodeRight.RowPosition, nextNodeRight.ColPosition]))
                {
                    return Direction.Right;
                }
            }

            nextNodeLeft = new TrackingNode(startNode.RowPosition, startNode.ColPosition - 1);
            isValid = IsValid(nextNodeLeft.RowPosition, nextNodeLeft.ColPosition);
            if (!isValid)
            {
                if (!IsWall(_trackingNodes[nextNodeLeft.RowPosition, nextNodeLeft.ColPosition]))
                {
                    return Direction.Left;
                }
            }
            return Direction.Null;
        }

        /// <summary>
        /// Get next direction
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Direction GetNextDirection(TrackingNode node)
        {
            TrackingNode nextNodeLeft, nextNodeRight, nextNodeUp, nextNodeDown = null;
            bool isValid = false;


            if(node.Direction == Direction.Right || node.Direction == Direction.Left)
            {
                nextNodeUp = new TrackingNode(node.RowPosition - 1, node.ColPosition);
                isValid = IsValid(nextNodeUp.RowPosition, nextNodeUp.ColPosition);

                if (!isValid)
                {
                    if (!IsWall(_trackingNodes[nextNodeUp.RowPosition, nextNodeUp.ColPosition])
                        && _trackingNodes[nextNodeUp.RowPosition, nextNodeUp.ColPosition].VisitedState == NodeVisitedState.Unvisited)
                    {
                        return Direction.Up;
                    }
                }

                nextNodeDown = new TrackingNode(node.RowPosition + 1, node.ColPosition);
                isValid = IsValid(nextNodeDown.RowPosition, nextNodeDown.ColPosition);

                if (!isValid)
                {
                    if (!IsWall(_trackingNodes[nextNodeDown.RowPosition, nextNodeDown.ColPosition])
                        && _trackingNodes[nextNodeDown.RowPosition, nextNodeDown.ColPosition].VisitedState == NodeVisitedState.Unvisited)
                    {
                        return Direction.Down;
                    }
                }
            }

            if (node.Direction == Direction.Down || node.Direction == Direction.Up)
            {
                nextNodeRight = new TrackingNode(node.RowPosition, node.ColPosition + 1);
                isValid = IsValid(nextNodeRight.RowPosition, nextNodeRight.ColPosition);

                if (!isValid)
                {
                    if (!IsWall(_trackingNodes[nextNodeRight.RowPosition, nextNodeRight.ColPosition])
                        && _trackingNodes[nextNodeRight.RowPosition, nextNodeRight.ColPosition].VisitedState == NodeVisitedState.Unvisited)
                    { 
                        return Direction.Right;
                    }
                }

                nextNodeLeft = new TrackingNode(node.RowPosition, node.ColPosition - 1);
                isValid = IsValid(nextNodeLeft.RowPosition, nextNodeLeft.ColPosition);

                if (!isValid)
                {
                    if (!IsWall(_trackingNodes[nextNodeLeft.RowPosition, nextNodeLeft.ColPosition])
                        && _trackingNodes[nextNodeLeft.RowPosition, nextNodeLeft.ColPosition].VisitedState == NodeVisitedState.Unvisited)
                    {
                        return Direction.Left;
                    }
                }
            } 
            return Direction.Null;
        }
        /// <summary>
        /// NextTrackingNode
        /// </summary>
        /// <param name="currentTrackingNode"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public TrackingNode NextTrackingNode( TrackingNode currentTrackingNode, Direction direction )
        {
            int rowPosition = currentTrackingNode.RowPosition;
            int colPosition = currentTrackingNode.ColPosition;

            switch (direction)
            {
                case Direction.Left:
                    colPosition--;
                    break;
                case Direction.Right:
                    colPosition++;
                    break;
                case Direction.Down:
                    rowPosition++;
                    break;
                case Direction.Up:
                    rowPosition--;
                    break;
            }
            var valid = IsValid(rowPosition, colPosition);

            return !valid ? _trackingNodes[rowPosition, colPosition] : null;
        }
        /// <summary>
        /// IsGoal - is element finishh node
        /// </summary>
        /// <param name="curNode"></param>
        /// <param name="asciiMap"></param>
        /// <returns></returns>

        public bool IsGoal(TrackingNode curNode, IAsciiMap asciiMap)
        {
            TrackingNode finish = GetTrackingNode(asciiMap.Finish);
            if (curNode == null)
                return false;
            return  curNode.Content.Equals(finish.Content);
        }
        /// <summary>
        /// Count all possible start directions
        /// </summary>
        /// <param name="startNode"></param>
        /// <returns></returns>
        public int CountStartDirections(INode startNode)
        {
            TrackingNode nextNodeLeft, nextNodeRight, nextNodeUp, nextNodeDown = null;
            bool isValid = false;
            int count = 0;

            nextNodeUp = new TrackingNode(startNode.RowPosition - 1, startNode.ColPosition);
            isValid = IsValid(nextNodeUp.RowPosition, nextNodeUp.ColPosition);

            if (!isValid)
            {
                if (!IsWall(_trackingNodes[nextNodeUp.RowPosition, nextNodeUp.ColPosition]))
                {
                    count++;
                }
            }

            nextNodeDown = new TrackingNode(startNode.RowPosition + 1, startNode.ColPosition);
            isValid = IsValid(nextNodeDown.RowPosition, nextNodeDown.ColPosition);
            if (!isValid)
            {
                if (!IsWall(_trackingNodes[nextNodeDown.RowPosition, nextNodeDown.ColPosition]))
                { 
                    count++;
                }
            }

            nextNodeRight = new TrackingNode(startNode.RowPosition, startNode.ColPosition + 1);
            isValid = IsValid(nextNodeRight.RowPosition, nextNodeRight.ColPosition);
            bool notWall = IsWall(_trackingNodes[nextNodeRight.RowPosition, nextNodeRight.ColPosition]);

            if (!isValid)
            {
                if (!IsWall(_trackingNodes[nextNodeRight.RowPosition, nextNodeRight.ColPosition]))
                {
                    count++;
                }
            }
            nextNodeLeft = new TrackingNode(startNode.RowPosition, startNode.ColPosition - 1);
            isValid = IsValid(nextNodeLeft.RowPosition, nextNodeLeft.ColPosition);
            if (!isValid)
            {
                if (!IsWall(_trackingNodes[nextNodeLeft.RowPosition, nextNodeLeft.ColPosition]))
                {
                    count++;
                }
            }
            return count;
        }
        /// <summary>
        /// Count all possible next directions
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int CountNextDirections(TrackingNode node)
        {
            TrackingNode nextNodeLeft, nextNodeRight, nextNodeUp, nextNodeDown = null;
            bool isValid = false;
            int count = 0;


            if (node.Direction == Direction.Right || node.Direction == Direction.Left)
            {
                nextNodeUp = new TrackingNode(node.RowPosition - 1, node.ColPosition);
                isValid = IsValid(nextNodeUp.RowPosition, nextNodeUp.ColPosition);

                if (!isValid)
                {
                    if (!IsWall(_trackingNodes[nextNodeUp.RowPosition, nextNodeUp.ColPosition])
                        && _trackingNodes[nextNodeUp.RowPosition, nextNodeUp.ColPosition].VisitedState == NodeVisitedState.Unvisited)
                    {
                        count++;
                    }
                }

                nextNodeDown = new TrackingNode(node.RowPosition + 1, node.ColPosition);
                isValid = IsValid(nextNodeDown.RowPosition, nextNodeDown.ColPosition);

                if (!isValid)
                {
                    if (!IsWall(_trackingNodes[nextNodeDown.RowPosition, nextNodeDown.ColPosition])
                        && _trackingNodes[nextNodeDown.RowPosition, nextNodeDown.ColPosition].VisitedState == NodeVisitedState.Unvisited)
                    {
                        count++;
                    }
                }
            }

            if (node.Direction == Direction.Down || node.Direction == Direction.Up)
            {
                nextNodeRight = new TrackingNode(node.RowPosition, node.ColPosition + 1);
                isValid = IsValid(nextNodeRight.RowPosition, nextNodeRight.ColPosition);

                if (!isValid)
                {
                    if (!IsWall(_trackingNodes[nextNodeRight.RowPosition, nextNodeRight.ColPosition])
                        && _trackingNodes[nextNodeRight.RowPosition, nextNodeRight.ColPosition].VisitedState == NodeVisitedState.Unvisited)
                    {
                        count++;
                    }
                }

                nextNodeLeft = new TrackingNode(node.RowPosition, node.ColPosition - 1);
                isValid = IsValid(nextNodeLeft.RowPosition, nextNodeLeft.ColPosition);

                if (!isValid)
                {
                    if (!IsWall(_trackingNodes[nextNodeLeft.RowPosition, nextNodeLeft.ColPosition])
                        && _trackingNodes[nextNodeLeft.RowPosition, nextNodeLeft.ColPosition].VisitedState == NodeVisitedState.Unvisited)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}

