using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHandler : MonoBehaviour
{

    public float maxHealth;
    private Rigidbody2D rb;
    private float currHealth;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        currHealth = maxHealth;

    }

    void Update() {
        Debug.Log(currHealth);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Asteroid"){
            currHealth -= col.collider.gameObject.GetComponent<Asteroid>().GetAsteroidDamage();
        }
    }

    public void MoveShip(ThrusterTerminal thruster){
        //Debug.Log("moving ship: " + thruster.burstDirection);
        //if the ship is too fast, dont add force, but still do particle effect
      // rb.AddForce(thruster.burstDirection * 100f, Forcemode2d.Impulse);
    }

    
}
