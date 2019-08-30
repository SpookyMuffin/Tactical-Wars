using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    /* Casilla en la que se encuentra el edificio */
    public GameObject Tile;

    /* Estado del edificion
     * Neutral = 0
     * Conquistado por el jugador = 1
     * Conquistado por el enemigo = 2 */
    public int status = 0;

    /* Progreso de conquista de un edificio
     * progress = 100 edificio pasa a ser aliado
     * progress = -100 pasa a ser enemigo */
    public int progress = 0;

    /* Tipo de edificio
     * Base principal = 0
     * Campamento = 1
     * Bomba de petroleo = 2 */
    public int type = 99; 

    /* Referencias a otros objetos */
    public GameObject interfaz;
    public GameObject endpanel;
    public GameObject menu;

    /* Función para conquistar un edificio */

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
    /* Función para asignar bando a un edificio */
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
    /* Función que determina si se ha acabado la partida
     * por conquista de la base principal de algun bando */
    private void finishGame()
    {
        menu.SetActive(false);
        endpanel.SetActive(true);

}

    /*Funcion para refrescar la interfaz con los parametros de este edificio */
    public void Display()
    {
        interfaz.GetComponent<Interfaz>().SwitchStatPanelBuilding(type, status, progress);
        interfaz.GetComponent<Interfaz>().setSelectedObj(this.gameObject);

    }
    /* Devuelve un vector2 con las coordenadas x e y de la casilla en la que se encuentra */
    public Vector2 getPos()
    {
        return new Vector2(Tile.GetComponent<Tile>().x, Tile.GetComponent<Tile>().y);
    }
}
