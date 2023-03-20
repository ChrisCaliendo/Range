using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalBulletBehavior : MonoBehaviour
{

    [SerializeField] private const float lifespan = 2;
    [SerializeField] private const float speed = 20f;
    [SerializeField] private const int damage = 40;
    
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * -speed;
        Destroy(gameObject, lifespan);
    }

    void OnTriggerEnter2D(Collider2D targetInfo)
    {
        Enemy enemy = targetInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage, rb.velocity, 100);
        }
        Destroy(gameObject);
    }

   
}
