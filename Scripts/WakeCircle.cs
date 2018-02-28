using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeCircle : MonoBehaviour {

    public GameObject enemy;

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player" && !Globals.paused)
        {
            enemy.GetComponent<EnemyController>().wake();
            Destroy(gameObject);
        }
    }
}
