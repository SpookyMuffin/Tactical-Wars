using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMov : MonoBehaviour
{
    /* Velocidades en los ejes */
    public float speedLeftRight = 40;
    public float speedUpDown = 20;

    /* Posición inicial de la camara */
    Vector3 initialPosition;

    /* Inicializa las posición y rotación */
    private void Start()
    {
        initialPosition = new Vector3(20.84f, 30.06f, 34.11f);
    }
    /*  En cada frame se detecta si se esta pulsado una tecla de moviento de la camara y mueve esta
     *  dentro de unos limites */
    void Update()
    {
        if (Input.GetAxis("Horizontal1") > 0 && this.transform.position.z < (initialPosition.z + 40))
        {
            transform.Translate(Vector3.right * speedLeftRight * Time.deltaTime);
        }
        if (Input.GetAxis("Horizontal1") < 0 && this.transform.position.z > (initialPosition.z - 40))
        {
            transform.Translate(Vector3.left * speedLeftRight * Time.deltaTime);
        }
        if (Input.GetAxis("Horizontal2") < 0 && this.transform.position.x < (initialPosition.x + 15))
        {
            transform.Translate(Vector3.back * speedLeftRight * Time.deltaTime);
        }
        if (Input.GetAxis("Horizontal2") > 0 && this.transform.position.x > (initialPosition.x - 60))
        {
            transform.Translate(Vector3.forward * speedLeftRight * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") > 0 && this.transform.position.y < (initialPosition.y + 10))
        {
            transform.Translate(Vector3.up * speedUpDown * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") < 0 && this.transform.position.y > (initialPosition.y - 20))
        {
            transform.Translate(Vector3.down * speedUpDown * Time.deltaTime);
        }
    }
}
