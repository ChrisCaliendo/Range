using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private float movementSpeed = 100.0f;
    private Vector2 movementInput;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        movementInput = new Vector2(Horizontal, Vertical).normalized;
        rigidbody2D.velocity = movementInput * movementSpeed * Time.fixedDeltaTime;
    }

    private void OnColliderEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.tag);
       if(other.gameObject.tag=="Fish") Debug.Log("did it"); 
    }
}
