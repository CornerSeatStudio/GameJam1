using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public float camSize = 10f;
    public float cameraZOffset = -500;
    public float smoothSpeed = .9f;
    public Transform target;

    public float CurrCamSize {get; set; }
    public Vector3 CurrCamOffset {get; set; }
    public float CurrSmoothSpeed {get; set; }

    void Awake(){ //HAS TO HAPPEN BEFORE VIEW TERMINAL SCRIPT
        Camera.main.orthographicSize = camSize;

        //init vars
        CurrCamSize = camSize;
        CurrCamOffset = new Vector3(0, 0, cameraZOffset);
        CurrSmoothSpeed = smoothSpeed;
    }

    void FixedUpdate() {
        //Debug.Log("currSize " + CurrCamSize);
        //Debug.Log("curr offset " + CurrCamOffset);

        Camera.main.orthographicSize = CurrCamSize;

        Vector3 dPos = target.transform.position + CurrCamOffset;
        Vector3 sPos = Vector3.Lerp(transform.position, dPos, CurrSmoothSpeed);
        transform.position = sPos;

        //Debug.Log(CurrSmoothSpeed);
        //transform.LookAt(target);

    }
}