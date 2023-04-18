using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FishingMinigame : MonoBehaviour
{
    public GameObject player;
    public Transform playerSpawn;
    public Transform catchSpawn;
    public Transform lineOrigin;
    public GameObject hook;
    public GameObject gameCatch;
    public Image progressBar;
    public GameObject judgementIcon;
    public Sprite[] thumbs;
    private SpriteRenderer judgementRenderer;
    private bool playerCatching;
    private float score;
    public float catchIncrease; 
    public float timeDecay;
    private Color badColor;
    private Color midColor;
    private Color okColor;
    private Color goodColor;

    // Start is called before the first frame update
    void Start()
    {
        gameCatch.GetComponent<FishMovement>().AssignMinigame(gameObject.GetComponent<FishingMinigame>());

        hook.GetComponent<HookMovement>().setLineOrigin(lineOrigin.position);
        player.GetComponent<Movement>().stopMovement(true);
        hook = Instantiate(hook, playerSpawn.position, playerSpawn.rotation);
        gameCatch = Instantiate(gameCatch, catchSpawn.position, catchSpawn.rotation);
        score = 0.80f;
        progressBar.fillAmount = score;
        judgementRenderer = judgementIcon.GetComponent<SpriteRenderer>();
        badColor = new Color32(226, 68, 68, 255);
        midColor = new Color32(215, 113, 77, 255);
        okColor = new Color32(255, 222, 117, 255);
        goodColor = new Color32(116, 208, 113, 255);
        //gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(255, 255, 255, 255);
        //progressBar.color = okColor;
        playerCatching = false;
        //endMinigame();
    }

   
    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateScore();
        UpdateUI();
        CheckEndGameConditions();
    }

    void UpdateScore()
    {
        if (playerCatching)
        {
            score += catchIncrease;
        }
        else score -= timeDecay;
    }

    void UpdateUI()
    {
        progressBar.fillAmount = score;
        if (score>=0.75)
        {
            judgementRenderer.sprite = thumbs[3];
            progressBar.color = goodColor;
        }
        else if(score >= 0.50)
        {
            judgementRenderer.sprite = thumbs[2];
            progressBar.color = okColor;
        }
        else if(score >= 0.25)
        {
            judgementRenderer.sprite = thumbs[1];
            progressBar.color = midColor;
        }
        else
        {
            judgementRenderer.sprite = thumbs[0];
            progressBar.color = badColor;
        }

    }

    void CheckEndGameConditions()
    {
        if (score >= 1)
        {
            endMinigame();
        }
        else if (score<=0)
        {
            endMinigame();
        }
    }

    void endMinigame()
    {
        Destroy(hook);
        Destroy(gameCatch);
        player.GetComponent<Movement>().stopMovement(false);
        Destroy(gameObject);
    }

    public void catchingInProgress()
    {
        playerCatching = true;
    }

    public void catchingDelayed()
    {
        playerCatching = false;
    }

}
