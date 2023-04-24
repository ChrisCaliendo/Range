using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fishing : MonoBehaviour
{
    [SerializeField] GameObject fishingScene;
    //public FishingView fishingScene;
    public Movement move;
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                //move.stopMovement(true);
                fishingScene.GetComponent<FishingMinigame>().player = gameObject;
                Instantiate(fishingScene, transform.position, transform.rotation);
            }
        }
    }
}
