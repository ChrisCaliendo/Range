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
        
        if(move.CanFish())
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                fishingScene.SetActive(true);
                Console.Write("works");
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                fishingScene.SetActive(false);
                Console.Write("also works");
            }
        }
    }
}
