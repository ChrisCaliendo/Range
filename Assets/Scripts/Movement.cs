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
    //public TileBase defaultTile;
    public float movementSpeed;
    private Vector2 movementInput;
    private float currentVelocity;
    private float HorizontalDir;
    private float VerticalDir;
    private bool OnWater;
    public float waterSpeed;
    //private Vector2 lastMovementInput; // to store the last movement input

    private void Awake()
    {

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckTile();
        Move();
        Animate();
    }

    private void Move()
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
    void CheckTile()
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
}