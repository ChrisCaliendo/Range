using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{

    private FishingMinigame minigame;


    public void setParentMinigame(GameObject parent)
    {
        minigame = parent.GetComponent<FishingMinigame>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        minigame.catchingInProgress(); 
    }
    private void OnTriggerExit2D(Collider2D other) {
        minigame.catchingDelayed();
    }
}
