using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mouseActions : MonoBehaviour
{
    //Raycast
    RaycastHit hit1; //Informacion del rayo que seleccionaremos con el click izquierdo
    RaycastHit hit2; //Informacion del rayo que seleccionaremos con el click derecho

    //Gestores de turno y recursos
    public GameObject resourceManager; //Informacion de los recursos y sus funciones
    public GameObject turnManager; //Informacion del turno y sus funciones

    //Material aliado
    public Material PlayerMat;
    //Mapa
    public GameObject map;
    //Comprobador
    bool CompClick2 = false, CompClick1 = false;
    GameObject click1,click2;
    public GameObject interfaz;



    void Update()
    {
        //Comprobamos que sea nuestro turno
        if (turnManager.GetComponent<Turns>().turn)
        {
            //Esperamos a que se presione el click izquierdo y almacenamos en hit1 lo que se ha clickado
            if (Input.GetAxis("Click1") > 0)
            {
                CompClick1 = true;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit1, 100)
                    && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (hit1.collider != null) click1 = hit1.collider.gameObject;

                    //Comprobamos que se ha seleccionado y actualizamos la interfaz con sus propiedades
                   // Debug.Log("Click Izq: " + hit1.collider);
                    if (click1.tag == "Unit")
                    {
                        click1.GetComponent<Unit>().Display();
                    }
                    if (click1.tag == "Building")
                    {
                       click1.GetComponent<Building>().Display();
                    }


                }
            }
            if (Input.GetAxis("Click2") > 0) CompClick2 = true;
            if (CompClick1 && CompClick2)
            {
                //Pasamos a clickar el segundo objeto con el click derecho
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit2, 100) &&
                    !EventSystem.current.IsPointerOverGameObject())
                {
                    if(hit2.collider != null) click2 = hit2.collider.gameObject;
                    // Debug.Log("Click Derecho: " + hit2.collider);
                    //Combate
                    //Si lo que seleccionamos con el click derecho es una unidad, haremos la accion de combatir
                    if (click2.gameObject.tag == "Unit" && click1.gameObject.GetComponent<Unit>().playable == true)
                    {
                        //Si es una unidad aliada a una enemiga atacara
                        //Si es una infanteria o opuede que meta un ingeniero puede que repare el tank si el objetivo es aliado

                        click1.GetComponent<Unit>().Attack(click2);
                        click1.GetComponent<Unit>().Display();
                    }

                    //Conquista
                    //Si lo segundo que seleccionamos es un edificio procederemos a la conquista si es posible
                    if (click2.tag == "Building" &&click1.gameObject.tag == "Unit" 
                        && click1.GetComponent<Unit>().playable == true)
                    {
                        click1.GetComponent<Unit>().Conquer(click2, PlayerMat);
                        click1.GetComponent<Unit>().Display();
                    }

                    //Movimiento
                    //Si lo segundo que seleccionamos es una casilla, haremos el proceso de movernos
                    //siempre que sea una posicion permitida
                    if (click1.tag == "Unit")
                    {
                        if(click2.tag == "Tile" && click1.GetComponent<Unit>().playable == true)
                        {
                            click1.GetComponent<Unit>().Move(click2);
                            click1.GetComponent<Unit>().Display();
                        }

                    }
                }
                //CompClick1 = false;
                CompClick2 = false;
            }
        }
        else
        {
            if (Input.GetAxis("Click1") > 0)
            {
                CompClick2 = true;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit1, 100)
                    && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (hit1.collider != null) click1 = hit1.collider.gameObject;

                    //Comprobamos que se ha seleccionado y actualizamos la interfaz con sus propiedades
                    // Debug.Log("Click Izq: " + hit1.collider);
                    if (click1.tag == "Unit")
                    {
                        click1.GetComponent<Unit>().Display();
                    }
                    if (click1.tag == "Building")
                    {
                        click1.GetComponent<Building>().Display();
                    }


                }
            }
        }
    }
}
