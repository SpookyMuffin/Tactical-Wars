using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Tank;
    public GameObject resourceManager;
    public GameObject Tile;

    //Funcion para generar una unidad. Type = 0 genera un tank
    /*public void AllySpawnUnit1(int type)
    {
        //Posicion del spawn
        int x = 0;
        int y = 0;
        if (resourceManager.GetComponent<Resources>().Goldmarks >= resourceManager.GetComponent<Resources>().PrecioTank && Map.mTiles[x, y].GetComponent<Tile>().notWalkable == false)
        {
            //instanciamos el tank y lo incializamos
            Instantiate(Tank);
            Tank.GetComponent<Unit>().Tile = Map.mTiles[x, y];
            Tank.transform.position = Map.mTiles[x, y].transform.position;
            Map.mTiles[x, y].GetComponent<Tile>().obj = Tank;
            Map.mTiles[x, y].GetComponent<Tile>().notWalkable = true;
            resourceManager.GetComponent<Resources>().GenerarTank(0);
        }

    }*/
}
