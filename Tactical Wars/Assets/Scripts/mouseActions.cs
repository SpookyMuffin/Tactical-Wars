using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mouseActions : MonoBehaviour
{
    /* Objetos de tipo raycast que almacenan ambos clics */
    RaycastHit hit1; 
    RaycastHit hit2;

    /* Referencias a otros objetos */
    public GameObject interfaz;
    public GameObject resourceManager; 
    public GameObject turnManager; 
    public GameObject map;

    /* Comprueban si se ha hecho clic */
    bool CompClick2 = false, CompClick1 = false;

    /* Almacenan el gameobject unido al collider del raycast */
    GameObject click1,click2;

    /* Material utilizado por el jugador */
    public Material PlayerMat;

    /* Funcion que se ejecuta cada frame, dependiendo del turno registra
     * el clic derecho e izquierdo o solo el izquiero, en caso de turno del jugador, 
     * se selecciona la unidad con el clic izquierdo refrescando la interfaz y en el
     * momento que el jugador pulse clic derecho en un objeto con posible acción esta 
     * se ejecutará. En caso de no ser su turno, solo ser registrará el clic izquiero,
     * permitiendo solamente seleccionar unidades */
    void Update()
    {
        if (turnManager.GetComponent<Turns>().turn)
        {
            if (Input.GetAxis("Click1") > 0)
            {
                CompClick1 = true;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit1, 100)
                    && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (hit1.collider != null && hit1.collider.gameObject.tag != "Tile") click1 = hit1.collider.gameObject;

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
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit2, 100) &&
                    !EventSystem.current.IsPointerOverGameObject())
                {
                    if(hit2.collider != null) click2 = hit2.collider.gameObject;
                    if (click2.gameObject.tag == "Unit" && click1.gameObject.GetComponent<Unit>().playable == true)
                    {
                        click1.GetComponent<Unit>().Attack(click2);
                        click1.GetComponent<Unit>().Display();
                    }

                    if (click2.tag == "Building" &&click1.gameObject.tag == "Unit" 
                        && click1.GetComponent<Unit>().playable == true)
                    {
                        click1.GetComponent<Unit>().Conquer(click2, PlayerMat);
                        click1.GetComponent<Unit>().Display();
                    }

                    if (click1.tag == "Unit")
                    {
                        if(click2.tag == "Tile" && click1.GetComponent<Unit>().playable == true)
                        {
                            click1.GetComponent<Unit>().Move(click2);
                            click1.GetComponent<Unit>().Display();
                        }

                    }
                }
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
