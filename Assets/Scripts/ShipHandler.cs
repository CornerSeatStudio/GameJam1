using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    //private Thruster[] thrusters;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        //thrusters = this.GetComponentsInChildren<Thruster>();

    }

    // Update is called once per frame
    void Update()
    {
    //    if(Input.GetButtonDown("Fire1")) {
    //         rb.AddForce(new Vector2(1000f, 0f));
    //     }    
    }

    void FixedUpdate() {
        
    }

    public void MoveShip(ThrusterTerminal thruster){
        Debug.Log("moving ship: " + thruster.burstDirection);
       rb.AddForce(thruster.burstDirection * 2000f);
    }

    
}
