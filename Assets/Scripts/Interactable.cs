using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //for both options
    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D col) {
    }

    protected virtual void OnTriggerExit2D(Collider2D col) {
    }
    
}
