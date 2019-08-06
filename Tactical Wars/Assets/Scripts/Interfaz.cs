﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interfaz : MonoBehaviour
{
    public GameObject GoldmarkText;
    public GameObject RacionesText;
    public GameObject CombustibleText;

    public GameObject infoPanel;
    public GameObject info1, info2, info3, info4, info5, info6;

    public GameObject passButton, waitButton;

    public GameObject selectedObj = null;
    GameObject tempObj = null;
    public GameObject recursos;

    public Material selectedMat;
    Material tempMat;
    public GameObject audioData;

    public GameObject mapa;

    void Awake()
    {


    }

    public void RefreshResources(int G, int R, int C)
    {
        GoldmarkText.GetComponent<Text>().text = "GM: " + G;
        RacionesText.GetComponent<Text>().text = "Rations: " + R;
        CombustibleText.GetComponent<Text>().text = "Gas: " + C;

    }

    public void SwitchStatPanelUnit(int type, int health, int steps, int power, bool action, bool Playable)
    {
        if (Playable)info1.GetComponent<Text>().text = "Faction: " + "Ally";
        else info1.GetComponent<Text>().text = "Faction: " + "Enemy";
        info2.GetComponent<Text>().text = "Unit: " + "Tank";
        info3.GetComponent<Text>().text = "Health: " + health;
        info4.GetComponent<Text>().text = "Action: " + action;
        info5.GetComponent<Text>().text = "Moves: " + steps;
        info6.GetComponent<Text>().text = "Action power: " + power;
       
        

    }

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

    public void Pass(bool estado)
    {
        passButton.SetActive(estado);
        waitButton.SetActive(!estado);
    }

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
            Debug.Log("selecciono");
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
    
    void pass()
    {
        selectedObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material = tempMat;
        selectedObj.transform.GetChild(0).transform.GetChild(1).GetComponent<Renderer>().material = tempMat;
        selectedObj.transform.GetChild(0).transform.GetChild(2).GetComponent<Renderer>().material = tempMat;
        selectedObj.transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().material = tempMat;

        selectedObj = null;
        tempObj = null;
        tempMat = null;
        selectedMat = null;
    }

}
