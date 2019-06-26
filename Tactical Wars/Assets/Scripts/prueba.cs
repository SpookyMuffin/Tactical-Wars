using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba : MonoBehaviour
{
    // Start is called before the first frame update

    Pathfinding.Node[,] Map = new Pathfinding.Node[4, 4];
    Pathfinding.Node Q = new Pathfinding.Node(new Vector2(0,0), true);
    void Start()
    {

        Map[1, 2].F = 5;

        Q = Map[1, 2];

        Debug.Log("map " + Map[1, 2].F);
        Debug.Log("Q " + Q.F);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
