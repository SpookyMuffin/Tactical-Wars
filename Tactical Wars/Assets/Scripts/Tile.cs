using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    /* Indica si se puede posicionar o caminar 
     * sobre esta casilla */
    public bool notWalkable; 

    /* Posición X e Y en el tablero */
    public int x;
    public int y;

    /* Objeto que se encuentra sobre ella */
    public GameObject obj = null;

}