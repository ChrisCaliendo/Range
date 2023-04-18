using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    float mainSpeed = 100.0f; //regular speed
    //private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun= 1.0f;
     
    private void FixedUpdate() 
    {
        Vector3 x = transform.position;
        int y = x.x;
    }
}
