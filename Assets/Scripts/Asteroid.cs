using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public float damage;
    public float damageRandomOffset;

    public Vector2 thrustRange;

    //private float spinSpeed;
    private float thrust;
    
    private Rigidbody2D rb;

    void Start() {
        thrust = Random.Range(thrustRange.x, thrustRange.y);
        rb = this.GetComponent<Rigidbody2D>();
        Vector2 temp = Random.insideUnitCircle.normalized;
        rb.AddForce(temp * thrust, ForceMode2D.Impulse);
    }

    public float GetAsteroidDamage() {
        return Random.Range(damage - damageRandomOffset, damage + damageRandomOffset);
    }

  
}
