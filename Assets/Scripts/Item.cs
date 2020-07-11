using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//deals with the item existing in the WORLD, not on player
public abstract class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) {
    }

    private void OnTriggerExit2D(Collider2D col) {
    }

    public virtual void OnPickup() {
        Debug.Log("destroying");

        //hide it
        this.gameObject.SetActive(false);
    }

    public virtual void OnDrop() {
        //show it
        Debug.Log("creating");
        this.gameObject.SetActive(true);


    }
}
