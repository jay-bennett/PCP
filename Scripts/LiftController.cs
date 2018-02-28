using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour {

    public bool goDown = false;
    public bool go = true;
    public float howFar = 0f;
    public float moveSpeed = 3f;

    Vector2 origPos;

    void Start()
    {
        origPos = GetComponent<Transform>().position;
    }

    void Update()
    {
        if (!Globals.paused)
        {
            if (go)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

                if (goDown) GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.75f);
                else GetComponent<Rigidbody2D>().velocity = new Vector2(0, moveSpeed);
            }
            else GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            if (howFar != 0)
            {

                if (!goDown && GetComponent<Transform>().position.y >= origPos.y + howFar) GetComponent<Transform>().position = origPos;
                else if (goDown && GetComponent<Transform>().position.y <= origPos.y - howFar) GetComponent<Transform>().position = origPos;


            }
        }else
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.collider.name == "Player" && !Globals.paused) FindObjectOfType<PlayerController>().setVelocity(0, 0);
    }
}
