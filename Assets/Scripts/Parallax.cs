using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour{

    private float sizeX;
    private float sizeY;
    private float startPosX;
    private float startPosY;

    private Camera cam;

    public float Plax;

    void Start() {
        cam = Camera.main;
        startPosY = transform.position.y;
        startPosX = transform.position.x;
        sizeY = GetComponent<SpriteRenderer>().bounds.size.y;
        sizeX = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update() {
        float tempY = cam.transform.position.y * (1 - Plax);
        float tempX = cam.transform.position.x * (1 - Plax);
        float distanceY = cam.transform.position.y * Plax;
        float distanceX = cam.transform.position.x * Plax;
        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);
        
       // Debug.Log(temp + " vs " + (startPos + size));

        if(tempY > startPosY + sizeY) {
           // Debug.Log("expand");
            startPosY += sizeY;
        }
        else if (tempY < startPosY - sizeY) {
              //  Debug.Log("expand");

            startPosY -= sizeY;
        }

        if(tempX > startPosX + sizeX) {
           // Debug.Log("expand");
            startPosX += sizeX;
        }
        else if (tempX < startPosX - sizeX) {
              //  Debug.Log("expand");

            startPosX -= sizeX;
        }

    
    }

}