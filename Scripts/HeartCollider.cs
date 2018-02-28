using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollider : MonoBehaviour {

    public Sprite heartPickup;
    float timer = 0f;
    bool pickedUp = false;

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player" && !pickedUp && Globals.health < 3 && !Globals.paused)
        {
            Globals.health += 1;
            GetComponent<SpriteRenderer>().sprite = heartPickup;
            pickedUp = true;
            GameObject.Find("Heart Controller").GetComponent<AudioSource>().Play();
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
