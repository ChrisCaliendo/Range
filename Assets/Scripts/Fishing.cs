using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Fishing : MonoBehaviour
{
    [SerializeField] 
    GameObject fishingScene;
    //public FishingView fishingScene;
    [NonSerialized]
    public Movement move;
    public FishingSpotMovement fishingSpot;
    public Boolean CanFish;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fish();
    }

    async void Fish(){
        if(move.CanFish())
        {
            Debug.Log("Hello " + fishingSpot.getCurrentPosition());
            TileBase fishingSpotTile = move.tilemap.GetTile(move.tilemap.WorldToCell(fishingSpot.getCurrentPosition()));
            bool foundTile = false;
            for (int i = 0; i < move.waterTiles.Length; i++)
            {
                if (fishingSpotTile.name == move.waterTiles[i].name)
                {
                    foundTile = true;
                    break;
                }
            }
            foundTile = false;
            if (Input.GetKeyDown(KeyCode.F))
            {
               //move.stopMovement(true);
                fishingScene.GetComponent<FishingMinigame>().player = gameObject;
                Instantiate(fishingScene, transform.position, transform.rotation);
            }
        }   
    }
}
