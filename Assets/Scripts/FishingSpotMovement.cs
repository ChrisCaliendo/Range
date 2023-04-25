using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpotMovement : MonoBehaviour
{
    //public Transform fishingSpot;
    //public Transform player;
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private Vector3 fishingSpotCurrentPosition;
    private bool lastDir;
    // Start is called before the first frame update
    void Start()
    {
        lastDir = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    async public void flipPosition(bool dir)
    {
        if (lastDir != dir) {
            lastDir = dir;
            Vector2 newScale = gameObject.transform.position;
            newScale.x *= -1;
            gameObject.transform.localScale = newScale;
        }
        else return;
        
    }
}
    
