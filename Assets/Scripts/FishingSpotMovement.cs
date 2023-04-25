using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpotMovement : MonoBehaviour
{
    public Transform fishingSpot;
    public Transform player;
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private Vector3 fishingSpotCurrentPosition;
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
            fishingSpotCurrentPosition.Set(currentPosition.x + 1.0f, currentPosition.y + 1.0f, currentPosition.z);
        }
        else if (currentPosition.x > previousPosition.x && currentPosition.y < previousPosition.y)
        {
            transform.localPosition = new Vector3(1, -1, 0);
            fishingSpotCurrentPosition.Set(currentPosition.x + 1.0f, currentPosition.y - 1.0f, currentPosition.z);
        }
        else if (currentPosition.x < previousPosition.x && currentPosition.y > previousPosition.y)
        {
            transform.localPosition = new Vector3(-1, 1, 0);
            fishingSpotCurrentPosition.Set(currentPosition.x - 1.0f, currentPosition.y + 1.0f, currentPosition.z);
        }
        else if (currentPosition.x < previousPosition.x && currentPosition.y < previousPosition.y)
        {
            transform.localPosition = new Vector3(-1, -1, 0);
            fishingSpotCurrentPosition.Set(currentPosition.x - 1.0f, currentPosition.y - 1.0f, currentPosition.z);
        }
        else if (currentPosition.x > previousPosition.x)
        {
            transform.localPosition = new Vector3(1, 0, 0);
            fishingSpotCurrentPosition.Set(currentPosition.x + 1.0f, currentPosition.y, currentPosition.z);
        }
        else if (currentPosition.x < previousPosition.x)
        {
            transform.localPosition = new Vector3(-1, 0, 0);
            fishingSpotCurrentPosition.Set(currentPosition.x - 1.0f, currentPosition.y, currentPosition.z);
        }
        else if (currentPosition.y > previousPosition.y)
        {
            transform.localPosition = new Vector3(0, 1, 0);
            fishingSpotCurrentPosition.Set(currentPosition.x, currentPosition.y + 1.0f, currentPosition.z);
        }
        else if (currentPosition.y < previousPosition.y)
        {
            transform.localPosition = new Vector3(0, -1, 0);
            fishingSpotCurrentPosition.Set(currentPosition.x, currentPosition.y - 1.0f, currentPosition.z);
        }         

        previousPosition = currentPosition;
    }

    public Vector3 getFishingSpotCurrentPosition()
    {
        return fishingSpotCurrentPosition;
    }
}
    
