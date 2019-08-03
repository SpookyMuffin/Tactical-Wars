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
        mapa.GetComponent<Map>().resetTiles();
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

            tempMat = obj.GetComponent<Renderer>().material;
            selectedObj.GetComponent<Renderer>().material = selectedMat;
        }
        tempObj = selectedObj;
        if(obj.tag == "Unit")
        {
            mapa.GetComponent<Map>().resetTiles();
            mapa.GetComponent<Map>().ColorTiles(obj.GetComponent<Unit>().playable, obj.GetComponent<Unit>().steps, obj.GetComponent<Unit>().Tile.GetComponent<Tile>().x, obj.GetComponent<Unit>().Tile.GetComponent<Tile>().y);
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
