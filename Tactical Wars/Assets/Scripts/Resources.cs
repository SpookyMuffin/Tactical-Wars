using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    //Recursos del jugador
    public int Goldmarks = 100; //Moneda del juego
    public int Raciones = 100; //Token que utiliza la infanteria en cada turno
    public int Combustible = 100; //Token que utiliza el tank por cada movimiento

    //Mismo que las variables anterior pero para el enemigo
    public int EnemyGoldmarks = 100;
    public int EnemyRaciones = 100;
    public int EnemyCombustible = 100;

    //Precio y coste de unidades y acciones
    public int PrecioTank = 20; //Precio en monedas que cuesta cada tanque
    public int gastoCombustibleTank = 5; //Combustible usado por cada movimiento de los tanques
    public int combustiblePump = 5; //Petroleo ganado cada turno por los edificios que generar este recurso
    public int racionesCamp = 10; //Raciones ganadas cada turno por los edificios que generan este recurso
    public int goldmarkEdificio = 1; //Monedas generadas por cada edificio al final de turno
    public int goldmarkTurno = 1; //Monedas por defecto ganada cada turno

    //Usados para actualizar la interfaz
    public GameObject interfaz;

    //Usado para los edificios
    public GameObject buildingManager;

    private void Start()
    {
        interfaz.GetComponent<Interfaz>().RefreshResources(Goldmarks,Raciones,Combustible);
    }

    //Resta el combustible usado por mover una casilla un tanque
    //Who == 0 jugador
    //Who == 1 IA
    public void MoverTank(int who)
    {
        if (who == 0)
        {
            Combustible = Combustible - gastoCombustibleTank;
            interfaz.GetComponent<Interfaz>().RefreshResources(Goldmarks, Raciones, Combustible);
        }
        else if( who == 1)
        {
            EnemyCombustible -= gastoCombustibleTank;
        }
        
    }

    //Resta goldmarks por generar una unidad tipo tank
    public bool GenerarTank(int who)
    {
        if (who == 0 && Goldmarks >= PrecioTank)
        {
            Goldmarks -= PrecioTank;
            interfaz.GetComponent<Interfaz>().RefreshResources(Goldmarks, Raciones, Combustible);
            return true;
        }
        else if(who == 1 && EnemyGoldmarks >= PrecioTank)
        {
            EnemyGoldmarks -= PrecioTank;
            return true;
        }

        return false;

    }

    //Añade el combustible por acabar el turno por cada edificio pumps que controlemos
    public void EndTurnPump(int who)
    {
        if(who == 0)
        {
            int pumps = buildingManager.GetComponent<buildingManager>().AllyPumps();
            Goldmarks = Goldmarks + (goldmarkEdificio * pumps);
            Combustible = Combustible + (combustiblePump * pumps);
        }
        else if(who == 1)
        {
            int pumps = buildingManager.GetComponent<buildingManager>().EnemyPumps();
            EnemyGoldmarks = EnemyGoldmarks + (goldmarkEdificio * pumps);
            EnemyCombustible = EnemyCombustible + (combustiblePump * pumps);
        }
    }

    //Añade las raciones y el goldmark generado por los edificios Camp que controlemos
    public void EndTurnCamp(int who)
    {
        if (who == 0)
        {
            int camps = buildingManager.GetComponent<buildingManager>().AllyCamps();
            Goldmarks = Goldmarks + (goldmarkEdificio * camps);
            Raciones = Raciones + (racionesCamp * camps);
        }
        else if (who == 1)
        {
            int camps = buildingManager.GetComponent<buildingManager>().EnemyCamps();
            EnemyGoldmarks = EnemyGoldmarks + (goldmarkEdificio * camps);
            EnemyRaciones = EnemyRaciones + (racionesCamp * camps);
        }

    }

    //Añadimos y actualizamos los recursos por acabar la ronda
    public void EndTurnResources(int who)
    {
        if(who == 0)
        {
            EndTurnPump(0);
            EndTurnCamp(0);
            Goldmarks += goldmarkTurno;
            interfaz.GetComponent<Interfaz>().RefreshResources(Goldmarks, Raciones, Combustible);
        }
        else if (who == 1)
        {
            EndTurnPump(1);
            EndTurnCamp(1);
            EnemyGoldmarks += goldmarkTurno;
        }

    }


}
