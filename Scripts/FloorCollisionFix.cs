using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollisionFix : MonoBehaviour {

    public GameObject player;

	void OnCollisionEnter2D() {
        player.GetComponent<PlayerController>().setVelocity(player.GetComponent<PlayerController>().getVelX(), 0);
    }

    void OnTriggerEnter2D()
    {
        player.GetComponent<PlayerController>().setVelocity(player.GetComponent<PlayerController>().getVelX(), 0);
    }
}
