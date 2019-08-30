using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interfaz : MonoBehaviour
{
    /* referencias a otros objetos */
    public GameObject GoldmarkText;
    public GameObject RacionesText;
    public GameObject CombustibleText;
    public GameObject infoPanel;
    public GameObject info1, info2, info3, info4, info5, info6;
    public GameObject WaitTime;
    public GameObject passButton, waitButton;
    public GameObject selectedObj = null;
    GameObject tempObj = null;
    public GameObject recursos;
    public Material selectedMat;
    Material tempMat;
    public GameObject audioData;
    public GameObject mapa;

    /* Actualiza el tiempo de espera en el panel correspondiente */
    public void waitTimeIA(float time)
    {
        WaitTime.GetComponent<Text>().text = "Current: " + time + "s";
    }

    /* Refresca el panel de recursos */
    public void RefreshResources(int G, int R, int C)
    {
        GoldmarkText.GetComponent<Text>().text = "GM: " + G;
        RacionesText.GetComponent<Text>().text = "Rations: " + R;
        CombustibleText.GetComponent<Text>().text = "Gas: " + C;

    }

    /* Actualiza el panel de información cuando se selecciona una unidad */
    public void SwitchStatPanelUnit(int type, int health, int steps, int power, bool action, bool Playable, bool feeded)
    {
        if (Playable)info1.GetComponent<Text>().text = "Faction: " + "Ally";
        else info1.GetComponent<Text>().text = "Faction: " + "Enemy";
        info2.GetComponent<Text>().text = "Health: " + health;
        info3.GetComponent<Text>().text = "Action: " + action;
        info4.GetComponent<Text>().text = "Feeded: " + feeded;
        info5.GetComponent<Text>().text = "Moves: " + steps;
        info6.GetComponent<Text>().text = "Action power: " + power;
       
        

    }

    /* Actualiza el panel de inforamción cuando se selecciona un edificio */
    public void SwitchStatPanelBuilding(int type, int status, int progress)
    {
        if(status == 0) info1.GetComponent<Text>().text = "Faction: " + "Neutral";
        else if(status == 1) info1.GetComponent<Text>().text = "Faction: " + "Ally";
        else if (status == 2) info1.GetComponent<Text>().text = "Faction: " + "Enemy";

       

        if (type == 0) info2.GetComponent<Text>().text = "Type: " + "Main base";
        else if (type == 2) info2.GetComponent<Text>().text = "Type: " + "Plumpjack";
        else if (type == 1) info2.GetComponent<Text>().text = "Type: " + "Camp";

        info3.GetComponent<Text>().text = "Conquer progress: " + progress;

        if (type == 0) info4.GetComponent<Text>().text = "Resource: " + "GM";
        else if (type == 2) info4.GetComponent<Text>().text = "Resource: " + "Gas";
        else if (type == 1) info4.GetComponent<Text>().text = "Resource: " + "Rations";

        if (type == 0) info5.GetComponent<Text>().text = "GM per turn: " + recursos.GetComponent<Resources>().goldmarkTurno;
        else if (type == 2) info5.GetComponent<Text>().text = "Gas per turn: " + recursos.GetComponent<Resources>().combustiblePump;
        else if (type == 1) info5.GetComponent<Text>().text = "Rations per turn: " + recursos.GetComponent<Resources>().racionesCamp;

        if (type != 0) info6.GetComponent<Text>().text = "GM per turn " + recursos.GetComponent<Resources>().goldmarkEdificio;
        else info6.GetComponent<Text>().text = "Important building!";
    }

    /* Esconde o muestra el botón de pasar turno */
    public void Pass(bool estado)
    {
        passButton.SetActive(estado);
        waitButton.SetActive(!estado);
    }

    /* Asigna un objeto seleccionado */
    public void setSelectedObj(GameObject obj)
    {
        bool suena = true;
        if (obj == selectedObj) suena = false;
        selectedObj = obj;
        if (selectedObj == null) return;
        if (tempObj != null)
        {
            if(tempObj.tag == "Unit")
            {
                
                tempObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material = tempMat;
                tempObj.transform.GetChild(0).transform.GetChild(1).GetComponent<Renderer>().material = tempMat;
                tempObj.transform.GetChild(0).transform.GetChild(2).GetComponent<Renderer>().material = tempMat;
                tempObj.transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = tempMat;
            }
            else
            {
                tempObj.GetComponent<Renderer>().material = tempMat;

            }
        }

        if (obj.tag == "Unit")
        {
            SwitchStatPanelUnit(obj.GetComponent<Unit>().type, obj.GetComponent<Unit>().health, obj.GetComponent<Unit>().steps, obj.GetComponent<Unit>().power, obj.GetComponent<Unit>().action,
                obj.GetComponent<Unit>().playable, obj.GetComponent<Unit>().feeded);
            tempMat = obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material;
            selectedObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material = selectedMat;
            selectedObj.transform.GetChild(0).transform.GetChild(1).GetComponent<Renderer>().material = selectedMat;
            selectedObj.transform.GetChild(0).transform.GetChild(2).GetComponent<Renderer>().material = selectedMat;
            selectedObj.transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = selectedMat;
        }
        else
        {
            if (suena)
            {
                audioData.transform.GetChild(6).gameObject.SetActive(false);
                audioData.transform.GetChild(6).gameObject.SetActive(true);
            }
            mapa.GetComponent<Map>().resetTiles();
            tempMat = obj.GetComponent<Renderer>().material;
            selectedObj.GetComponent<Renderer>().material = selectedMat;
            mapa.GetComponent<Map>().ColorTile(selectedObj.GetComponent<Building>().Tile.GetComponent<Tile>().x, selectedObj.GetComponent<Building>().Tile.GetComponent<Tile>().y, selectedObj.GetComponent<Building>().status);
        }
        tempObj = selectedObj;
        if(obj.tag == "Unit")
        {
            if (suena)
            {
                audioData.transform.GetChild(3).gameObject.SetActive(false);
                audioData.transform.GetChild(3).gameObject.SetActive(true);
            }
            int steps;
            int comb;
            if(obj.GetComponent<Unit>().playable == true) comb= recursos.GetComponent<Resources>().Combustible;
            else comb = recursos.GetComponent<Resources>().EnemyCombustible;
            int gasto = recursos.GetComponent<Resources>().gastoCombustibleTank;
            if ((comb / gasto) > obj.GetComponent<Unit>().steps) steps = obj.GetComponent<Unit>().steps;
            else steps = comb / gasto;
            mapa.GetComponent<Map>().resetTiles();
            mapa.GetComponent<Map>().ColorTiles(obj.GetComponent<Unit>().playable,steps, obj.GetComponent<Unit>().Tile.GetComponent<Tile>().x, obj.GetComponent<Unit>().Tile.GetComponent<Tile>().y, obj.GetComponent<Unit>().action);
        }

        

    }
}
