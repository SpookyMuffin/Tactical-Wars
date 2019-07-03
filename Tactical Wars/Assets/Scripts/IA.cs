using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    public GameObject TurnManager;
    public GameObject Map;
    List<GameObject> IAUnits = new List<GameObject>();
    List<GameObject> PlayerUnits = new List<GameObject>();



    public void IATurn()
    {

        FindUnits();
        StartCoroutine(move(0));
      
    }
    IEnumerator move(int numUnit)
    {
        Debug.Log(numUnit);
        GameObject nearest;
        nearest = encuentraMasCercano(IAUnits[numUnit]);
        List<GameObject> Path;
        Path = Map.GetComponent<Map>().GetPath(IAUnits[numUnit].GetComponent<Unit>().getPos(), nearest.GetComponent<Unit>().getPos());

        int tempSteps = IAUnits[numUnit].GetComponent<Unit>().steps;
        int k = 0;
        while (k < Path.Count && IAUnits[numUnit].GetComponent<Unit>().steps > 0 && Path[k].GetComponent<Tile>().notWalkable == false)
        {

            IAUnits[numUnit].GetComponent<Unit>().Move(Path[k]);
            k++;
            Debug.Log("muevo");
            yield return new WaitForSeconds(1f);
        }

        if (CalculaDistanciaEuclidea(IAUnits[numUnit].GetComponent<Unit>().Tile.GetComponent<Tile>().x, IAUnits[numUnit].GetComponent<Unit>().Tile.GetComponent<Tile>().y,
            nearest.GetComponent<Unit>().Tile.GetComponent<Tile>().x, nearest.GetComponent<Unit>().Tile.GetComponent<Tile>().y) <= 1)
        {
            IAUnits[numUnit].GetComponent<Unit>().Attack(nearest);
            Debug.Log("ataco");
            yield return new WaitForSeconds(1f);
        }


        numUnit++;
        if(numUnit < IAUnits.Count) yield return move(numUnit);
        else yield return endTurn();
    }
    IEnumerator endTurn()
    {
        TurnManager.GetComponent<Turns>().Pass(1);
        Debug.Log("Pulso el end");
        yield return new WaitForSeconds(0);

    }

    void FindUnits()
    {
        IAUnits.Clear();
        PlayerUnits.Clear();
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
        }

    }
    float CalculaDistanciaEuclidea(int x1, int y1, int x2, int y2)
    {
        return Mathf.Sqrt(Mathf.Pow(x2 - x1, 2f) + Mathf.Pow(y2 - y1, 2f));
    }
    
    GameObject encuentraMasCercano(GameObject x)
    {
        GameObject nearObject = PlayerUnits[0];
        float temp = 999; ;
        float distNearest = 999;
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
        return nearObject;
    }

}
