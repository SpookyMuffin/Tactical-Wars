﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject AllyTank;
    public GameObject EnemyTank;
    public GameObject resourceManager;
    public GameObject UI;
    public GameObject Tile0;
    public GameObject Tile1;
    public GameObject turnManager;
    

    public void AllySpawnTank(int spawn)
    {
        if (turnManager.GetComponent<Turns>().turn == true && Tile0.GetComponent<Tile>().notWalkable == false)
        {
            Debug.Log("Paso el primer if"+Tile0.name);
            if (resourceManager.GetComponent<Resources>().GenerarTank(0) == true)
            {
                Debug.Log("Paso el 2º if");
                Instantiate(AllyTank);
                AllyTank.GetComponent<Unit>().type = 0;
                AllyTank.GetComponent<Unit>().playable = true;
                AllyTank.GetComponent<Unit>().UI = UI;
                AllyTank.GetComponent<Unit>().resourceManager = resourceManager;
                AllyTank.GetComponent<Unit>().initialiteUnit(Tile0);

            }
        }
    }

    public void EnemySpawnTank(int spawn)
    {
        if (turnManager.GetComponent<Turns>().turn == false && Tile1.GetComponent<Tile>().notWalkable == false)
        {
            
            if (resourceManager.GetComponent<Resources>().GenerarTank(1) == true)
            {
                Instantiate(EnemyTank);
                EnemyTank.GetComponent<Unit>().type = 0;
                EnemyTank.GetComponent<Unit>().playable = false;
                EnemyTank.GetComponent<Unit>().UI = UI;
                EnemyTank.GetComponent<Unit>().resourceManager = resourceManager;
                EnemyTank.GetComponent<Unit>().initialiteUnit(Tile1);


            }
        }
    }

}
