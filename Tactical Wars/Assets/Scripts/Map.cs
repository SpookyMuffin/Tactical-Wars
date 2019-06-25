using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    public static int x = 3; //Numero de columnas de casillas
    public static int y = 3; //Numero de filas de casillas

    public static GameObject[,] mTiles = new GameObject[x,y]; //Matirz de casillas
    public static GameObject[,] mGrid = new GameObject[x, y]; //Matirz de grids

    //Materiales para indicar las casillas y enemigso
    public Material neutral, enemy, ally;
    public Material emptyTile, CanMove, cantMove;
    //Vectores de casillas y grids
    GameObject[] Grid;
    GameObject[] tiles;

    

    void Awake()
    { 
        //Creamos la matriz de casillas
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile"); //ponemos todas las casillas en un vector
        Grid = GameObject.FindGameObjectsWithTag("GridPanel"); //grid en vector

        //Las metemos en la matriz
        int k = 0; 
        for (int i = 0; i < x; i++){
            for (int j = 0; j < y; j++){
                mTiles[i,j] = tiles[k];
                mGrid[i, j] = Grid[k];
                k++;
            }
        }

        Pathfinding();
    }

    public void Pathfinding()
    {
        Vector2 inicio = new Vector2(0, 1);
        Vector2 destino = new Vector2(2, 1);
        Stack<aStar.Node> path;
        List<List<aStar.Node>> pathmap = MakeMapPath();
        aStar.Astar AEstrella = new aStar.Astar(pathmap);
        aStar.Node aux;

        path = AEstrella.FindPath(inicio, destino);
        for(int i = 0; i < path.Count; i++)
        {
            aux = path.Pop();
            Debug.Log("x: " + aux.Position.x + " " + "y: " + aux.Position.y);
        }

    }

    List<List<aStar.Node>> MakeMapPath()
    {

        List<List<aStar.Node>> PathMap = new List<List<aStar.Node>>();
        Vector2 pos;
        bool walkable;
        for (int i = 0; i < x; i++)
        {
            List<aStar.Node> temp = new List<aStar.Node>();
            for (int j = 0; j < y; j++)
            {
                pos.x = mTiles[i, j].GetComponent<Tile>().x;
                pos.y = mTiles[i, j].GetComponent<Tile>().y;
                walkable = !mTiles[i, j].GetComponent<Tile>().notWalkable;
                
                temp.Add(new aStar.Node(pos,walkable));
            }
            PathMap.Add(temp);
        }

        return PathMap;
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
