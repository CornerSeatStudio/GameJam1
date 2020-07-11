﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class BurstEvent : UnityEvent<ThrusterTerminal> {}
public class ThrusterTerminal : Interactable //goes on each terminal
{
    public PlayerHandler player;

    public float targetBurstTime;
    public float targetBurstRandomnessRange; //+- the BurstTime (MUST BE < THAN TARGETBURSTTIME)

    public Vector2 burstDirection;
    public BurstEvent burstEvent;

    private Item currItem;
    private IEnumerator mainBurstCoroutine;

    void Start(){
        //start thruster coroutine
        mainBurstCoroutine = BurstDriver();
        StartCoroutine(mainBurstCoroutine);
    }

    //if i access terminal with no item
    public override void Interact() {
        if(currItem == null) {
            Debug.Log("no item in terminal");
        } else {
            RetrieveFromInteractable();
        }
    }

    public override void Interact(Item item) {
        if (currItem == null) {
            AddToInteractable(item);
        } else {
            SwapWithInteractable(item);
        }
    }

    protected override void AddToInteractable(Item item) {
        currItem = item;
        Debug.Log("item " + currItem.name + " added to console");
        player.CurrItem = null;
    }

    protected override void SwapWithInteractable(Item item){
        Debug.Log("Swapping item " + currItem.name + " with " + item.name);
        Item temp = currItem;
        currItem = item;
        player.CurrItem = temp;

    }

    protected override void RetrieveFromInteractable(){
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
    }


}
