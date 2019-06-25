using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public class Node
    {

        Node Parent;
        Vector2 Pos;
        int F, G, H;
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
    }
    public class ASTAR
    {
        List<Node> Closed;
        List<Node> Open;
    }
}
