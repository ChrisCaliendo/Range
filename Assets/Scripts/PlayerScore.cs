using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore instance;
    private FishingMinigame fishingMinigame;
    [SerializeField]
    private Text scoreText;
    public int score = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void addPoints()
    {
        score += 100;
        scoreText.text = "Score: " + score.ToString();
    }
}
