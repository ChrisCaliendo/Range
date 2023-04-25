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
    public GameObject fishingSpot;
    // Start is called before the first frame update
    private Tilemap tileChecker;
    private bool FishSpotOnWater;
    private bool inMinigame;

    void Start()
    {
        tileChecker = move.tilemap;
        move = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFishingSpot();
        Fish();
    }

    async void Fish(){
        if(move.CanFish()&&FishSpotOnWater&&!inMinigame)
        {
            inMinigame = true;
            if (Input.GetKeyDown(KeyCode.F) && FishSpotOnWater)
            {
               //move.stopMovement(true);
                fishingScene.GetComponent<FishingMinigame>().player = gameObject;
                Instantiate(fishingScene, transform.position, transform.rotation);
            }
        }   
    }

    void CheckFishingSpot()
    {
        TileBase fishingSpotTile = move.tilemap.GetTile(move.tilemap.WorldToCell(fishingSpot.transform.position));
        bool foundTile = false;
        for (int i = 0; i < move.waterTiles.Length; i++)
        {
            if (fishingSpotTile.name == move.waterTiles[i].name)
            {
                foundTile = true;
                //Debug.Log("True!");
                break;
            }
        }
        FishSpotOnWater = foundTile;
        //foundTile = false;
    }

    public void confirmEndMinigame()
    {
        inMinigame = false;
    }
}
