using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip menuMusic;
    [SerializeField]
    private AudioClip gameMusic;
    [SerializeField]
    private AudioSource source;
    private static MusicManager instance;

    private void Awake()
    {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        playMenuMusic();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            playGameMusic();
        }
        else if(scene.name == "Menu")
        {
            playMenuMusic();
        }
    }

    static public void playMenuMusic()
    {
        if(instance != null)
        {
            if(instance.source != null)
            {
                instance.source.Stop();
                instance.source.clip = instance.menuMusic;
                instance.source.Play();
            }
            else 
            {
                Debug.LogError("Unavailable MusicManager component");
            }
        }
    }

    static public void playGameMusic()
    {
        if(instance != null) 
        {
            if (instance.source != null) 
            {
                instance.source.Stop();
                instance.source.clip = instance.gameMusic;
                instance.source.Play();
            }
         } 
        else 
        {
            Debug.LogError("Unavailable MusicManager component");
        }
    }
}
