using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    //Caracteristicas de la unidad
    public int type; //Tipo de la unidad 0 = tank, 1  = infanteria
    public int health = 100; //Vida de la unidad
    public int steps = 3; //Numero de movimientos que puede realizar
    public int power = 50; //Daño que inflnge nuestra unidad

    //Si le quedan acciones y si el jugador la puede controlar
    public bool action = true; //Si puede atacar o conquistar
    public bool playable = true; //Si la podemos controlar

    int TankPower = 50;
    int TankSteps = 3;


    //Casilla en la que se encuentra
    public GameObject Tile;

    //Usados para la interfaz
    public GameObject UI;

    public GameObject resourceManager;


    public bool initialiteUnit(GameObject spawnTile)
    {
        if (spawnTile.GetComponent<Tile>().notWalkable == true) return false;
        else if (type == 0)
        {
            health = 100;
            steps = 0;
            power = TankPower;
            action = false;
            Tile = spawnTile;
            spawnTile.GetComponent<Tile>().obj = this.gameObject;
            spawnTile.GetComponent<Tile>().notWalkable = true;
            this.gameObject.transform.position = spawnTile.transform.position;
            return true;
        }
        return false;
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

        if (Target.GetComponent<Unit>().health < 1)
        {
            Target.GetComponent<Unit>().Tile.GetComponent<Tile>().notWalkable = false;
            Target.GetComponent<Unit>().Tile.GetComponent<Tile>().obj = null;
            Destroy(Target);
        }
        action = false;
        steps = 0;
        RotateUnit(x1, y1, x2, y2);

    }

    //Funcion para conquistar un edificio.
    //Recibe el gameobject que debe ser un edificio
    //Material con el que se reemplazara el actual al conquistar.
    public void Conquer(GameObject Target, Material mat)
    {
        double distancia = 99;
        int x1, y1, x2, y2;
        //Si es ya esta conquistado, impedimos conquista
        if (action == false) return;
        if (playable == true && Target.GetComponent<Building>().status == 1) return;
        if (playable == false && Target.GetComponent<Building>().status == 2) return;

        x1 = this.Tile.GetComponent<Tile>().x;
        y1 = this.Tile.GetComponent<Tile>().y;
        x2 = Target.GetComponent<Building>().Tile.GetComponent<Tile>().x;
        y2 = Target.GetComponent<Building>().Tile.GetComponent<Tile>().y;
        distancia = Mathf.Sqrt(Mathf.Pow(x2 - x1, 2f) + Mathf.Pow(y2 - y1, 2f));
        if (distancia > 1) return;

        if (playable == true)
        {
            Target.gameObject.GetComponent<Building>().Conquer(0, mat);
        }
        else
        {
            Target.gameObject.GetComponent<Building>().Conquer(1, mat);

        }
        action = false;
        steps = 0;
        RotateUnit(x1, y1, x2, y2);

    }

    //Mover a una casilla adyacente
    public void Move(GameObject Tile)
    {
        if (playable == true && resourceManager.GetComponent<Resources>().Combustible < resourceManager.GetComponent<Resources>().gastoCombustibleTank)
        {
            return;
        }
        else if (playable == false && resourceManager.GetComponent<Resources>().EnemyCombustible < resourceManager.GetComponent<Resources>().gastoCombustibleTank)
        {
            return;

        }
        double distancia = 1000f;
        int x1, y1, x2, y2;
        x1 = this.Tile.GetComponent<Tile>().x;
        y1 = this.Tile.GetComponent<Tile>().y;
        x2 = Tile.GetComponent<Tile>().x;
        y2 = Tile.GetComponent<Tile>().y;
        distancia = Mathf.Sqrt(Mathf.Pow(x2 - x1, 2f) + Mathf.Pow(y2 - y1, 2f));
        if ( distancia > 1 || steps == 0 || Tile.GetComponent<Tile>().notWalkable == true) return;

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
        if (playable == true)
        {
            resourceManager.GetComponent<Resources>().MoverTank(0);
        }
        else if (playable == false)
        {
            resourceManager.GetComponent<Resources>().MoverTank(1);

        }

    }

    //Para refrescar al final de turnos los movimientos de cada unidad
    public void RefreshSteps()
    {
        if (type == 0) steps = TankSteps;
        action = true;
    }

    //Usada para actualizar la interfaz con los datos de esta unidad
    public void Display()
    {
        UI.GetComponent<UI>().SwitchStatPanelUnit(type,health,steps,power,action,Tile.name);
    }

    //Funcion para rotar la unidad
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

    public Vector2 getPos()
    {
        return new Vector2(Tile.GetComponent<Tile>().x, Tile.GetComponent<Tile>().y);
    }
}