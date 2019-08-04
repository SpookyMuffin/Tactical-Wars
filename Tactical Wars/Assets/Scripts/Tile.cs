using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool notWalkable; //Si se puede mover a esta casilla

    //Posicion en la matriz
    public int x;
    public int y;

    //Objeto que tiene encima
    public GameObject obj = null;

}