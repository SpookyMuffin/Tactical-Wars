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


    public GameObject interfaz;
    public GameObject end;
    public GameObject mouse;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    //Funcion para conquista de un edificio
    //Who = 1 es del jugador antes 0
    //Who = -1 es de la IA antes 1
    public void Conquer(int who, Material mat)
    {
        progress += 50 * who;
        if (progress >= 100)
        {
            SetConquered(1, mat);
        }
        else if (progress <= -100)
        {
            SetConquered(-1,mat);
        }
    }

    private void SetConquered(int who, Material mat)
    {
        if (who == 1)
        {
            progress = 100;
            status = 1;
        }
        else if (who == -1)
        {
            progress = -100;
            status = 2;
        }

        this.GetComponent<Renderer>().material = mat;

        if (type == 0)
        {
            finishGame();
        }
    }

    private void finishGame()
    {
        mouse.SetActive(false);
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);
    }

    //Funcion para refrescar la interfaz con los parametros de este edificio.
    public void Display()
    {
        interfaz.GetComponent<Interfaz>().SwitchStatPanelBuilding(type, status, progress, Tile.name);
    }

    public Vector2 getPos()
    {
        return new Vector2(Tile.GetComponent<Tile>().x, Tile.GetComponent<Tile>().y);
    }
}
