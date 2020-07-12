using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private float startingDistanceToFinish;
    private float currDistToFinish;
    
    public ShipHandler ship;
    public FinishLine finish;
    public Image distanceImage;

    public GameObject mainUI;
    public GameObject winScreen;


    private IEnumerator currDistCoroutine;
 

    public void OnWin() {   
        Debug.Log("reached finish line");
        StopCoroutine(currDistCoroutine);
        
        
        mainUI.SetActive(false) ;               winScreen.transform.parent = null;

        winScreen.SetActive(true);


    }

    public void OnDeath() {
        StopCoroutine(currDistCoroutine);
        //display UI
        
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Start() {
        startingDistanceToFinish = Mathf.Abs(transform.position.y - ship.transform.position.y);
        currDistCoroutine = distanceToFinish();
        StartCoroutine(currDistCoroutine);
    }

    //use a bar, revere it
    private IEnumerator distanceToFinish() {
        while (true) {
            currDistToFinish = Mathf.Abs(transform.position.y - ship.transform.position.y);
            float tempVal = Mathf.Abs(currDistToFinish - startingDistanceToFinish);
            distanceImage.fillAmount = tempVal / startingDistanceToFinish;
            yield return new WaitForSeconds(.1f);
        }
    }

}
