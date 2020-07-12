using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public List<Transform> backgrounds;
    public float pScale;
    public float prf;
    public float smoothing;

    private Transform camTransform;
    private Vector3 previousCamPosition;

    void Start() {
        camTransform = Camera.main.transform;
        previousCamPosition = camTransform.position;
    }

    void Update() {
        float parallax = (previousCamPosition.x - camTransform.position.x) * pScale;

        for(int i = 0; i < backgrounds.Count; ++i){
            float bgTargetPosX = backgrounds[i].position.x + parallax * (i * prf + 1);
            Vector3 bgTargetPos = new Vector3(bgTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, bgTargetPos, smoothing * Time.deltaTime);
            previousCamPosition = camTransform.position;
        }
    }
    
}
