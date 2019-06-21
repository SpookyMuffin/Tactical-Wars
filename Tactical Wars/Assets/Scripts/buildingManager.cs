using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingManager : MonoBehaviour
{
    //Lista de edificios
    public List<GameObject> edificios;

    //Devuelve entero con el numero de campamentos aliados
   public int AllyCamps()
   {
        int campCount = 0;
        foreach (GameObject x in edificios){
            if (x.GetComponent<Building>().type == 1 && x.GetComponent<Building>().status == 1) campCount++;
        }
        return campCount;
   }
    //Devuelve entero con el numero de campamentos enemigos
    public int EnemyCamps()
    {
        int campCount = 0;
        foreach (GameObject x in edificios)
        {
            if (x.GetComponent<Building>().type == 1 && x.GetComponent<Building>().status == 2) campCount++;
        }
        return campCount;
    }

    //Devuelve el numero de edificios que obtienen petroleo aliadas
    public int AllyPumps()
    {
       int pumpCount = 0;
        foreach (GameObject x in edificios)
        {
            if (x.GetComponent<Building>().type == 2 && x.GetComponent<Building>().status == 1) pumpCount++;
        }
        return pumpCount;
    }

    //Similar a la funcion anterior pero del enemigo.
    public int EnemyPumps()
    {
        int pumpCount = 0;

        foreach (GameObject x in edificios)
        {
            if (x.GetComponent<Building>().type == 2 && x.GetComponent<Building>().status == 2) pumpCount++;
        }
        return pumpCount;
    }


}
