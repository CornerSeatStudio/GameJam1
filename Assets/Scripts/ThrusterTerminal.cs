using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[System.Serializable] public class BurstEvent : UnityEvent<ThrusterTerminal> {}
public class ThrusterTerminal : Interactable //goes on each terminal
{
    public PlayerHandler player;
    public float targetBurstTime;
    public float targetBurstRandomnessRange; //+- the BurstTime (MUST BE < THAN TARGETBURSTTIME)
    public Vector2 burstDirection;
    public BurstEvent burstEvent;
    public GameObject ThrustLeft;

    public GameObject interactKey;

    private Item currItem;
    private IEnumerator mainBurstCoroutine;

    public GameObject terminalsAdd;

    public GameObject terminalsRetrieve;
    public GameObject terminalsSwap;
    protected void Start(){
        interactKey.SetActive(false);
        terminalsAdd.SetActive(false);
        terminalsSwap.SetActive(false);
        terminalsRetrieve.SetActive(false);
        //start thruster coroutine
        mainBurstCoroutine = BurstDriver();
        ThrustLeft.SetActive(false);
        StartCoroutine(mainBurstCoroutine);
    }

    protected override void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Player") interactKey.SetActive(true);
    }

    protected override void OnTriggerExit2D(Collider2D col) {
        if(col.gameObject.tag == "Player") interactKey.SetActive(false);
    }
    

    //if i access terminal with no item
    public override void Interact() {
        if(currItem == null) {
            Debug.Log("no item in terminal");
        } else {
            RetrieveFromInteractable();
            terminalsRetrieve.SetActive(true);
            StartCoroutine(StopSoundAfterTime2(0.5f));
        }
    }

    public void Interact(Item item) {
        if (currItem == null) {
            AddToInteractable(item);
            terminalsAdd.SetActive(true);
            StartCoroutine(StopSoundAfterTime2(0.5f));
        } else {
            SwapWithInteractable(item);
            terminalsSwap.SetActive(true);
            StartCoroutine(StopSoundAfterTime2(1f));
        }
    }

    protected void AddToInteractable(Item item) {
        currItem = item;
        Debug.Log("item " + currItem.name + " added to console");

        player.CurrItem = null;
    }

    protected void SwapWithInteractable(Item item){
        Debug.Log("Swapping item " + currItem.name + " with " + item.name);
        Item temp = currItem;
        currItem = item;
        player.CurrItem = temp;

    }

    protected void RetrieveFromInteractable(){
        Debug.Log("curr item " + currItem.name + " retrieved");
        player.CurrItem = currItem;
        currItem = null;
    }


    private IEnumerator localBurstCoroutine;

    public IEnumerator BurstDriver() {
        while(true) {
            if(localBurstCoroutine != null) { //for interupts
                if(currItem is ThrustHalter) { //for the interuppts
                    StopCoroutine(localBurstCoroutine);
                }
            } else { //for cycling
                if (currItem is ThrustHastener) {
                    localBurstCoroutine = BurstCycle(targetBurstTime/2);
                    StartCoroutine(localBurstCoroutine);
                } else {
                    localBurstCoroutine = BurstCycle(targetBurstTime);
                    StartCoroutine(localBurstCoroutine);
                }
            }

            yield return new WaitForSeconds(0.2f); //if laggy
        }
    }

    private IEnumerator BurstCycle(float currBurstTime) {
        yield return BurstCycleTimer(currBurstTime);
        Burst();
        
        localBurstCoroutine = null;
        
    }

    //every x seconds (aka a cycle), at least one thruster is guaranteed to burst
    private IEnumerator BurstCycleTimer(float currBurstTime) {
        float nextWaitTime = Random.Range(currBurstTime - targetBurstRandomnessRange, currBurstTime + targetBurstRandomnessRange);
        yield return new WaitForSeconds(nextWaitTime);

    }

    private void Burst() {
        burstEvent.Invoke(this);
        ThrustLeft.SetActive(true);
        StartCoroutine(StopSoundAfterTime(3));
    }
    private IEnumerator StopSoundAfterTime(float audiotime){
        yield return new WaitForSeconds(audiotime);
        ThrustLeft.SetActive(false);
    }
    private IEnumerator StopSoundAfterTime2(float audiotime){
        yield return new WaitForSeconds(audiotime);
        terminalsAdd.SetActive(false);
        terminalsRetrieve.SetActive(false);
        terminalsSwap.SetActive(false);
    }


}
