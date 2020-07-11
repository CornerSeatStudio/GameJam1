using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    public float moveSpeed = 8f;

    private Vector2 inputHandler;
    private Collider2D col;
    public Item CurrItem {get; set;} = null;
    
    // Start is called before the first frame update
    void Start()
    {
      //  rb = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<Collider2D>();
        col.isTrigger = true; //prevents collisions from fucking shit
    }  

    // Update is called once per frame
    void Update()
    {

        inputHandler = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputHandler.Normalize();
        inputHandler *= moveSpeed;

       
        if(Input.GetButtonDown("Fire1")) {
            InteractWithThrusterTerminal();
            InteractWithItem();
            InteractWithViewTerminal();
            //Debug.Log("fire1d");
        }

        if(Input.GetButtonDown("Fire2")) { //for dropping shit
            if(CurrItem != null) DropCurrItem();
        }

        // if(CurrItem != null) Debug.Log("currItem: " + CurrItem.name);
        // else Debug.Log("no item in hand");
    }


    void FixedUpdate() {
        transform.Translate(inputHandler * Time.deltaTime);
    }


    //upon approaching an item
    private void InteractWithItem() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1, Vector2.zero);
        foreach(RaycastHit2D hit in hits) { //for the hits
            if(hit.transform.GetComponent<Item>()) { //if ive interacted with an item
                DealWithItem(hit.transform.GetComponent<Item>());
                break;
            }
        }
    }

    private void DealWithItem(Item item) {
        if(CurrItem != null) DropCurrItem(); //if im holding an item, swap it off
        CurrItem = item;
        item.OnPickup();        
    }

    //dropping the item
    private void DropCurrItem(){
        CurrItem.OnDrop();
        CurrItem.transform.position = transform.position; //move it to the right place
        Debug.Log(CurrItem.transform.position + " vs " + transform.position);
        CurrItem = null;
    }


    //checks if cunts up to terminal
    private void InteractWithThrusterTerminal() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1, Vector2.zero);
        foreach(RaycastHit2D hit in hits) { //for the hits
            if(hit.transform.GetComponent<ThrusterTerminal>()) { //if ive interacted with a thruster terminal WITH an item
                DealWithInteraction(hit.transform.GetComponent<ThrusterTerminal>());
                break; 
            }
        }
    }

    private void DealWithInteraction(ThrusterTerminal terminal){
        if(CurrItem == null) {
            terminal.Interact();
        } else {
            terminal.Interact(CurrItem);
        }
    }

     private void InteractWithViewTerminal() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1, Vector2.zero);
        foreach(RaycastHit2D hit in hits) { //for the hits
            if(hit.transform.GetComponent<ViewTerminal>()) { //if ive interacted with a thruster terminal WITH an item
                hit.transform.GetComponent<ViewTerminal>().Interact();
            }
        }
    }

  

    
}
