using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//deals with the item existing in the WORLD, not on player
public abstract class Item : MonoBehaviour
{

    public GameObject keySprite;
    public PlayerHandler player;

    public bool isHeld {get; private set;} = false;

    protected virtual void Start() {
        keySprite.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Player") {
            keySprite.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if(col.tag == "Player") {
            keySprite.SetActive(false);
        }
    }

    public void Update() {
        if(isHeld) {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 1, 0), .2f);
        }
    }

    public virtual void OnPickup() {
        this.gameObject.SetActive(true);
        isHeld = true;
    }

    public virtual void OnDrop() {
        isHeld = false;
    }

    public virtual void OnUse() {
        this.gameObject.SetActive(false);
    }
}
