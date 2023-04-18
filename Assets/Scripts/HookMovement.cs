
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HookMovement : MonoBehaviour
{
    public GameObject linePoint;
    private LineRenderer fishingLine;
    private Rigidbody2D rigidbody2D;
    private float movementSpeed = 100.0f;
    private Vector2 movementInput;

    // Start is called before the first frame update
    void Start()
    {

        rigidbody2D = GetComponent<Rigidbody2D>();
        if (fishingLine == null)
            fishingLine = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
        
    }

    void DrawLine()
    {
        fishingLine.SetPosition(0, linePoint.transform.position);
        
    }
    void Movement()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        movementInput = new Vector2(Horizontal, Vertical).normalized;
        rigidbody2D.velocity = movementInput * movementSpeed * Time.fixedDeltaTime;
    }

    public void setLineOrigin(Vector2 lineOrigin)
    {
        if(fishingLine == null)
            fishingLine = GetComponent<LineRenderer>();
        fishingLine.SetPosition(1, lineOrigin);

    }

}
