using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMov : MonoBehaviour
{

    public float speedLeftRight = 40;
    public float speedUpDown = 20;
    public float turnSpeed = 20;
    Vector3 initialRotation;
    private void Start()
    {
        initialRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }
    // Update is called once per frame
    void Update()
    {
    if(Input.GetAxis("Horizontal1") > 0)
    {
      transform.Translate(Vector3.right * speedLeftRight * Time.deltaTime);
     }
     if(Input.GetAxis("Horizontal1") < 0)
     {
        transform.Translate(Vector3.left * speedLeftRight * Time.deltaTime);
     }
     if(Input.GetAxis("Horizontal2") < 0)
     {
      transform.Translate(Vector3.back * speedLeftRight * Time.deltaTime);
     }
     if(Input.GetAxis("Horizontal2") > 0)
     {
      transform.Translate(Vector3.forward * speedLeftRight * Time.deltaTime);
     }
    if(Input.GetAxis("Vertical") > 0)
     {
      transform.Translate(Vector3.up * speedUpDown * Time.deltaTime);
     }
     if(Input.GetAxis("Vertical") < 0)
     {
      transform.Translate(Vector3.down * speedUpDown * Time.deltaTime);
     }
     if(Input.GetAxis("Rotate") > 0)
     {
        this.transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime);
     }

      if(Input.GetAxis("Rotate") < 0)
     {
        this.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
     }
        if (Input.GetAxis("Reset") > 0)
        {
            this.transform.eulerAngles = initialRotation;
        }
    }
}
