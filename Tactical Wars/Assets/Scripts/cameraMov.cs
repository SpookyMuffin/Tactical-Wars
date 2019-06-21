using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMov : MonoBehaviour
{

    public float speedLeftRight = 40;
    public float speedUpDown = 20;
    public float turnSpeed = 20; 

    // Update is called once per frame
    void Update()
    {
    if(Input.GetKey(KeyCode.D))
    {
      transform.Translate(Vector3.right * speedLeftRight * Time.deltaTime);
     }
     if(Input.GetKey(KeyCode.A))
     {
        transform.Translate(Vector3.left * speedLeftRight * Time.deltaTime);
     }
     if(Input.GetKey(KeyCode.S))
     {
      transform.Translate(Vector3.back * speedLeftRight * Time.deltaTime);
     }
     if(Input.GetKey(KeyCode.W))
     {
      transform.Translate(Vector3.forward * speedLeftRight * Time.deltaTime);
     }
    if(Input.GetKey(KeyCode.Space))
     {
      transform.Translate(Vector3.up * speedUpDown * Time.deltaTime);
     }
     if(Input.GetKey(KeyCode.LeftShift))
     {
      transform.Translate(Vector3.down * speedUpDown * Time.deltaTime);
     }
     if(Input.GetKey(KeyCode.Q))
     {
        this.transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime);
     }

      if(Input.GetKey(KeyCode.E))
     {
        this.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
     }
    }
}
