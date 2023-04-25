using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpotMovement : MonoBehaviour
{
    public Transform fishingSpot;
    public Transform player;
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = player.position;
        if (currentPosition.x > previousPosition.x && currentPosition.y > previousPosition.y)
        {
            transform.localPosition = new Vector3(1, 1, 0);
        }
        else if (currentPosition.x > previousPosition.x && currentPosition.y < previousPosition.y)
        {
            transform.localPosition = new Vector3(1, -1, 0);
        }
        else if (currentPosition.x < previousPosition.x && currentPosition.y > previousPosition.y)
        {
            transform.localPosition = new Vector3(-1, 1, 0);
        }
        else if (currentPosition.x < previousPosition.x && currentPosition.y < previousPosition.y)
        {
            transform.localPosition = new Vector3(-1, -1, 0);
        }
        else if (currentPosition.x > previousPosition.x)
        {
            transform.localPosition = new Vector3(1, 0, 0);
        }
        else if (currentPosition.x < previousPosition.x)
        {
            transform.localPosition = new Vector3(-1, 0, 0);
        }
        else if (currentPosition.y > previousPosition.y)
        {
            transform.localPosition = new Vector3(0, 1, 0);
        }
        else if (currentPosition.y < previousPosition.y)
        {
            transform.localPosition = new Vector3(0, -1, 0);
        }         

        previousPosition = currentPosition;
    }

    public Vector3 getCurrentPosition()
    {
        return currentPosition;
    }
}
    
