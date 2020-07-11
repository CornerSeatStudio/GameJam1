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
    
    private bool isOpened;

    private float originCameraSize;
    private Vector3 originCamOffset;
    private float originSmoothSpeed;


    void Start() {
        exteriorShipSprite.SetActive(false);
        cameraHandler = Camera.main.GetComponent<CameraHandler>();
        isOpened = false;
        originCameraSize = Camera.main.orthographicSize;
        originCamOffset = cameraHandler.CurrCamOffset;
        originSmoothSpeed = cameraHandler.CurrSmoothSpeed;

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

    protected override void OnTriggerExit2D(Collider2D col) {
        
       // Debug.Log("left view terminal");
        //if the terminal is left, reset camera
        if (isOpened) OnClose();
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
