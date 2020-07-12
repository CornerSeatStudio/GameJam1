using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirs {UP, DOWN, LEFT, RIGHT, UPLEFT, UPRIGHT, DOWNLEFT, DOWNRIGHT}

public class PlayerHandler : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 5f;
    public LayerMask layerMask;


    public bool IsIdle;
    public bool IsWalkingDown;
    public bool IsWalkingLeft;
    public bool IsWalkingUp;
    public bool IsWalkingRight;
    private Vector2 inputHandler;
    private Collider2D col;
    public Item CurrItem {get; set;} = null;
    private bool atWall;
    private Dictionary<MoveDirs, Vector2> moveToDirVec;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
      //  rb = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<Collider2D>();
        col.isTrigger = true; //prevents collisions from fucking shit
        moveToDirVec = new Dictionary<MoveDirs, Vector2>();
        setupdirvec();
    }  

    private void setupdirvec(){
        moveToDirVec[MoveDirs.UPLEFT] =    new Vector2(-1, 1);
        moveToDirVec[MoveDirs.UP] =        new Vector2(0, 1);
        moveToDirVec[MoveDirs.UPRIGHT] =   new Vector2(1, 1);
        moveToDirVec[MoveDirs.LEFT] =      new Vector2(-1, 0);
        moveToDirVec[MoveDirs.RIGHT] =     new Vector2(1, 0);
        moveToDirVec[MoveDirs.DOWNLEFT] =  new Vector2(-1, -1);
        moveToDirVec[MoveDirs.DOWN] =      new Vector2(0, -1);
        moveToDirVec[MoveDirs.DOWNRIGHT] = new Vector2(1, -1);
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

        // if(atWall) {
        //     DealWithWall();
        //     //transform.Translate(-inputHandler * Time.deltaTime, Space.World);
        // } else {
        //     transform.Translate(inputHandler * Time.deltaTime, Space.World);
        // }

       MovementCheck();
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0), 5f, layerMask);
               // RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1, Vector2.zero);

       // RaycastHit2D hit2 = Physics2D.CircleCast(transform.position, 5, Vector3.zero);
            //Debug.Log(hit.collider.tag);
        

        if(CanWalk()) transform.Translate(inputHandler * Time.deltaTime, Space.World);


      //  Debug.Log("last: " + lastdirection + " vs input: " + inputHandler.normalized);

    }

    private bool CompareKeyStuffs(Vector2 input1, Vector2 input2){
        return CompareKeyPresses(input1.x, input2.x) && CompareKeyPresses(input1.y, input2.y);
    }

    private bool CompareKeyPresses(float in1, float in2) {
        return in1 > 0 && in2 > 0 || in1 < 0 && in2 < 0 || in1 == 0 && in2 == 0;
    }

    //compare inputvector to raycastInfo
    bool CanWalk() {
        List<Vector2> unallowedDirs = MovementCheck();
        foreach(Vector2 mv in unallowedDirs){
            if(CompareKeyStuffs(inputHandler, mv)){ //if the directions ARE the same
                return false;
            }
        }
        return true;
    }



    List<Vector2> MovementCheck() {
        //Debug.Log("testc");
        //cast 8 rays for info
        List<Vector2> unallowedDirs = new List<Vector2>();

        if(Physics2D.Raycast(transform.position, moveToDirVec[MoveDirs.UPLEFT], 1f, layerMask)) unallowedDirs.Add(moveToDirVec[MoveDirs.UPLEFT]);        
        if(Physics2D.Raycast(transform.position, moveToDirVec[MoveDirs.UP], 1f, layerMask)) unallowedDirs.Add(moveToDirVec[MoveDirs.UP]);        
        if(Physics2D.Raycast(transform.position, moveToDirVec[MoveDirs.UPRIGHT], 1f, layerMask)) unallowedDirs.Add(moveToDirVec[MoveDirs.UPRIGHT]);        
        if(Physics2D.Raycast(transform.position, moveToDirVec[MoveDirs.LEFT], 1f, layerMask)) unallowedDirs.Add(moveToDirVec[MoveDirs.LEFT]);        
        if(Physics2D.Raycast(transform.position, moveToDirVec[MoveDirs.RIGHT], 1f, layerMask)) unallowedDirs.Add(moveToDirVec[MoveDirs.RIGHT]);        
        if(Physics2D.Raycast(transform.position, moveToDirVec[MoveDirs.DOWNLEFT], 1f, layerMask)) unallowedDirs.Add(moveToDirVec[MoveDirs.DOWNLEFT]);        
        if(Physics2D.Raycast(transform.position, moveToDirVec[MoveDirs.DOWN], 1f, layerMask)) unallowedDirs.Add(moveToDirVec[MoveDirs.DOWN]);        
        if(Physics2D.Raycast(transform.position, moveToDirVec[MoveDirs.DOWNRIGHT], 1f, layerMask)) unallowedDirs.Add(moveToDirVec[MoveDirs.DOWNRIGHT]);     

        return unallowedDirs;
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
        CurrItem.OnPickup();        
    }

    //dropping the item
    private void DropCurrItem(){
        CurrItem.OnDrop();
        CurrItem.transform.position = transform.position; //move it to the right place
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