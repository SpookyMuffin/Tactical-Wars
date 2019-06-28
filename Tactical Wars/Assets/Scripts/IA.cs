using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    public GameObject TurnManager;

    public void IATurn()
    {
        StartCoroutine(Example());
        
    }
    IEnumerator Example()
    {
        yield return new WaitForSeconds(5);
        TurnManager.GetComponent<Turns>().Pass(1);
    }
}
