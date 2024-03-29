﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    public GameObject TurnManager;
    public GameObject Map;
    public GameObject resourceManager;
    public GameObject spawn;

    List<GameObject> IAUnits = new List<GameObject>();
    List<GameObject> PlayerUnits = new List<GameObject>();

    List<GameObject> IABuildings = new List<GameObject>();
    List<GameObject> PlayerBuildings = new List<GameObject>();
    List<GameObject> NeutralBuildings = new List<GameObject>();

    public Material IAMaterial;



    public void IATurn()
    {
        StartCoroutine(Move(0)); 
    }
    IEnumerator Move(int numUnit)
    {

        
        FindUnitsAndBuildings();
        Debug.Log(numUnit);
        GameObject nearest = null;
        if(IAUnits.Count > 0)
        {
            nearest = encuentraMasCercano(IAUnits[numUnit]);
        }

        bool HasCombustible;
        if (resourceManager.GetComponent<Resources>().EnemyCombustible >= resourceManager.GetComponent<Resources>().gastoCombustibleTank) HasCombustible = true;
        else HasCombustible = false;

        if (nearest != null)
        {
            Debug.Log("Nearest tag: " + nearest.tag);
            List<GameObject> Path;
            if (nearest.tag == "Unit") Path = Map.GetComponent<Map>().GetPath(IAUnits[numUnit].GetComponent<Unit>().getPos(), nearest.GetComponent<Unit>().getPos());
            else Path = Map.GetComponent<Map>().GetPath(IAUnits[numUnit].GetComponent<Unit>().getPos(), nearest.GetComponent<Building>().getPos());
            int tempSteps = IAUnits[numUnit].GetComponent<Unit>().steps;
            int k = 0;
            while (k < Path.Count && IAUnits[numUnit].GetComponent<Unit>().steps > 0 && Path[k].GetComponent<Tile>().notWalkable == false && HasCombustible == true)
            {

                IAUnits[numUnit].GetComponent<Unit>().Move(Path[k]);
                k++;
                Debug.Log("muevo");
                yield return new WaitForSeconds(1f);
            }
           

            if (nearest.tag == "Building")
            {
                if (CalculaDistanciaEuclidea(IAUnits[numUnit].GetComponent<Unit>().Tile.GetComponent<Tile>().x, IAUnits[numUnit].GetComponent<Unit>().Tile.GetComponent<Tile>().y,
        nearest.GetComponent<Building>().Tile.GetComponent<Tile>().x, nearest.GetComponent<Building>().Tile.GetComponent<Tile>().y) <= 1)
                {
                    IAUnits[numUnit].GetComponent<Unit>().Conquer(nearest, IAMaterial);
                    Debug.Log("Conquisto");
                    yield return new WaitForSeconds(1f);
                }
            }
            else if (nearest.tag == "Unit" && nearest != null)
            {
                if (CalculaDistanciaEuclidea(IAUnits[numUnit].GetComponent<Unit>().Tile.GetComponent<Tile>().x, IAUnits[numUnit].GetComponent<Unit>().Tile.GetComponent<Tile>().y,
        nearest.GetComponent<Unit>().Tile.GetComponent<Tile>().x, nearest.GetComponent<Unit>().Tile.GetComponent<Tile>().y) <= 1)
                {

                    IAUnits[numUnit].GetComponent<Unit>().Attack(nearest);
                    Debug.Log("ataco");
                    yield return new WaitForSeconds(1f);


                }
            }

            numUnit++;
            if (numUnit < IAUnits.Count) yield return Move(numUnit);
            else
            {
                if (resourceManager.GetComponent<Resources>().EnemyGoldmarks > resourceManager.GetComponent<Resources>().PrecioTank)
                {
                    Debug.Log("Intento Spawnear un tank");
                    spawn.GetComponent<Spawn>().EnemySpawnTank(0);
                    yield return new WaitForSeconds(1f);
                }
                yield return endTurn();
            }
        }
        else yield return endTurn();

    }
    IEnumerator endTurn()
    {
        TurnManager.GetComponent<Turns>().Pass(1);
        Debug.Log("Pulso el end");
        yield return new WaitForSeconds(0);

    }

    void FindUnitsAndBuildings()
    {
        IAUnits.Clear();
        PlayerUnits.Clear();
        IABuildings.Clear();
        PlayerBuildings.Clear();
        NeutralBuildings.Clear();

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if(x.GetComponent<Unit>().playable == true)
            {
                PlayerUnits.Add(x);
            }
            else
            {
                IAUnits.Add(x);
            }

            foreach(GameObject y in GameObject.FindGameObjectsWithTag("Building"))
            {
                if (y.GetComponent<Building>().status == 0)
                {

                    NeutralBuildings.Add(y);
                }
                else if (y.GetComponent<Building>().status == 1)
                {

                    PlayerBuildings.Add(y);
                }
                else IABuildings.Add(y);
            }
        }

    }
    float CalculaDistanciaEuclidea(int x1, int y1, int x2, int y2)
    {
        return Mathf.Sqrt(Mathf.Pow(x2 - x1, 2f) + Mathf.Pow(y2 - y1, 2f));
    }
    
    GameObject encuentraMasCercano(GameObject x)
    {
        GameObject nearObject = null;
        float temp = 999; ;
        float distNearest = 999;
        if(PlayerUnits.Count > 0)
        {
            foreach (GameObject playerUnit in PlayerUnits)
            {
                temp = CalculaDistanciaEuclidea(x.GetComponent<Unit>().Tile.GetComponent<Tile>().x, x.GetComponent<Unit>().Tile.GetComponent<Tile>().y,
                    playerUnit.GetComponent<Unit>().Tile.GetComponent<Tile>().x, playerUnit.GetComponent<Unit>().Tile.GetComponent<Tile>().y);
                if (temp < distNearest)
                {

                    distNearest = temp;
                    nearObject = playerUnit;
                }

            }
        }

        if(PlayerBuildings.Count > 0)
        {
            foreach (GameObject pb in PlayerBuildings)
            {
                temp = CalculaDistanciaEuclidea(x.GetComponent<Unit>().Tile.GetComponent<Tile>().x, x.GetComponent<Unit>().Tile.GetComponent<Tile>().y,
                    pb.GetComponent<Building>().Tile.GetComponent<Tile>().x, pb.GetComponent<Building>().Tile.GetComponent<Tile>().y);
                if (temp < distNearest)
                {

                    distNearest = temp;
                    nearObject = pb;
                }

            }
        }

        if(NeutralBuildings.Count > 0)
        {
            foreach (GameObject nb in NeutralBuildings)
            {
                temp = CalculaDistanciaEuclidea(x.GetComponent<Unit>().Tile.GetComponent<Tile>().x, x.GetComponent<Unit>().Tile.GetComponent<Tile>().y,
                    nb.GetComponent<Building>().Tile.GetComponent<Tile>().x, nb.GetComponent<Building>().Tile.GetComponent<Tile>().y);
                if (temp < distNearest)
                {

                    distNearest = temp;
                    nearObject = nb;
                }

            }
        }

        return nearObject;
    }

}
