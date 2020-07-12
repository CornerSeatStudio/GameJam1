using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private float currDistToFinish;
    
    public ShipHandler ship;
    public FinishLine finish;
 

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

    public void Start() {

    }

    //use a bar, revere it
    private IEnumerator distanceToFinish() {
        while (true) {

            //calculate distance between ship and finish

            yield return new WaitForSeconds(.2f);
        }
    }

}
