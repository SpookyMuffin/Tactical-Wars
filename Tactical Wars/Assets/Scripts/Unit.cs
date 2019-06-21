using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int tankSteps = 3;

    //Caracteristicas de la unidad
    public int type; //Tipo de la unidad 0 = tank, 1  = infanteria
    public int health = 100; //Vida de la unidad
    public int steps = 3; //Numero de movimientos que puede realizar
    public int power = 50; //Daño que inflnge nuestra unidad

    //Si le quedan acciones y si el jugador la puede controlar
    public bool action = true; //Si puede atacar o conquistar
    public bool playable = true; //Si la podemos controlar



    //Casilla en la que se encuentra
    public GameObject Tile; 

    //Usados para la interfaz
    GameObject info1, info2, info3, info4, info5; 

    void Start()
    {
        //Recopilamos para actualizar la interfaz
        info1 = GameObject.FindGameObjectWithTag("info1");
        info2 = GameObject.FindGameObjectWithTag("info2");
        info3 = GameObject.FindGameObjectWithTag("info3");
        info4 = GameObject.FindGameObjectWithTag("info4");
        info5 = GameObject.FindGameObjectWithTag("info5");

    }

    //Ataca a otra unidad
    public void Attack(GameObject Target)
    {
        double distancia = 99;
        int x1, y1, x2, y2;
        //Si es ya esta conquistado, impedimos conquista
        if (action == false) return;
        if (playable == true && Target.GetComponent<Unit>().playable == true) return;
        if (playable == false && Target.GetComponent<Unit>().playable == false) return;

        x1 = this.Tile.GetComponent<Tile>().x;
        y1 = this.Tile.GetComponent<Tile>().y;
        x2 = Target.GetComponent<Unit>().Tile.GetComponent<Tile>().x;
        y2 = Target.GetComponent<Unit>().Tile.GetComponent<Tile>().y;
        distancia = Mathf.Sqrt(Mathf.Pow(x2 - x1, 2f) + Mathf.Pow(y2 - y1, 2f));
        if (distancia > 1 || action == false) return;
        
        Target.GetComponent<Unit>().health -= power;
        if (Target.GetComponent<Unit>().health < 1) Destroy(Target);
        action = false;
        RotateUnit(x1, y1, x2, y2);

    }

    public void Conquer(GameObject Target, Material mat)
    {
        double distancia = 99;
        int x1, y1, x2, y2;
        //Si es ya esta conquistado, impedimos conquista
        if (action == false) return;
        if (playable == true && Target.GetComponent<Building>().status == 1) return;
        if (playable == false && Target.GetComponent<Building>().status == 1) return;

        x1 = this.Tile.GetComponent<Tile>().x;
        y1 = this.Tile.GetComponent<Tile>().y;
        x2 = Target.GetComponent<Building>().Tile.GetComponent<Tile>().x;
        y2 = Target.GetComponent<Building>().Tile.GetComponent<Tile>().y;
        distancia = Mathf.Sqrt(Mathf.Pow(x2 - x1, 2f) + Mathf.Pow(y2 - y1, 2f));
        if (distancia > 1 || action == false) return;

        if (playable == true) Target.gameObject.GetComponent<Building>().Conquer(0,mat);
        else Target.gameObject.GetComponent<Building>().Conquer(1,mat);
        action = false;
        RotateUnit(x1, y1, x2, y2);

    }

    //Mover a una casilla adyacente TODO
    public void Move(GameObject Tile)
    {
        Debug.Log("me ejecuto");
        double distancia = 1000f;
        int x1, y1, x2, y2;
        x1 = this.Tile.GetComponent<Tile>().x;
        y1 = this.Tile.GetComponent<Tile>().y;
        x2 = Tile.GetComponent<Tile>().x;
        y2 = Tile.GetComponent<Tile>().y;
        distancia = Mathf.Sqrt(Mathf.Pow(x2 - x1, 2f) + Mathf.Pow(y2 - y1, 2f));
        if ( distancia > 1 || steps == 0) return;

        //Liberamos la anterior casilla
        this.Tile.GetComponent<Tile>().obj = null;
        this.Tile.GetComponent<Tile>().notWalkable = false;

        //Asignamos y bloqueamos la nueva
        this.transform.position = Tile.transform.position;
        this.Tile = Tile;
        Tile.GetComponent<Tile>().obj = this.gameObject;
        Tile.GetComponent<Tile>().notWalkable = true;

        //Rotamos la unida para que no vaya haciendo Deja vu
        RotateUnit(x1, y1, x2, y2);
        //Restamos los movimientos y refrescamos la interfaz
        steps--;
                            
    }

    //Para refrescar al final de turnos los movimientos de cada unidad
    public void RefreshSteps()
    {
        if (type == 0) steps = tankSteps;
    }

    //Usada para actualizar la interfaz con los datos de esta unidad
    public void Display()
    {
        info1.GetComponent<Text>().text = "Health: " + health;
        info2.GetComponent<Text>().text = "movimientos: " + steps;
        info3.GetComponent<Text>().text = "Ataque: " + action;
        info4.GetComponent<Text>().text = "casilla: " + Tile.name;
        info5.GetComponent<Text>().text = "Unidad: " + type;
    }

    void RotateUnit(int x1, int y1, int x2, int y2)
    {
        if (y2 > y1)
        {
            this.transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if(y2 < y1)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else if (x2 > x1)
        {
            this.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if(x2 < x1)
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
}