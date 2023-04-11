using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMinigame : MonoBehaviour
{
    public GameObject player;
    public Transform playerSpawn;
    public Transform catchSpawn;
    public GameObject hook;
    public GameObject gameCatch;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Movement>().stopMovement(true);
        hook = Instantiate(hook, playerSpawn.position, playerSpawn.rotation);
        gameCatch = Instantiate(gameCatch, catchSpawn.position, catchSpawn.rotation);
         endMinigame();
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }

    void endMinigame()
    {
        Destroy(hook);
        Destroy(gameCatch);
        player.GetComponent<Movement>().stopMovement(false);
        Destroy(gameObject);
    }

    

}
