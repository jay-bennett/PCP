using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour {

    public Sprite activeSprite;
    bool active = false;

    public Vector2 spawnLocation;

    void Start() {
        spawnLocation = GetComponent<Transform>().position;
    }

	void OnTriggerEnter2D(Collider2D col) {
        if (!active && col.name == "Player" && !Globals.paused) {
            active = true;
            FindObjectOfType<PlayerController>().currentCheckpoint = gameObject;
            GetComponent<SpriteRenderer>().sprite = activeSprite;
            GameObject.Find("Checkpoint Controller").GetComponent<AudioSource>().Play();
        }
        
    }
}
