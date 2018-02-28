using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinHandler : MonoBehaviour
{

    public Sprite activeSprite;
    float timer = 0f;
    bool startTimer = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player" && !Globals.paused)
        {
            GetComponent<SpriteRenderer>().sprite = activeSprite;
            GameObject.Find("Music Controller").GetComponent<AudioSource>().Stop();
            FindObjectOfType<PlayerController>().freezeNotY();
            FindObjectOfType<PlayerController>().setDefaultSprite();
            Globals.paused = true;
            GameObject.Find("Win Controller").GetComponent<AudioSource>().Play();
            startTimer = true;

        }
    }

    void Update()
    {
        if(startTimer)
        {
            timer += Time.deltaTime;

            if(timer >= 2.25f)
            {
                startTimer = false;
                FindObjectOfType<PlayerController>().unfreeze();
                if((Globals.currentLevel + 1) != 3) GameObject.Find("Music Controller").GetComponent<AudioSource>().Play();
                Globals.paused = false;
                Globals.loadNextLevel();
            }
        }
    }
}
