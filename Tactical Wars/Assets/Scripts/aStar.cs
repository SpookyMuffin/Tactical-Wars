using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;


public class aStar : MonoBehaviour
{

    public class Node
    {
        // Change this depending on what the desired size is for each element in the grid
        public static int NODE_SIZE = 1;
        public Node Parent;
        public Vector2 Position;
        public Vector2 Center
        {
            get
            {
                return new Vector2(Position.x + NODE_SIZE / 2, Position.y + NODE_SIZE / 2);
            }
        }
        public float DistanceToTarget;
        public float Cost;
        public float F
        {
            get
            {
                if (DistanceToTarget != -1 && Cost != -1)
                    return DistanceToTarget + Cost;
                else
                    return -1;
            }
        }
        public bool Walkable;

        public Node(Vector2 pos, bool walkable)
        {
            Parent = null;
            Position = pos;
            DistanceToTarget = -1;
            Cost = 1;
            Walkable = walkable;
        }
    }

    public class Astar
    {
        List<List<Node>> Grid;
        int GridRows
        {
            get
            {
                return Grid[0].Count;
                
            }
        }
        int GridCols
        {
            get
            {
                return Grid.Count;
            }
        }

        public Astar(List<List<Node>> grid)
        {
            Grid = grid;
        }

        public Stack<Node> FindPath(Vector2 Start, Vector2 End)
        {
            Node start = new Node(new Vector2((int)(Start.x / Node.NODE_SIZE), (int)(Start.y / Node.NODE_SIZE)), true);
            Node end = new Node(new Vector2((int)(End.x / Node.NODE_SIZE), (int)(End.y / Node.NODE_SIZE)), true);

            Stack<Node> Path = new Stack<Node>();
            List<Node> OpenList = new List<Node>();
            List<Node> ClosedList = new List<Node>();
            List<Node> adjacencies;
            Node current = start;

            // add start node to Open List
            OpenList.Add(start);

            while (OpenList.Count != 0 && !ClosedList.Exists(x => x.Position == end.Position))
            {
                current = OpenList[0];
                OpenList.Remove(current);
                ClosedList.Add(current);
                adjacencies = GetAdjacentNodes(current);


                foreach (Node n in adjacencies)
                {
                    if (!ClosedList.Contains(n) && n.Walkable)
                    {
                        if (!OpenList.Contains(n))
                        {
                            n.Parent = current;
                            n.DistanceToTarget = Math.Abs(n.Position.x - end.Position.x) + Math.Abs(n.Position.y - end.Position.y);
                            n.Cost = 1 + n.Parent.Cost;
                            OpenList.Add(n);
                            OpenList = OpenList.OrderBy(node => node.F).ToList<Node>();
                        }
                    }
                }
            }

            // construct path, if end was not closed return null
            if (!ClosedList.Exists(x => x.Position == end.Position))
            {
                return null;
            }

            // if all good, return path
            Node temp = ClosedList[ClosedList.IndexOf(current)];
            while (temp.Parent != start && temp != null)
            {
                Path.Push(temp);
                temp = temp.Parent;
            }
            return Path;
        }

        private List<Node> GetAdjacentNodes(Node n)
        {
            Debug.Log("busco adyacentes para el nodo" + n.Position.x+","+n.Position.y);
            List<Node> temp = new List<Node>();

            int row = (int)n.Position.y;
            int col = (int)n.Position.x;

            if (row + 1 < GridRows)
            {
                temp.Add(Grid[col][row + 1]);
                Debug.Log("1");
            }
            if (row - 1 >= 0)
            {
                temp.Add(Grid[col][row - 1]);
                Debug.Log("2");
            }
            if (col - 1 >= 0)
            {
                temp.Add(Grid[col - 1][row]);
                Debug.Log("3");
            }
            if (col + 1 < GridCols)
            {
                temp.Add(Grid[col + 1][row]);
                Debug.Log("");
            }

            return temp;
        }
    }
}