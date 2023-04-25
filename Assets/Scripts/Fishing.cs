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
        inMinigame = false;
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
            Debug.Log("Can Fish: " + move.CanFish() + "In Minigame: " + inMinigame);
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                inMinigame = true;    
               //move.stopMovement(true);
                fishingScene.GetComponent<FishingMinigame>().player = gameObject;
                Instantiate(fishingScene, transform.position, transform.rotation);
            }
        }   
    }

    void CheckFishingSpot()
    {
        TileBase fishingSpotTile = move.tilemap.GetTile(move.tilemap.WorldToCell(fishingSpot.transform.position));
        FishSpotOnWater = move.onWaterTile(fishingSpotTile);
        //foundTile = false;
    }

    public void confirmEndMinigame()
    {
        inMinigame = false;
    }
}
