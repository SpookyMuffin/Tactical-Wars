using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnClick : MonoBehaviour
{
    /* Función que sale de la aplicación
     * o del modo jugar del editor */
    public void Quit(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
       Application.Quit();
    #endif
    }
}

