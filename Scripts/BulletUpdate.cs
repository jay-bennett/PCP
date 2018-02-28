using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpdate : MonoBehaviour {

    public bool goLeft = false;

    bool hitPlayer = false;

	void OnTriggerEnter2D(Collider2D col)
    {
        if( (gameObject.name.Contains("Up") || gameObject.name.Contains("Enemy")) && col.name == "Player" && !hitPlayer && !Globals.paused)
        {
            //Globals.health -= 1;
            FindObjectOfType<PlayerController>().hurtPlayer();
            hitPlayer = true;
            //if (Globals.health == 0) FindObjectOfType<PlayerController>().killPlayer();
        }
    }
}
