using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{
    private Rigidbody2D corpse;
    public float finalBlowForce;
    public Vector2 finalBlowDirection;

    void Start()
    {
        corpse = gameObject.GetComponent<Rigidbody2D>();
        float combinedForce = Mathf.Abs(finalBlowDirection.x) + Mathf.Abs(finalBlowDirection.y);
        Vector2 finalBlow = new Vector2((finalBlowDirection.x / combinedForce) * finalBlowForce, (finalBlowDirection.y / combinedForce) * finalBlowForce + 100);
        corpse.AddForce(finalBlow);
    }


}
