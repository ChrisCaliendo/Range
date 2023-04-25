using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    public Tilemap tilemap;
    public TileBase[] waterTiles;
    public TileBase[] drownTiles;
    //public TileBase defaultTile;
    public float movementSpeed;
    private Vector2 movementInput;
    private float currentVelocity;
    private float HorizontalDir;
    private float VerticalDir;
    private bool OnWater;
    private bool OnLand;
    private bool Drowning;
    private bool Waiting;
    private bool stopMoving;
    private FishingSpotMovement fsm;
    public float waterSpeed;
    public GameObject spawnpoint;
    public GameObject fishingSpot;
    //private Vector2 lastMovementInput; // to store the last movement input
    private Vector2 previousPositionOnLand;

    private void Awake()
    {
        fsm = fishingSpot.GetComponent<FishingSpotMovement>();
        player = GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnpoint = Instantiate(spawnpoint);
        OnLand = true;
    }

    private void FixedUpdate(){
        CheckTileForWater();
        CheckIfDrowning();
        EnsureSpawn();
    }

    private void Update()
    {
        if(!stopMoving){
            Move();
            Animate();
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, 0);
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
            
            Vector2 newScale = fishingSpot.transform.localPosition;
            if (Mathf.Sign(Horizontal)!= Mathf.Sign(newScale.x))
            {
                newScale.x *= -1;
                fishingSpot.transform.localPosition = newScale;
            }
            float currentSpeed;
            movementInput = new Vector2(Horizontal, Vertical).normalized;
            if (OnWater) currentSpeed = waterSpeed;
            else currentSpeed = movementSpeed;
            rigidbody2D.velocity = movementInput * currentSpeed * Time.fixedDeltaTime;
            //ishingSpot.transform.position.x *= (Horizontal/(Mathf.Abs(Horizontal)));
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

    public void CheckTileForWater()
    {
        TileBase currentTile = CheckTile(transform.position);
        OnWater = onWaterTile(currentTile);

        bool foundTile = false;
        for (int i = 0; i < drownTiles.Length; i++)
        {
            if (currentTile.name == drownTiles[i].name)
            {
                foundTile = true;
                break;
            }
        }
        Drowning = foundTile;

    }

    public bool onWaterTile(TileBase currentTile)
    {
        bool foundTile = false;
        for (int i = 0; i < waterTiles.Length; i++)
        {

            if (currentTile.name == waterTiles[i].name)
            {
                foundTile = true;
                break;
            }
        }
        return foundTile;
    }

    public TileBase CheckTile(Vector3 tilePos){
        Vector3Int playerPosition = tilemap.WorldToCell(tilePos);
        // Get the tile at the player's position
        return tilemap.GetTile(playerPosition);
    }


    void EnsureSpawn(){
        if(OnWater==false&&OnLand==false){
            Destroy(spawnpoint);
            spawnpoint = Instantiate(spawnpoint, transform.position, transform.rotation);
            OnLand = true;
        } 
        else if(OnWater==true&&OnLand==true) OnLand = false;
    }

    public async void CheckIfDrowning(){
        if(Drowning){
            stopMoving = true;
            animator.SetBool("Drown?", Drowning);
            Invoke("Respawn", 3);
        }
    }

    void Respawn()
    {
        player.position = spawnpoint.transform.position;
        Drowning = false;
        animator.SetBool("Drown?", Drowning);
        stopMoving = false;
    }


    public Boolean CanFish()
    {
        return !OnWater;
    }

    public void stopMovement(bool x){
        stopMoving = x;
    } 
}