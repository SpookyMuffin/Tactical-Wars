using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    /* Clase nodo */
    public class Node
    {
        /* Nodo padre */
        public Node Parent;
        /* Posición que ocupa en la matriz */
        public Vector2 Pos;
        /* Parametros */
        public float G, H, F;
        /* Si se puede caminar sobre el nodo */
        public bool Walkable;

        /* Constructor de la clase */
        public Node(Vector2 posicion, bool valido)
        {
            Parent = null;
            F = 0;
            G = 0;
            H = 0;
            Pos = posicion;
            Walkable = valido;
        }
        /* Asigna F y lo devuelve dependiendo de un vector */
        public float GetAndSetFValue(Vector2 End)
        {
            SetHValue(End);
            F = G + H;
            return F;
        }

        /* Asigna el parametro H */
        public void SetHValue(Vector2 end)
        {
            float distance;
            Vector2 current = Pos;
            distance = Mathf.Sqrt(Mathf.Pow(end.x - current.x, 2f) + Mathf.Pow(end.y - current.y, 2f));
            H = distance;
        }
    }
    /* Clase A* */
    public class ASTAR
    {
        /* Mapa en forma de nodos */
        Node[,] Map;

        /* Filas del mapa */
        int MapRows;

        /* Columnas del mapa */
        int MapCols;
       
        /* Convierte el mapa de GameObject en nodo */
        public void SetMap(GameObject[,] Tiles, int ROWS, int COLS)
        {
            Map = new Node[ROWS, COLS];
            Node Q = new Node(new Vector2(0, 0), true);
            
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j< COLS; j++)
                {
                    Map[i, j] = new Node(new Vector2(Tiles[i, j].GetComponent<Tile>().x,
                        Tiles[i, j].GetComponent<Tile>().y), !Tiles[i, j].GetComponent<Tile>().notWalkable);
                    
                }
            }
        }

        /* Constructor de clase */
        public ASTAR(GameObject[,] Tiles, int r, int c)
        {
            SetMap(Tiles, r, c);
            MapRows = r;
            MapCols = c;
        }

        /* Función que ejecuta el el algoritmo */
        public List<Node> FindPath(Vector2 start, Vector2 end)
        {
            Map[(int)end.x, (int)end.y].Walkable = true;
            List<Node> Closed = new List<Node>();
            List<Node> Open = new List<Node>();

            Node Start = Map[(int)start.x, (int)start.y];
            Node End = Map[(int)end.x,(int)end.y];

            Node Q = Start;

            List<Node> temp;

            Open.Add(Start);

            while(Open.Count != 0 && !Closed.Exists(x => x.Pos == end))
            {
                Open = Open.OrderBy(x => x.F).ToList();
                Q = Open[0];
                Open.Remove(Q);
                temp = GetAdjacentNodes(Q);

                foreach(Node n in temp)
                {
                    if (!Closed.Contains(n) && n.Walkable)
                    {
                        if (!Open.Contains(n))
                        {
                            n.Parent = Q;
                            n.G = n.Parent.G + 1;
                            n.GetAndSetFValue(end);
                            Open.Add(n);
                            
                        }
                    }
                    
                }
                Closed.Add(Q);
            }
            List<Node> Path = new List<Node>();

            if (!Closed.Exists(x => x.Pos == end))
            {
                return null;
            }

            Node tempNode = Closed[Closed.IndexOf(Q)];
            while (tempNode.Parent != null)
            {
                Path.Insert(0, tempNode);

                tempNode = tempNode.Parent;

            }

            return Path;
        }

        /* Devuelve los nodos adyacentes de un nodo dado */
        private List<Node> GetAdjacentNodes(Node n)
        {
            List<Node> lista = new List<Node>();
            int row = (int)n.Pos.x;
            int col = (int)n.Pos.y;

            if (col + 1 < MapCols)
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
            if (row + 1 < MapRows)
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
