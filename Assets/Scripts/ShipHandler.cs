﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ShipHandler : MonoBehaviour
{

    public float maxHealth;
    public Image healthbar;
    public float maxSpeed;
    public float thrusterForce;
    public List<GameObject> brokenShipParts;
    public GameObject deathMenu;
    public GameObject mainUI;
    private Rigidbody2D rb;
    private float currHealth;

    void Awake() {
        //dont show broken ship;
        foreach(GameObject go in brokenShipParts){
            go.gameObject.SetActive(false);
        }

        deathMenu.gameObject.SetActive(false);
        mainUI.gameObject.SetActive(true);

        healthbar.fillAmount = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
        

    }

    void Update()
{
    // if(Input.GetButtonDown("Fire1")){
    //     ShipDeath();
    // }
}

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Asteroid"){
            currHealth -= col.collider.gameObject.GetComponent<Asteroid>().GetAsteroidDamage();
            healthbar.fillAmount = currHealth / maxHealth;

          //  Debug.Log(currHealth);
            if (currHealth <= 0) {
                ShipDeath();
            }
        
        }
    }

    public void MoveShip(ThrusterTerminal thruster){
        //Debug.Log("moving ship: " + thruster.burstDirection);
        //if the ship is too fast, dont add force, but still do particle effect

        StartCoroutine(ParticleLoop(thruster));
       // rb.AddForce(thruster.burstDirection * thrusterForce, ForceMode2D.Impulse);

    }

    private IEnumerator ParticleLoop(ThrusterTerminal thruster) {
        //Debug.Log("test");
        foreach(ParticleSystem p in thruster.thrustParticles) {
            p.Play();
        }
        yield return new WaitForSeconds(2f);
        foreach(ParticleSystem p in thruster.thrustParticles) {
            p.Stop();
        }
    }

    private void ShipDeath(){
        Debug.Log("deidZD");
        mainUI.SetActive(false);

        deathMenu.transform.parent = null;
        deathMenu.SetActive(true);
        //unparent the camera
        Camera.main.gameObject.transform.parent = null;

        //disable render for main ship game oject
        this.gameObject.SetActive(false);


 
        //enable render for ship bits
        foreach(GameObject go in brokenShipParts) {
            //unparent
            go.SetActive(true);
            go.transform.parent = null;
        
        }

        brokenShipParts[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10));
        brokenShipParts[1].GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0));
        brokenShipParts[2].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -10));
        brokenShipParts[3].GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 0));
       // Debug.Log("deadt");
    }

    
}
