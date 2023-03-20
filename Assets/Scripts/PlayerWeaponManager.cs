using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private SpriteRenderer gunSpriteRenderer;
    [SerializeField] private Sprite[] gunSprites;
    

    private void Awake()
    {
        gunSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    void SetWeapon(int weaponId)
    {
        switch (weaponId)
        {
            case (0): //Reserved (potential no gun option)
                gunSpriteRenderer.sprite = gunSprites[0];
                break;
            case (1): //The Dixon
                //Standard gun shoots regular bullets at moderate rate
                gunSpriteRenderer.sprite = gunSprites[1];
                break;
            case (2): //The Anaconda
                //Fire based gun that shoots fire bullets and has a more wiggly bullet path if possible
                gunSpriteRenderer.sprite = gunSprites[2];
                break;
            case (3): //The Diagun
                //gun will always shoot 45 degrees from where player is aiming
                //and will bounce off walls to do trick shots
                gunSpriteRenderer.sprite = gunSprites[3];
                break;
            case (4): //The Cycle
                //bullets that aim adjust toward enemies mid flight
                gunSpriteRenderer.sprite = gunSprites[4];
                break;
            case (5): //The Capman
                //bigger bullets, more damage, more cooldown between shoots 
                gunSpriteRenderer.sprite = gunSprites[5];
                break;
                
        }

    }

                /* Gun Ideas for later
                * 
                * The Room Cleaner
                * - Wind based gun that does excellent knockback and potentially moves objects
                * 
                * The War Season
                * - gun that blows exposive powder around that enemies cant see, 
                * if a gun is shot while in powder gas area the area explodes
                * 
                * The Miller
                * - gun has one bullet and can devestate any enemy, 
                * key point: the gun is notorious and many enemies will run away if they see that the gun is pointed at them
                * - The gun will jam if attenpted to be used during a boss fight :))))
                * 
                */
    
}
