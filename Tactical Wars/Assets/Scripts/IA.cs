using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    public GameObject TurnManager;
    public GameObject Map;
    List<GameObject> IAUnits = new List<GameObject>();
    List<GameObject> PlayerUnits = new List<GameObject>();
    List<GameObject> Path;

    public void IATurn()
    {
        int tempSteps;
        FindUnits();
         foreach (GameObject x in IAUnits)
         {
             tempSteps = x.GetComponent<Unit>().steps;
             Path = Map.GetComponent<Map>().GetPath(x.GetComponent<Unit>().getPos(), PlayerUnits[0].GetComponent<Unit>().getPos());
             StartCoroutine(move(tempSteps, x, Path));
         }
    }
    IEnumerator move(int steps, GameObject unit, List<GameObject> path)
    {
        int k = 0;
        for (int i = 0; i < steps; i++)
        {
            if (k < Path.Count)
            {
                
                unit.GetComponent<Unit>().Move(Path[k]);
                k++;
                yield return new WaitForSeconds(1f);
            }
        }
        yield return endTurn();
        
    }
    IEnumerator endTurn()
    {
        TurnManager.GetComponent<Turns>().Pass(1);
        yield return new WaitForSeconds(0f);

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

}
