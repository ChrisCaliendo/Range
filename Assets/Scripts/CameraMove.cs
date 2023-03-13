using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float mainSpeed = 100.0f; //regular speed
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun= 1.0f;
     
    private void FixedUpdate() 
    {
        if (Input.GetKey (KeyCode.W)){
            transform.position += new Vector3(0, 1 , 0);
        }
        if (Input.GetKey (KeyCode.S)){
            transform.position += new Vector3(0, -1, 0);
        }
        if (Input.GetKey (KeyCode.A)){
            transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D)){
            transform.position += new Vector3(1, 0, 0);
        }
    }
}
