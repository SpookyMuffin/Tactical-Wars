using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMov : MonoBehaviour
{

    public float speedLeftRight = 40;
    public float speedUpDown = 20;
    public float turnSpeed = 20;
    Vector3 initialRotation;
    Vector3 initialPosition;

    private void Start()
    {
        initialRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        initialPosition = new Vector3(20.84f, 30.06f, 34.11f);
    }
    // Update is called once per frame
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
       /* if (Input.GetAxis("Rotate") > 0)
        {
            this.transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Rotate") < 0)
        {
            this.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Reset") > 0)
        {
            this.transform.eulerAngles = initialRotation;
        }*/
    }
}
