using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public class Node
    {
        Node Parent;
        Vector2 Pos;
        public float G, H, F;
        bool Walkable;

        public Node(Vector2 posicion, bool valido)
        {
            Parent = null;
            F = 0;
            G = 0;
            H = 0;
            Pos = posicion;
            Walkable = valido;
        }
        public float GetHValue()
        {
            F = G + H;
            return F;
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

        public Stack<Node> FindPath(Vector2 start, Vector2 end)
        {
            Stack<Node> Path = new Stack<Node>();

            List<Node> Closed = new List<Node>();
            List<Node> Open = new List<Node>();

            Node Start = new Node(start, true);
            Node End = new Node(end, true);

            Node Q;

            Closed.Add(Start);

            while(Closed.Count != 0)
            {
                Closed = Closed.OrderBy(x => x.F).ToList();


            }

            return Path;
        }

    }
}
