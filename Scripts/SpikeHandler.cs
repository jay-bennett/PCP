using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHandler : MonoBehaviour {

	void OnCollisionStay2D(Collision2D col)
    {
        if(col.collider.name == "Player" && !Globals.paused) FindObjectOfType<PlayerController>().hurtPlayer();
    }
}
