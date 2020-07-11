using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public void OnWin() {   
        Debug.Log("reached finish line");
        //display UI
        //show score
        //restart or main menu

    }

    public void OnDeath() {
        //display UI
        //restart or main menu

    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
