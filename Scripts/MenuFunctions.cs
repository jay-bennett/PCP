using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour {

    public GameObject mainCanvas;
    public GameObject optionsCanvas;
    AsyncOperation game;


    void Start() {
        game = SceneManager.LoadSceneAsync("game");
        game.allowSceneActivation = false;
    }

	public void startGame() {
        game.allowSceneActivation = true;
    }

	public void exitGame() {
		Application.Quit();
	}

    public void loadOptions() {
        mainCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void loadCredits()
    {
        SceneManager.LoadScene("credits");
    }
}
