using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{

    public FishingMinigame minigame;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignMinigame(FishingMinigame parent)
    {
        minigame = parent;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        minigame.catchingInProgress(); 
    }
    private void OnTriggerExit2D(Collider2D other) {
        minigame.catchingDelayed();
    }
}
