using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    /* Número de filas del mapa */
    public int ROWS = 4; 
    /* Número de columnas del mapa */
    public int COLS = 5; 

    /* Vector y matriz de objetos de tipo casillas */
    GameObject[] tiles;
    public GameObject[,] mTiles;

    /* Materiales usados en las casillas */
    public Material allyMat;
    public Material enemyMat;
    public Material neutralMat;
    public Material defMat;


    /* Referencias a otros objetos */
    public GameObject turno;
    public GameObject resourceManager;

    /* Inicializa el mapa de la escena */
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

    /* LLama a la función anterior */
    private void Awake()
    {
        SetMap();
    }

    /* Ejecuta el algoritmo A* dado dos posiciones en el mapa */
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

    /* Retorna una casilla dada una posicion */
    public GameObject returnTile(Vector2 x)
    {
        return mTiles[(int)x.x, (int)x.y];
    }

    /* Colorea las casillas dependiendo de los movimientos restantes o disponibles de una unidad */
    public void ColorTiles(bool ally, int steps, int x, int y, bool action)
    {
        if (ally)
        {
            mTiles[x, y].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
            if (turno.GetComponent<Turns>().turn && action == true && (resourceManager.GetComponent<Resources>().Raciones > 0 || mTiles[x, y].GetComponent<Tile>().obj.GetComponent<Unit>().feeded == true))
            {
                for (int i = 0; i <= (steps+1); i++)
                {
                    for (int j = (steps+1) - i; j >= 0; j--)
                    {
                        if (x + i < ROWS && y + j < COLS)
                        {
                            if (mTiles[x + i, y + j].GetComponent<Tile>().notWalkable == false && GetPath(new Vector2(x, y), new Vector2(x + i, y + j)).Count <= steps && GetPath(new Vector2(x, y), new Vector2(x + i, y + j)).Count > 0)
                            {
                                mTiles[x + i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                            }
                            else if (mTiles[x + i, y + j].GetComponent<Tile>().notWalkable == true && GetPath(new Vector2(x, y), new Vector2(x + i, y + j)).Count <= (steps + 1) && mTiles[x + i, y + j].GetComponent<Tile>().obj != null
                                && GetPath(new Vector2(x, y), new Vector2(x + i, y + j)).Count > 0)
                            {
                                if (mTiles[x + i, y + j].GetComponent<Tile>().obj.tag == "Unit")
                                {
                                    if(mTiles[x + i, y + j].GetComponent<Tile>().obj.GetComponent<Unit>().playable == false) mTiles[x + i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
                                }
                                else if(mTiles[x + i, y + j].GetComponent<Tile>().obj.tag == "Building")
                                {
                                    if (mTiles[x + i, y + j].GetComponent<Tile>().obj.GetComponent<Building>().status == 0) mTiles[x + i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = neutralMat;
                                    else if (mTiles[x + i, y + j].GetComponent<Tile>().obj.GetComponent<Building>().status == 2) mTiles[x + i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
                                }
                            }

                        }
                        if (x + i < ROWS && y - j >= 0)
                        {
                            if (mTiles[x + i, y - j].GetComponent<Tile>().notWalkable == false && GetPath(new Vector2(x, y), new Vector2(x + i, y - j)).Count <= steps && GetPath(new Vector2(x, y), new Vector2(x + i, y - j)).Count > 0)
                            {
                                mTiles[x + i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                            }
                            else if (mTiles[x + i, y - j].GetComponent<Tile>().notWalkable == true && GetPath(new Vector2(x, y), new Vector2(x + i, y - j)).Count <= (steps + 1) && mTiles[x + i, y - j].GetComponent<Tile>().obj != null
                                && GetPath(new Vector2(x, y), new Vector2(x + i, y - j)).Count > 0)
                            {
                                if (mTiles[x + i, y - j].GetComponent<Tile>().obj.tag == "Unit")
                                {
                                    if (mTiles[x + i, y - j].GetComponent<Tile>().obj.GetComponent<Unit>().playable == false) mTiles[x + i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
                                }
                                else if (mTiles[x + i, y - j].GetComponent<Tile>().obj.tag == "Building")
                                {
                                    if (mTiles[x + i, y - j].GetComponent<Tile>().obj.GetComponent<Building>().status == 0) mTiles[x + i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = neutralMat;
                                    else if (mTiles[x + i, y - j].GetComponent<Tile>().obj.GetComponent<Building>().status == 2) mTiles[x + i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
                                }
                            }
                        }
                        if (x - i >= 0 && y + j < COLS)
                        {
                            if (mTiles[x - i, y + j].GetComponent<Tile>().notWalkable == false && GetPath(new Vector2(x, y), new Vector2(x - i, y + j)).Count <= steps && GetPath(new Vector2(x, y), new Vector2(x - i, y + j)).Count > 0)
                            {
                                mTiles[x - i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                            }
                            else if (mTiles[x - i, y + j].GetComponent<Tile>().notWalkable == true && GetPath(new Vector2(x, y), new Vector2(x - i, y + j)).Count <= (steps + 1) && mTiles[x - i, y + j].GetComponent<Tile>().obj != null
                                && GetPath(new Vector2(x, y), new Vector2(x - i, y + j)).Count > 0)
                            {
                                if (mTiles[x - i, y + j].GetComponent<Tile>().obj.tag == "Unit")
                                {
                                    if (mTiles[x - i, y + j].GetComponent<Tile>().obj.GetComponent<Unit>().playable == false) mTiles[x - i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
                                }
                                else if (mTiles[x - i, y + j].GetComponent<Tile>().obj.tag == "Building")
                                {
                                    if (mTiles[x - i, y + j].GetComponent<Tile>().obj.GetComponent<Building>().status == 0) mTiles[x - i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = neutralMat;
                                    if (mTiles[x - i, y + j].GetComponent<Tile>().obj.GetComponent<Building>().status == 2) mTiles[x - i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
                                }
                            }
                        }
                        if (x - i >= 0 && y - j >= 0)
                        {
                            if (mTiles[x - i, y - j].GetComponent<Tile>().notWalkable == false && GetPath(new Vector2(x, y), new Vector2(x - i, y - j)).Count <= steps && GetPath(new Vector2(x, y), new Vector2(x - i, y - j)).Count > 0)
                            {
                                mTiles[x - i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                            }
                            else if (mTiles[x - i, y - j].GetComponent<Tile>().notWalkable == true && GetPath(new Vector2(x, y), new Vector2(x - i, y - j)).Count <= (steps + 1) && mTiles[x - i, y - j].GetComponent<Tile>().obj != null
                                && GetPath(new Vector2(x, y), new Vector2(x - i, y - j)).Count > 0)
                            {
                                if (mTiles[x - i, y - j].GetComponent<Tile>().obj.tag == "Unit")
                                {
                                    if (mTiles[x - i, y - j].GetComponent<Tile>().obj.GetComponent<Unit>().playable == false) mTiles[x - i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
                                }
                                else if (mTiles[x - i, y - j].GetComponent<Tile>().obj.tag == "Building")
                                {
                                    if (mTiles[x - i, y - j].GetComponent<Tile>().obj.GetComponent<Building>().status == 0) mTiles[x - i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = neutralMat;
                                    else if (mTiles[x - i, y - j].GetComponent<Tile>().obj.GetComponent<Building>().status == 2) mTiles[x - i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
                                
                                }
                            }
                        }
                    }
                }
            }


        }
        else if(ally == false)
        {

            if (turno.GetComponent<Turns>().turn)
                mTiles[x, y].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
            {

                for (int i = 0; i <= (steps + 1); i++)
                {
                    for (int j = (steps + 1) - i; j >= 0; j--)
                    {
                        if (x + i < ROWS && y + j < COLS)
                        {
                            if (mTiles[x + i, y + j].GetComponent<Tile>().notWalkable == false && GetPath(new Vector2(x, y), new Vector2(x + i, y + j)).Count <= steps && GetPath(new Vector2(x, y), new Vector2(x + i, y + j)).Count > 0)
                            {
                                mTiles[x + i, y + j].transform.GetChild(0).GetComponent<Renderer>().material =enemyMat;
                            }
                            else if (mTiles[x + i, y + j].GetComponent<Tile>().notWalkable == true && GetPath(new Vector2(x, y), new Vector2(x + i, y + j)).Count <= (steps + 1) && mTiles[x + i, y + j].GetComponent<Tile>().obj != null
                                 && GetPath(new Vector2(x, y), new Vector2(x + i, y + j)).Count > 0)
                            {
                                if (mTiles[x + i, y + j].GetComponent<Tile>().obj.tag == "Unit")
                                {
                                    if (mTiles[x + i, y + j].GetComponent<Tile>().obj.GetComponent<Unit>().playable == true) mTiles[x + i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                                }
                                else if (mTiles[x + i, y + j].GetComponent<Tile>().obj.tag == "Building")
                                {
                                    if (mTiles[x + i, y + j].GetComponent<Tile>().obj.GetComponent<Building>().status == 1) mTiles[x + i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                                    else if(mTiles[x + i, y + j].GetComponent<Tile>().obj.GetComponent<Building>().status == 0) mTiles[x + i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = neutralMat;
                                }
                            }

                        }
                        if (x + i < ROWS && y - j >= 0)
                        {
                            if (mTiles[x + i, y - j].GetComponent<Tile>().notWalkable == false && GetPath(new Vector2(x, y), new Vector2(x + i, y - j)).Count <= steps && GetPath(new Vector2(x, y), new Vector2(x + i, y - j)).Count > 0)
                            {
                                mTiles[x + i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
                            }
                            else if (mTiles[x + i, y - j].GetComponent<Tile>().notWalkable == true && GetPath(new Vector2(x, y), new Vector2(x + i, y - j)).Count <= (steps + 1) && mTiles[x + i, y - j].GetComponent<Tile>().obj != null
                                && GetPath(new Vector2(x, y), new Vector2(x + i, y - j)).Count > 0)
                            {
                                if (mTiles[x + i, y - j].GetComponent<Tile>().obj.tag == "Unit")
                                {
                                    if (mTiles[x + i, y - j].GetComponent<Tile>().obj.GetComponent<Unit>().playable == true) mTiles[x + i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                                }
                                else if (mTiles[x + i, y - j].GetComponent<Tile>().obj.tag == "Building")
                                {
                                    if (mTiles[x + i, y - j].GetComponent<Tile>().obj.GetComponent<Building>().status == 1) mTiles[x + i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                                    else if (mTiles[x + i, y - j].GetComponent<Tile>().obj.GetComponent<Building>().status == 0) mTiles[x + i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = neutralMat;
                                }
                            }
                        }
                        if (x - i >= 0 && y + j < COLS)
                        {
                            if (mTiles[x - i, y + j].GetComponent<Tile>().notWalkable == false && GetPath(new Vector2(x, y), new Vector2(x - i, y + j)).Count <= steps && GetPath(new Vector2(x, y), new Vector2(x - i, y + j)).Count > 0)
                            {
                                mTiles[x - i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
                            }
                            else if (mTiles[x - i, y + j].GetComponent<Tile>().notWalkable == true && GetPath(new Vector2(x, y), new Vector2(x - i, y + j)).Count <= (steps + 1) && mTiles[x - i, y + j].GetComponent<Tile>().obj != null
                                && GetPath(new Vector2(x, y), new Vector2(x - i, y + j)).Count > 0)
                            {
                                if (mTiles[x - i, y + j].GetComponent<Tile>().obj.tag == "Unit")
                                {
                                    if (mTiles[x - i, y + j].GetComponent<Tile>().obj.GetComponent<Unit>().playable == true) mTiles[x - i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                                }
                                else if (mTiles[x - i, y + j].GetComponent<Tile>().obj.tag == "Building")
                                {
                                    if (mTiles[x - i, y + j].GetComponent<Tile>().obj.GetComponent<Building>().status == 1) mTiles[x - i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                                    else if (mTiles[x - i, y + j].GetComponent<Tile>().obj.GetComponent<Building>().status == 0) mTiles[x - i, y + j].transform.GetChild(0).GetComponent<Renderer>().material = neutralMat;
                                }
                            }
                        }
                        if (x - i >= 0 && y - j >= 0)
                        {
                            if (mTiles[x - i, y - j].GetComponent<Tile>().notWalkable == false && GetPath(new Vector2(x, y), new Vector2(x - i, y - j)).Count <= steps && GetPath(new Vector2(x, y), new Vector2(x - i, y - j)).Count > 0)
                            {
                                mTiles[x - i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
                            }
                            else if (mTiles[x - i, y - j].GetComponent<Tile>().notWalkable == true && GetPath(new Vector2(x, y), new Vector2(x - i, y - j)).Count <= (steps + 1) && mTiles[x - i, y - j].GetComponent<Tile>().obj != null
                                && GetPath(new Vector2(x, y), new Vector2(x - i, y - j)).Count > 0)
                            {
                                if (mTiles[x - i, y - j].GetComponent<Tile>().obj.tag == "Unit")
                                {
                                    if (mTiles[x - i, y - j].GetComponent<Tile>().obj.GetComponent<Unit>().playable == true) mTiles[x - i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                                }
                                else if (mTiles[x - i, y - j].GetComponent<Tile>().obj.tag == "Building")
                                {
                                    if (mTiles[x - i, y - j].GetComponent<Tile>().obj.GetComponent<Building>().status == 1) mTiles[x - i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
                                    else if (mTiles[x - i, y - j].GetComponent<Tile>().obj.GetComponent<Building>().status == 0) mTiles[x - i, y - j].transform.GetChild(0).GetComponent<Renderer>().material = neutralMat;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /* Vuelve de color inicial todas las casillas */
    public void resetTiles()
    {
        for (int i = 0; i < ROWS; i++)
        {
            for(int j = 0; j < COLS; j++)
            {
                mTiles[i, j].transform.GetChild(0).GetComponent<Renderer>().material = defMat;
            }
        }
    }

    /* Colorea una sola casillas */
    public void ColorTile(int x, int y, int mat)
    {
        if(mat == 0) mTiles[x, y].transform.GetChild(0).GetComponent<Renderer>().material = neutralMat;
        else if(mat == 1) mTiles[x, y].transform.GetChild(0).GetComponent<Renderer>().material = allyMat;
        else if (mat == 2) mTiles[x, y].transform.GetChild(0).GetComponent<Renderer>().material = enemyMat;
    }
}
