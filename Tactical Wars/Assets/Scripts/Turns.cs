using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    public bool turn = true; //Lleva el estado del turno
    public GameObject resourceManager; //Gestor de recursos
    public GameObject IA;

    public GameObject UI;

    //Pasamos de turno, y actualizamos los recursos y el turno
    public void Pass(int who)
    {
        if (who == 0)
        {
            turn = false;
            UI.GetComponent<UI>().Pass(false);
            resourceManager.GetComponent<Resources>().EndTurnResources(0);
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (x.GetComponent<Unit>().playable == true) x.GetComponent<Unit>().RefreshSteps();
            }
            IA.GetComponent<IA>().IATurn();

        }
        else
        {
            turn = true;
            resourceManager.GetComponent<Resources>().EndTurnResources(1);
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (x.GetComponent<Unit>().playable == false) x.GetComponent<Unit>().RefreshSteps();
            }
            UI.GetComponent<UI>().Pass(true);
        }

    }

}
