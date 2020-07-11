using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameManager manager;
    void OnTriggerEnter() { //upon reaching the finish zone, do a thing
        manager.OnWin();
    }
}
