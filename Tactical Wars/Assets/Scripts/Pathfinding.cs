using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public class Node
    {
        public Node Parent;
        public Vector2 Pos;
        public float G, H, F;
        public bool Walkable;

        public Node(Vector2 posicion, bool valido)
        {
            Parent = null;
            F = 0;
            G = 0;
            H = 0;
            Pos = posicion;
            Walkable = valido;
        }
        public float GetAndSetHValue()
        {
            F = G + H;
            return F;
        }
        public void SetHValue(Vector2 current, Vector2 end, Node n)
        {
            float distance;
            distance = Mathf.Sqrt(Mathf.Pow(end.x - current.x, 2f) + Mathf.Pow(end.y - current.y, 2f));
            n.H = distance;
        }
    }
    public class ASTAR
    {
        Node[,] Map;
        int MapRows;
        int MapCols;
       

        public void SetMap(GameObject[,] Tiles, int ROWS, int COLS)
        {
            Map = new Node[ROWS, COLS];
            
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j< COLS; j++)
                {
                    Map[i, j] = new Node(new Vector2(Tiles[i, j].GetComponent<Tile>().x,
                        Tiles[i, j].GetComponent<Tile>().y), !Tiles[i, j].GetComponent<Tile>().notWalkable);
                    
                }
            }
        }

        public ASTAR(GameObject[,] Tiles, int r, int c)
        {
            SetMap(Tiles, r, c);
            MapRows = r;
            MapCols = c;
        }

        public List<Node> FindPath(Vector2 start, Vector2 end)
        {
            
            List<Node> Closed = new List<Node>();
            List<Node> Open = new List<Node>();

            Node Start = new Node(start, true);
            Node End = new Node(end, true);

            Node Q;

            List<Node> temp;
   

            Open.Add(Start);

            while(Open.Count != 0 && !Closed.Exists(x => x.Pos == end))
            {
                Open = Open.OrderBy(x => x.F).ToList();
                Q = Open[0];
                Open.Remove(Q);
                temp = GetAdjacentNodes(Q);

                //debug para ver si todo esta correcto
               /*Debug.Log("Adyacentes de " + Q.Pos);
                foreach (Node x in temp)
                {
                    Debug.Log(x.Pos);
                }*/


                SetPartentToList(temp, Q);

                foreach(Node n in temp)
                {
                    if(!Closed.Contains(n) && n.Walkable)
                    {
                        if (!Open.Contains(n))
                        {
                            n.G = n.Parent.G + 1;
                            n.SetHValue(n.Pos, end, n);
                            n.GetAndSetHValue();
                            Open.Add(n);
                        }
                    }
                    
                }
                Closed.Add(Q);
            }

            return Closed;
        }

        private List<Node> GetAdjacentNodes(Node n)
        {
            List<Node> lista = new List<Node>();
            int row = (int)n.Pos.x;
            int col = (int)n.Pos.y;

            if (col + 1 < MapRows)
            {
                lista.Add(Map[row,col + 1]);
            }
            if (col - 1 >= 0)
            {
                lista.Add(Map[row,col - 1]);
            }
            if (row - 1 >= 0)
            {
                lista.Add(Map[row - 1,col]);
            }
            if (row + 1 < MapCols)
            {
                lista.Add(Map[row + 1,col]);
            }

            return lista;

        }

        private void SetPartentToList(List<Node> list, Node parent)
        {
            foreach (Node n in list)
            {
                n.Parent = parent;
            }
        }
    }
}
