using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ShipHandler : MonoBehaviour
{

    public float maxHealth;
    public Image healthbar;
    public float maxSpeed;
    public UnityEvent onDeath;
    private Rigidbody2D rb;
    private float currHealth;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
        healthbar.fillAmount = 1;

    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Asteroid"){
            currHealth -= col.collider.gameObject.GetComponent<Asteroid>().GetAsteroidDamage();
            healthbar.fillAmount = currHealth / maxHealth;

            if (currHealth <= 0) {
                ShipDeath();
                onDeath.Invoke();
            }
        
        }
    }

    public void MoveShip(ThrusterTerminal thruster){
        //Debug.Log("moving ship: " + thruster.burstDirection);
        //if the ship is too fast, dont add force, but still do particle effect

        StartCoroutine(ParticleLoop(thruster));
        rb.AddForce(thruster.burstDirection * 10f, ForceMode2D.Impulse);

    }

    private IEnumerator ParticleLoop(ThrusterTerminal thruster) {
        Debug.Log("test");
        foreach(ParticleSystem p in thruster.thrustParticles) {
            p.Play();
        }
        yield return new WaitForSeconds(2f);
        foreach(ParticleSystem p in thruster.thrustParticles) {
            p.Stop();
        }
    }

    private void ShipDeath(){
        //disable render for main ship game oject
        //play a sound
        //enable render for ship bits
        //change camera target to a random ship bit
    }

    
}
