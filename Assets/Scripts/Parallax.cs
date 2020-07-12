using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour{

    private float size;
    private float startPos;

    private Camera cam;

    public float Plax;

    void Start() {
        cam = Camera.main;
        startPos = transform.position.y;
        size = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update() {
        float temp = cam.transform.position.y * (1 - Plax);
        float distance = cam.transform.position.y * Plax;
        transform.position = new Vector3(transform.position.x, startPos + distance, transform.position.z);
        
       // Debug.Log(temp + " vs " + (startPos + size));

        if(temp > startPos + size) {
           // Debug.Log("expand");
            startPos += size;
        }
        else if (temp < startPos - size) {
              //  Debug.Log("expand");

            startPos -= size;
        }
    
    }

}