using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    public int ROWS = 4; //Numero de filas de casillas
    public int COLS = 5; //Numero de columnas de casillas

    //Materiales para indicar las casillas y enemigso
    public Material neutral, enemy, ally;
    public Material emptyTile, CanMove, cantMove;
    //Vectores de casillas y grids
    GameObject[] tiles;
    public GameObject[,] mTiles;


    void SetMap()
    {
        mTiles = new GameObject[ROWS, COLS]; //Matirz de casillas
        int x, y;
        //Creamos la matriz de casillas
        tiles = GameObject.FindGameObjectsWithTag("Tile"); //ponemos todas las casillas en un vector

        //Las metemos en la matriz
        int k = 0;
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
               
                x = tiles[k].GetComponent<Tile>().x;
                y = tiles[k].GetComponent<Tile>().y;
                mTiles[x, y] = tiles[k];
                k++;
            }
        }
    }
    private void Awake()
    {
        SetMap();
    }

    /*private void Start()
    {
       List<Pathfinding.Node> camino;
       Pathfinding.ASTAR pathF = new Pathfinding.ASTAR(mTiles, ROWS, COLS);
        camino = pathF.FindPath(new Vector2(0,0),new Vector2(2,2));

       if (camino != null)
       {
           foreach (Pathfinding.Node n in camino)
           {

               Debug.Log(n.Pos);
           }

       }
    }*/

    public List<GameObject> GetPath(Vector2 Start, Vector2 End)
    {
        List<Pathfinding.Node> camino;
        List<GameObject> ruta = new List<GameObject>();
        Pathfinding.ASTAR pathF = new Pathfinding.ASTAR(mTiles, ROWS, COLS);
        camino = pathF.FindPath(Start,End);

        if (camino != null)
        {
            foreach (Pathfinding.Node n in camino)
            {
                ruta.Add(mTiles[(int)n.Pos.x, (int)n.Pos.y]);
            }
        }
        return ruta;
    }

    
}
