using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishLine : MonoBehaviour
{
    public GameManager manager;
    void OnTriggerEnter2D(Collider2D col) { //upon reaching the finish zone, do a thing
        if(col.gameObject.tag == "Ship") {
            manager.OnWin();
        }
    }
}
