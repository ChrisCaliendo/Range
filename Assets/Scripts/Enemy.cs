using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    [Range (0, 2)]
    public float knockbackResistance = 1;
    public GameObject corpse;
    private Rigidbody2D body;

    private void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }
    public void TakeDamage (int damage, Vector2 damageDirection, float knockbackForce)
    {
        health -= damage;

        if (health <= 0) Die(damageDirection, knockbackForce);
        else
        {
            Knockback(damageDirection, knockbackForce);//When shoot enemy should be 
        }
    }

    public void TakeNonPhysicalDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die( new Vector2(0,0), 0);
    }

    void Die(Vector2 finalBlowDirection, float finalBlowForce)
    {
        corpse.GetComponent<DeadEnemy>().finalBlowDirection = finalBlowDirection;
        corpse.GetComponent<DeadEnemy>().finalBlowForce = finalBlowForce;
        Instantiate(corpse, body.position, body.transform.rotation);
        Destroy(gameObject);
    }

    void Knockback(Vector2 direction, float knockbackForce)
    {
        float combinedForce = Mathf.Abs(direction.x)+ Mathf.Abs(direction.y);
        
        Vector2 knockback = new Vector2((direction.x/ combinedForce) * knockbackForce, (direction.y / combinedForce) * knockbackForce + 1);
        body.AddForce(knockback);
    }

   
}
