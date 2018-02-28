using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayBlockHandler : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            GetComponentInParent<BoxCollider2D>().isTrigger = false;
            gameObject.transform.parent.gameObject.AddComponent<BoxCollider2D>();
            Destroy(gameObject);
        }
    }
}
