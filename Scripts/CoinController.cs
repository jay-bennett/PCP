using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    public Sprite coinPickup;
    public AudioSource coinAudio;
    float timer = 0f;
    bool pickedUp = false;

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player" && !pickedUp && !Globals.paused)
        {
            Globals.money += 5;
            GetComponent<SpriteRenderer>().sprite = coinPickup;
            pickedUp = true;
            coinAudio.Play();
        }
    }

    void Update() {
        if(pickedUp && !Globals.paused)
        {
            timer += Time.deltaTime;
            if(timer >= 0.2f)
            {
                Destroy(gameObject);
            }
        }
    }
}
