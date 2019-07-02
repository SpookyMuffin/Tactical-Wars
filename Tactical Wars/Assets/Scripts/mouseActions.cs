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
    bool CompClick2 = false;
    GameObject click1,click2;



    void Update()
    {
        //Comprobamos que sea nuestro turno
        if (turnManager.GetComponent<Turns>().turn)
        {
            //Esperamos a que se presione el click izquierdo y almacenamos en hit1 lo que se ha clickado
            if (Input.GetMouseButtonDown(0))
            {
                CompClick2 = true;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit1, 100)
                    && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (hit1.collider != null) click1 = hit1.collider.gameObject;

                    //Comprobamos que se ha seleccionado y actualizamos la interfaz con sus propiedades
                    Debug.Log("Click Izq: " + hit1.collider);
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
            if (Input.GetMouseButtonDown(1) && CompClick2)
            {
                //Pasamos a clickar el segundo objeto con el click derecho
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit2, 100) &&
                    !EventSystem.current.IsPointerOverGameObject())
                {
                    if(hit2.collider != null) click2 = hit2.collider.gameObject;
                    Debug.Log("Click Derecho: " + hit2.collider);
                    //Combate TODO
                    //Si lo que seleccionamos con el click derecho es una unidad, haremos la accion de combatir
                    if (click2.gameObject.tag == "Unit")
                    {
                        //Si es una unidad aliada a una enemiga atacara
                        //Si es una infanteria o opuede que meta un ingeniero puede que repare el tank si el objetivo es aliado

                        click1.GetComponent<Unit>().Attack(click2);
                        click2.GetComponent<Unit>().Display();
                    }

                    //Conquista TODO
                    //Si lo segundo que seleccionamos es un edificio procederemos a la conquista si es posible
                    if (click2.tag == "Building" &&click1.gameObject.tag == "Unit" 
                        && click1.GetComponent<Unit>().playable == true)
                    {
                        click1.GetComponent<Unit>().Conquer(click2, PlayerMat);
                        click2.GetComponent<Building>().Display();
                    }

                    //Movimiento TODO
                    //Si lo segundo que seleccionamos es una casilla, haremos el proceso de movernos
                    //siempre que sea una posicion permitida
                    if (click1.tag == "Unit")
                    {
                        if(click2.tag == "Tile" && click1.GetComponent<Unit>().playable == true)
                        {
                            resourceManager.GetComponent<Resources>().MoverTank(0);
                            click1.GetComponent<Unit>().Move(click2);
                            click1.GetComponent<Unit>().Display();
                        }

                    }
                }
            }
        }
    }

    //Funcion que calcula la distancia euclidea entre unidades, casillas, edificios,etc
    float dEculidea(RaycastHit origen, RaycastHit destino){
        Vector2 o;
        Vector2 d;
        o.x = 999;
        o.y = 999;
        d.x = 999;
        d.y = 999;

        if (hit1.collider.gameObject.tag == "Unit")
        {
            o.x = hit1.collider.gameObject.GetComponent<Unit>().Tile.GetComponent<Tile>().x;
            o.y = hit1.collider.gameObject.GetComponent<Unit>().Tile.GetComponent<Tile>().y;
        }
        else if (hit1.collider.gameObject.tag == "Building")
        {
            o.x = hit1.collider.gameObject.GetComponent<Building>().Tile.GetComponent<Tile>().x;
            o.y = hit1.collider.gameObject.GetComponent<Building>().Tile.GetComponent<Tile>().y;
        }
        if(hit2.collider.gameObject.tag == "Tile"){
            d.x = hit2.collider.gameObject.GetComponent< Tile >().x;
            d.y = hit2.collider.gameObject.GetComponent< Tile >().y;
        }
        if (hit2.collider.gameObject.tag == "Unit")
        {
            d.x = hit1.collider.gameObject.GetComponent<Unit>().Tile.GetComponent<Tile>().x;
            d.y = hit1.collider.gameObject.GetComponent<Unit>().Tile.GetComponent<Tile>().y;
        }


        float x1,x2,y1,y2;
        float distancia = 2f;

        x1 = (float)o.x;
        x2 = (float)d.x;
        y1 = (float)o.y;
        y2 = (float)d.y;

        distancia = Mathf.Sqrt(Mathf.Pow(x2 - x1 , 2f) + Mathf.Pow(y2 - y1, 2f));
        Debug.Log(distancia);
        return distancia;
    }

}
