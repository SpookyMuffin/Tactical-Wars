﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    public bool turn = true; //Lleva el estado del turno
    public GameObject resourceManager; //Gestor de recursos
    public GameObject IA;

    public GameObject passButton, waitButton;

    //Pasamos de turno, y actualizamos los recursos y el turno
    public void Pass(int who)
    {
        if (who == 0)
        {
            passButton.SetActive(false);
            waitButton.SetActive(true);
            turn = false;
            resourceManager.GetComponent<Resources>().EndTurnResources(0);

            IA.GetComponent<IA>().IATurn();

        }
        else
        {

            turn = true;
            resourceManager.GetComponent<Resources>().EndTurnResources(1);
            waitButton.SetActive(false);
            passButton.SetActive(true);
        }

    }

}
