using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTerminal : Interactable
{

    private CameraHandler cameraHandler;

    public GameObject exteriorShipSprite;
    public float zoomOutCamSize;
    public Vector3 zoomOutDisplacement;
    [Range(0, 1)] public float zoomOutSmoothness; 
    
    public GameObject interactKey;


    private bool isOpened;

    private float originCameraSize;
    private Vector3 originCamOffset;
    private float originSmoothSpeed;


    protected void Start() {
        interactKey.SetActive(false);
        //Debug.Log("yewot");
        exteriorShipSprite.SetActive(false);
        cameraHandler = Camera.main.GetComponent<CameraHandler>();
        isOpened = false;
        originCameraSize = Camera.main.orthographicSize;
        originCamOffset = cameraHandler.CurrCamOffset;
        originSmoothSpeed = cameraHandler.CurrSmoothSpeed;

    }

    protected override void OnTriggerEnter2D(Collider2D col) {
       // Debug.Log("dsaifj");
        if(col.gameObject.tag == "Player") interactKey.SetActive(true);
    }

    protected override void OnTriggerExit2D(Collider2D col) {
        if(col.gameObject.tag == "Player") {
            interactKey.SetActive(false);
            if (isOpened) OnClose();
        } 

       // Debug.Log("left view terminal");
        //if the terminal is left, reset camera
    }

    public override void Interact() {

        if (isOpened) {
            isOpened = false;
            OnClose();
        } else {
            isOpened = true;
            OnOpen();
        }


        

    }


    //enable the top down view mesh

    //zoom out of the camera and adjust z offset
    private void OnOpen() {
        //first store curr values 
        cameraHandler.CurrCamSize = zoomOutCamSize;
        cameraHandler.CurrCamOffset = zoomOutDisplacement;
        cameraHandler.CurrSmoothSpeed = zoomOutSmoothness;

        exteriorShipSprite.SetActive(true);
    }

    private void OnClose(){
        cameraHandler.CurrCamSize = originCameraSize;
        cameraHandler.CurrCamOffset = originCamOffset;
        cameraHandler.CurrSmoothSpeed = originSmoothSpeed;

        exteriorShipSprite.SetActive(false);
    }

}
