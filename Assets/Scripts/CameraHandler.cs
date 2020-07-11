using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public float cameraZOffset = -500;
    public float smoothSpeed = .9f;
    public Transform target;

    void Start(){
        Camera.main.orthographicSize = 10f;
    }

    void LateUpdate() {
        Vector3 dPos = new Vector3(target.transform.position.x, target.transform.position.y, cameraZOffset);
        Vector3 sPos = Vector3.Lerp(transform.position, dPos, smoothSpeed);
        transform.position = sPos;

        transform.LookAt(target);

    }
}