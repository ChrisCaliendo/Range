using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    public Tilemap tilemap;
    public TileBase[] waterTiles;
    public TileBase[] landTiles;
    //public TileBase defaultTile;
    public float movementSpeed;
    private Vector2 movementInput;
    private float currentVelocity;
    private float HorizontalDir;
    private float VerticalDir;
    private bool OnWater;
    private bool OnLand;
    private bool stopMoving;
    public float waterSpeed;
    public GameObject spawnpoint;
    //private Vector2 lastMovementInput; // to store the last movement input
    private Vector2 previousPositionOnLand;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnpoint = Instantiate(spawnpoint);
        OnLand = true;
    }

    private void FixedUpdate(){
        CheckTile();
        EnsureSpawn();
    }

    private void Update()
    {
        if(!stopMoving){
            Move();
            Animate();
        } 
    }

    public void Move()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        
        if (Mathf.Abs(Horizontal) <= 0.2f && Mathf.Abs(Vertical) <= 0.2f)
        {
            rigidbody2D.velocity = new Vector2(0, 0);
            return;
        }
        else
        {
            float currentSpeed;
            movementInput = new Vector2(Horizontal, Vertical).normalized;
            if (OnWater) currentSpeed = waterSpeed;
            else currentSpeed = movementSpeed;
            rigidbody2D.velocity = movementInput * currentSpeed * Time.fixedDeltaTime;
            //lastMovementInput = movementInput; // store the last movement input
        }
        
    }

    private void Animate()
    {
        HorizontalDir = Input.GetAxis("Horizontal");
        VerticalDir = Input.GetAxis("Vertical");
        if (Mathf.Abs(HorizontalDir) >= 0.1f)
        {
            animator.SetFloat("MovementX", HorizontalDir);
        }
        if(Mathf.Abs(VerticalDir) >= 0.1f)
        {
            animator.SetFloat("MovementY", VerticalDir);
        }
        animator.SetFloat("Speed", rigidbody2D.velocity.magnitude);
        animator.SetBool("PlayerInWater?", OnWater);
    }
    public void CheckTile()
    {
        Vector3Int playerPosition = tilemap.WorldToCell(transform.position);

        // Get the tile at the player's position
        TileBase currentTile = tilemap.GetTile(playerPosition);
        bool foundTile = false;
        for (int i = 0; i < waterTiles.Length; i++)
        {
            if (currentTile.name == waterTiles[i].name)
            {
                foundTile = true;
                break;
            }
        }
        OnWater = foundTile;

    }


    void EnsureSpawn(){
        if(OnWater==false&&OnLand==false){
            Destroy(spawnpoint);
            spawnpoint = Instantiate(spawnpoint, transform.position);
            OnLand = true;
        } 
        else if(OnWater==true&&OnLand==true) OnLand = false;
    }


    public Boolean CanFish()
    {
        if(OnWater)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void stopMovement(bool x){
        stopMoving = x;
    } 
}