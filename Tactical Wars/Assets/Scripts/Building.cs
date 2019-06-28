using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public GameObject Tile; //Casilla en la que esta el edificio
    public int status = 0; // Estado del edificio 0 = netrual \\ 1 = conquistado\\ 2 = enemigo lo ha conquistado
    public int progress = 0; // Progreso de conquista de un edificio, si llega a -100 se pone a propiedad del enemigo, 100 de alidado.
                             //Tiene que llegar a -100 o a 100 para cambiar de bando, si esta entre 100 y -100, es del ultimo propietario
    public int type = 99; //Tipo del edifico  0 = cuartel general, 1 = campamento 2 = pump


    public GameObject UI;


    //Funcion para conquista de un edificio
    //Who = 0 es del jugador
    //Who = 1 es de la IA
    public void Conquer(int who, Material mat)
    {
        if (who == 0 && status == 0)
        {
            progress += 50;
            if (progress >= 100)
            {
                progress = 100;
                status = 1;
                this.GetComponent<Renderer>().material = mat;
            }

        }
        if(who == 0 && status == 2)
        {
            progress += 50;
            if (progress >= 100)
            {
                progress = 100;
                status = 1;
                this.GetComponent<Renderer>().material = mat;
            }
        }
        if(who == 1 && status == 0)
        {
            progress -= 50;
            if (progress <= -100)
            {
                progress = -100;
                status = 2;
                this.GetComponent<Renderer>().material = mat;
            }
        }
        if (who == 1 && status == 1)
        {
            progress -= 50;
            if (progress <= -100)
            {
                progress = -100;
                status = 2;
                this.GetComponent<Renderer>().material = mat;
            }
        }
    }

    //Funcion para refrescar la interfaz con los parametros de este edificio.
    public void Display()
    {
        UI.GetComponent<UI>().SwitchStatPanelBuilding(type, status, progress, Tile.name);
    }
}
