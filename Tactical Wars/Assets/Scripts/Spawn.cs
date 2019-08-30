using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject ATank;
    public GameObject ETank;
    public GameObject resourceManager;
    public GameObject Tile0;
    public GameObject Tile1;
    public GameObject turnManager;
    public GameObject mapa;
    public GameObject interfaz;
    public GameObject audioData;

    

    public void AllySpawnTank(int spawn)
    {
        mapa.GetComponent<Map>().resetTiles();
        interfaz.GetComponent<Interfaz>().setSelectedObj(interfaz.GetComponent<Interfaz>().selectedObj);
        if (turnManager.GetComponent<Turns>().turn == true && Tile0.GetComponent<Tile>().notWalkable == false)
        {
            if (resourceManager.GetComponent<Resources>().GenerarTank(0) == true)
            {
                audioData.transform.GetChild(7).gameObject.SetActive(false);
                audioData.transform.GetChild(7).gameObject.SetActive(true);

                GameObject AllyTank =  Instantiate(ATank);
                AllyTank.GetComponent<Unit>().type = 0;
                AllyTank.GetComponent<Unit>().playable = true;
                AllyTank.GetComponent<Unit>().mapa = mapa;
                AllyTank.GetComponent<Unit>().interfaz = interfaz;
                AllyTank.GetComponent<Unit>().audioData = audioData;
                AllyTank.GetComponent<Unit>().initialiteUnit(Tile0);
                AllyTank.GetComponent<Unit>().feeded = false;



            }
        }
    }

    public void EnemySpawnTank(int spawn)
    {
        if (turnManager.GetComponent<Turns>().turn == false && Tile1.GetComponent<Tile>().notWalkable == false)
        {
            
            if (resourceManager.GetComponent<Resources>().GenerarTank(1) == true)
            {
                audioData.transform.GetChild(7).gameObject.SetActive(false);
                audioData.transform.GetChild(7).gameObject.SetActive(true);

                GameObject EnemyTank = Instantiate(ETank);
                EnemyTank.gameObject.name = ""+GameObject.FindGameObjectsWithTag("Unit").Length;
                EnemyTank.GetComponent<Unit>().type = 0;
                EnemyTank.GetComponent<Unit>().playable = false;
                EnemyTank.GetComponent<Unit>().resourceManager = resourceManager;
                EnemyTank.GetComponent<Unit>().mapa = mapa;
                EnemyTank.GetComponent<Unit>().interfaz = interfaz;
                EnemyTank.GetComponent<Unit>().audioData = audioData;
                EnemyTank.GetComponent<Unit>().initialiteUnit(Tile1);
                EnemyTank.GetComponent<Unit>().feeded = false;



            }
        }
    }

}
