using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //for both options
    public abstract void Interact();
    public abstract void Interact(Item item);

    protected abstract void AddToInteractable(Item item);

    protected abstract void SwapWithInteractable(Item item);

    protected abstract void RetrieveFromInteractable();

    private void OnTriggerEnter2D(Collider2D col) {
    }

    private void OnTriggerExit2D(Collider2D col) {
    }
    
}
