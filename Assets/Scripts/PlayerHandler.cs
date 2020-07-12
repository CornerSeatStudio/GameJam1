using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 5f;

    public bool IsIdle;
    public bool IsWalkingDown;
    public bool IsWalkingLeft;
    public bool IsWalkingUp;
    public bool IsWalkingRight;
    private Vector2 inputHandler;
    private Collider2D col;
    public Item CurrItem {get; set;} = null;
    private bool atWall;
        

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
      //  rb = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<Collider2D>();
        col.isTrigger = true; //prevents collisions from fucking shit
    }  

    // Update is called once per frame
    void Update() {

        inputHandler = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputHandler.Normalize();

        if(inputHandler == Vector2.zero){
            IsIdle=true;
        }else{
            IsIdle=false;
        }
        if(Input.GetKeyDown(KeyCode.S)){
            IsWalkingDown=true;
        }else if(Input.GetKeyUp(KeyCode.S)){
            IsWalkingDown=false;
        }
        if(Input.GetKeyDown(KeyCode.A)){
            IsWalkingLeft=true;
        }else if(Input.GetKeyUp(KeyCode.A)){
            IsWalkingLeft=false;
        }
        if(Input.GetKeyDown(KeyCode.W)){
            IsWalkingUp=true;
        }else if(Input.GetKeyUp(KeyCode.W)){
            IsWalkingUp=false;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            IsWalkingRight=true;
        }else if(Input.GetKeyUp(KeyCode.D)){
            IsWalkingRight=false;
        }
        inputHandler *= moveSpeed;

     //   Debug.Log("forward transform" + transform.up);
       
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

        if(atWall) {
            DealWithWall();
        } else {
            transform.Translate(inputHandler * Time.deltaTime, Space.World);
        }

      //  Debug.Log("last: " + lastdirection + " vs input: " + inputHandler.normalized);

    }

    void DealWithWall() {
        //or if only a single key is pressed

        //check if both keys pressed at same time
        if(lastX != 0 && lastY != 0) {
            Debug.Log("both key axis pressed");
            if(!(inputHandler.x == lastX && inputHandler.y == 0
                || inputHandler.x == 0 && inputHandler.y == lastY
                || inputHandler.x == lastX && inputHandler.y == lastY)) {
                    transform.Translate(inputHandler * Time.deltaTime, Space.World);
                }
            
        } else if (lastX != 0 && lastY == 0) { //if only one of a d
            Debug.Log("a or d pressed");
            if(!(inputHandler.x == lastX)) { //only move if a or d isnt pressed
                transform.Translate(inputHandler * Time.deltaTime, Space.World);
            }
            
        }else if (lastX == 0 && lastY != 0) { // if only one of w 
            Debug.Log("w or s pressed");
            if(!(inputHandler.y == lastY)) { //only move if a or d isnt pressed
                transform.Translate(inputHandler * Time.deltaTime, Space.World);
            }

        } 
            

    }

    float lastX;
    float lastY;

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Wall") {
            lastX = inputHandler.x;
            lastY = inputHandler.y;
            Debug.Log("entered wall");
            atWall = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(collider.tag == "Wall") {
            Debug.Log("exited wall");
            atWall = false;
            
        }
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
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 2, Vector2.zero);
        foreach(RaycastHit2D hit in hits) { //for the hits
            if(hit.transform.GetComponent<ViewTerminal>()) { //if ive interacted with a thruster terminal WITH an item
                hit.transform.GetComponent<ViewTerminal>().Interact();
            }
        }
    }

  
    void LateUpdate(){

        //anim stuff
        animator.SetBool("IsIdle", IsIdle);
        animator.SetBool("IsWalkingDown", IsWalkingDown);
        animator.SetBool("IsWalkingLeft", IsWalkingLeft);
        animator.SetBool("IsWalkingRight", IsWalkingRight);
        animator.SetBool("IsWalkingUp", IsWalkingUp);

    }
    
}
