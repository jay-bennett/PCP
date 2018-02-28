using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePoint : MonoBehaviour {

    public GameObject enemy;

    void OnTriggerEnter2D(Collider2D col)
    {
        try {
            if (col.name.Contains("Enemy") && !col.name.Contains("Bullet")) enemy.GetComponent<EnemyController>().moveSpeed = -enemy.GetComponent<EnemyController>().moveSpeed;
        } catch (MissingReferenceException) { }
    }
}

