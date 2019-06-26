using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    public int ROWS = 3; //Numero de columnas de casillas
    public int COLS = 3; //Numero de filas de casillas

    //Materiales para indicar las casillas y enemigso
    public Material neutral, enemy, ally;
    public Material emptyTile, CanMove, cantMove;
    //Vectores de casillas y grids
    GameObject[] Grid;
    GameObject[] tiles;
    public GameObject[,] mTiles;
    public GameObject[,] mGrid;
    List<Pathfinding.Node> camino;


    void Awake()
    {
        mTiles = new GameObject[ROWS, COLS]; //Matirz de casillas
        mGrid = new GameObject[ROWS, COLS]; //Matirz de grids

        //Creamos la matriz de casillas
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile"); //ponemos todas las casillas en un vector
        Grid = GameObject.FindGameObjectsWithTag("GridPanel"); //grid en vector

        //Las metemos en la matriz
        int k = 0;
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                mTiles[i, j] = tiles[k];
                mGrid[i, j] = Grid[k];
                k++;
            }
        }
    }

    private void Start()
    {
       Pathfinding.ASTAR pathF = new Pathfinding.ASTAR(mTiles, ROWS, COLS);
       camino = pathF.FindPath(new Vector2(0,0),new Vector2(1,0));
       foreach(Pathfinding.Node n in camino)
       {

            Debug.Log(n.Pos);
       }

    }

    //Para mostrar las rejillas 
    public void MostrarGrid()
    {
       
        foreach (GameObject x in Grid)
        {
            x.SetActive(true);
        }
    }

    //Ocultar rejillas
    public void OcultarGrid()
    {
        foreach (GameObject x in Grid)
        {
            x.SetActive(false);
        }
    }

    public void ResetColorGrid()
    {
        foreach (GameObject x in Grid)
        {
            x.GetComponent<Renderer>().material = neutral;
        }
    }
}
