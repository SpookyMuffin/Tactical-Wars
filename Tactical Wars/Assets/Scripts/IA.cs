using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    GameObject TurnManager;
    GameObject[] Units;
    GameObject[] IAUnits;
    GameObject[] PlayerUnits;
    GameObject[] Buildings;
    GameObject[] IABuildings;
    GameObject[] PlayerBuildings;

    public void IATurn()
    {
        StartCoroutine(Example());
        
    }
    IEnumerator Example()
    {
        yield return new WaitForSeconds(1);
        TurnManager.GetComponent<Turns>().Pass(1);
    }
}
