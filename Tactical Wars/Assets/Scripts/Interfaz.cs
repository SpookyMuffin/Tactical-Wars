using System.Collections;
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

    public GameObject mapa;

    void Awake()
    {


    }

    public void RefreshResources(int G, int R, int C)
    {
        GoldmarkText.GetComponent<Text>().text = "Goldmarks: " + G;
        RacionesText.GetComponent<Text>().text = "Raciones: " + R;
        CombustibleText.GetComponent<Text>().text = "Combustible: " + C;

    }

    public void SwitchStatPanelUnit(int type, int health, int steps, int power, bool action, string Casilla)
    {

        info1.GetComponent<Text>().text = " Tipo de unidad " + type;
        info2.GetComponent<Text>().text = "Vida " + health;
        info3.GetComponent<Text>().text = "Pasos " + steps;
        info4.GetComponent<Text>().text = "Poder " + power;
        info5.GetComponent<Text>().text = "Accion? " + action;
        info6.GetComponent<Text>().text = "Casilla " + Casilla;

    }

    public void SwitchStatPanelBuilding(int type, int status, int progress, string casilla)
    {
        info1.GetComponent<Text>().text = "status: " + status;
        info2.GetComponent<Text>().text = "progress: " + progress;
        info3.GetComponent<Text>().text = "type: " + type;
        info4.GetComponent<Text>().text = "casilla: " + casilla;
        info5.GetComponent<Text>().text = "GM: " + 20;
        info6.GetComponent<Text>().text = "Recurso " + 20;
    }

    public void Pass(bool estado)
    {
        passButton.SetActive(estado);
        waitButton.SetActive(!estado);
    }

    public void setSelectedObj(GameObject obj)
    {
        
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
            mapa.GetComponent<Map>().resetTiles();
            tempMat = obj.GetComponent<Renderer>().material;
            selectedObj.GetComponent<Renderer>().material = selectedMat;
            mapa.GetComponent<Map>().ColorTile(selectedObj.GetComponent<Building>().Tile.GetComponent<Tile>().x, selectedObj.GetComponent<Building>().Tile.GetComponent<Tile>().y, selectedObj.GetComponent<Building>().status);
        }
        tempObj = selectedObj;
        if(obj.tag == "Unit")
        {
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
