using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splashscreen : MonoBehaviour {

    public GameObject mainCanvas;
    bool fadedIn = false;
    float timer = 0f;

    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        Application.targetFrameRate = 144;

        if(Globals.splashPlayed)
        {
            mainCanvas.SetActive(true);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    void Update() {
        timer += Time.deltaTime;
        if (GetComponent<CanvasGroup>().alpha != 1f && !fadedIn)
        {
            GetComponent<CanvasGroup>().alpha += Time.deltaTime * (1 / 3f);
        }

        if(timer >= 6.0f) {
            fadedIn = true;
            GetComponent<CanvasGroup>().alpha += -Time.deltaTime * (1 / 3f);
        }

        if(timer >= 6.0f && GetComponent<CanvasGroup>().alpha == 0) {
            mainCanvas.SetActive(true);
            Globals.splashPlayed = true;
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}