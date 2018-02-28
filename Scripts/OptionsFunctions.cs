using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsFunctions : MonoBehaviour {

    public GameObject mainCanvas;
    public GameObject optionsCanvas;

    public GameObject resText;
    public GameObject fsText;

    int width;
    int height;
    bool fullscreen;

    void Start()
    {
        width = Screen.width;
        height = Screen.height;
        fullscreen = Screen.fullScreen;
        fillButtons();
    }

    void fillButtons() {
        resText.GetComponent<Text>().text = "Resolution: " + width + "x" + height;
        fsText.GetComponent<Text>().text = "Fullscreen: " + fullscreen;
    }

    public void nextRes() {
        switch(width)
        {
            case 1152:
                width = 1280;
                height = 720;
                break;

            case 1280:
                width = 1360;
                height = 768;
                break;

            case 1360:
                width = 1366;
                height = 768;
                break;

            case 1366:
                width = 1600;
                height = 900;
                break;

            case 1600:
                width = 1776;
                height = 1000;
                break;

            case 1776:
                width = 1920;
                height = 1080;
                break;

            case 1920:
                width = 1152;
                height = 648;
                break;
        }

        fillButtons();
    }

    public void setFullscreen() {
        fullscreen = !fullscreen;
        fillButtons();
    }

    public void apply() {
        Screen.SetResolution(width, height, fullscreen);
    }

    public void exitOptions() {
        mainCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

}
