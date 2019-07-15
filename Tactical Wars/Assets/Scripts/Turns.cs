using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    public bool turn = true; //Lleva el estado del turno
    public GameObject resourceManager; //Gestor de recursos
    public GameObject IA;

    public GameObject interfaz;

    //Pasamos de turno, y actualizamos los recursos y el turno
    public void Pass(int who)
    {
        if (who == 0 && turn == true)
        {
            turn = false;
            interfaz.GetComponent<Interfaz>().Pass(false);
            resourceManager.GetComponent<Resources>().EndTurnResources(0);
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (x.GetComponent<Unit>().playable == true) x.GetComponent<Unit>().RefreshSteps();
            }
            IA.GetComponent<IA>().IATurn();

        }
        else if(who == 1 && turn == false)
        {
            turn = true;
            resourceManager.GetComponent<Resources>().EndTurnResources(1);
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (x.GetComponent<Unit>().playable == false) x.GetComponent<Unit>().RefreshSteps();
            }
            interfaz.GetComponent<Interfaz>().Pass(true);
        }

    }

}
