using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {

    public Canvas pauseCanvas;
    public Canvas goCanvas;
    public GameObject player;

	public void togglePause(bool gameOver = false)
    {
        Globals.paused = !Globals.paused;

        if (gameOver)
        {
            goCanvas.enabled = !goCanvas.enabled;
            foreach (Button x in goCanvas.GetComponentsInChildren<Button>()) x.enabled = !x.enabled;
        }
        else
        {
            pauseCanvas.enabled = !pauseCanvas.enabled;
            foreach (Button x in pauseCanvas.GetComponentsInChildren<Button>()) x.enabled = !x.enabled;
        }


        if (Globals.paused)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            Globals.jumped = true;
            FindObjectOfType<PlayerController>().setVelocity(0, -0.1f);
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene("game");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("main");
    }

    
}
