using System.Collections;
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
        if(turnManager.GetComponent<Turns>().turn == true && resourceManager.GetComponent<Resources>().GenerarTank(0) == true)
        {
            Instantiate(AllyTank);
            if (AllyTank.GetComponent<Unit>().initialiteUnit(Tile0))
            {
                AllyTank.GetComponent<Unit>().type = 0;
                AllyTank.GetComponent<Unit>().playable = true;
                AllyTank.GetComponent<Unit>().UI = UI;
                AllyTank.GetComponent<Unit>().resourceManager = resourceManager;
            }
            else Destroy(AllyTank);


        }
    }
    public void EnemySpawnTank(int spawn)
    {
        if (turnManager.GetComponent<Turns>().turn == false && resourceManager.GetComponent<Resources>().GenerarTank(1) == true)
        {
            
            if (Tile1.GetComponent<Tile>().notWalkable == false)
            {
                Instantiate(EnemyTank);
                EnemyTank.GetComponent<Unit>().initialiteUnit(Tile1);
                EnemyTank.GetComponent<Unit>().type = 0;
                EnemyTank.GetComponent<Unit>().playable = false;
                EnemyTank.GetComponent<Unit>().UI = UI;
                EnemyTank.GetComponent<Unit>().resourceManager = resourceManager;
            }
        }
    }

}
