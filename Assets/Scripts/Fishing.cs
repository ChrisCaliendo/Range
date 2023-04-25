using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Fishing : MonoBehaviour
{
    [SerializeField] 
    public GameObject fishingScene;
    //public FishingView fishingScene;
    public Movement move;
    public FishingSpotMovement fishingSpot;
    // Start is called before the first frame update
    private bool OnWater;

    void Start()
    {
        move = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Fish();
    }

    async void Fish(){
        if(move.CanFish())
        {
            TileBase fishingSpotTile = move.tilemap.GetTile(move.tilemap.WorldToCell(fishingSpot.getFishingSpotCurrentPosition()));
            bool foundTile = false;
            for (int i = 0; i < move.waterTiles.Length; i++)
            {
                if (fishingSpotTile.name == move.waterTiles[i].name)
                {
                    foundTile = true;
                    Debug.Log("True!");
                    break;
                }
            }
            OnWater = foundTile;
            foundTile = false;
            if (Input.GetKeyDown(KeyCode.F) && OnWater)
            {
               //move.stopMovement(true);
                fishingScene.GetComponent<FishingMinigame>().player = gameObject;
                Instantiate(fishingScene, transform.position, transform.rotation);
            }
        }   
    }
}
