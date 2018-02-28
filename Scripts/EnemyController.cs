using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Sprite left_inactive;
    public Sprite left_shooting;
    public Sprite middle_sleep;
    public Sprite middle_awake;
    public Sprite right_inactive;
    public Sprite right_shooting;

    public Sprite poof;

    public GameObject wakeCircle;
    public GameObject[] reversePoints;
    public GameObject bulletPrefab;
    public GameObject heartPrefab;

    public Material enemyShootColour;

    public AudioSource enemyShootAudio;
    public AudioSource deathAudio;

    List<GameObject> bulletList = new List<GameObject>();

    public int health = 2;
    public float moveSpeed = 3f;

    float timer = 0f;
    float deathTimer = 0f;

    bool awake = false;
    public bool dead = false;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.name == "Player" && !Globals.paused)
        {
            FindObjectOfType<PlayerController>().hurtPlayer();
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.Contains("PlayerBullet")) {
            Destroy(col.gameObject);
            FindObjectOfType<PlayerController>().bulletList.Remove(col.gameObject);

            health -= Globals.coreCount;

            if (health == 0)
            {
                die();
            }
        }
    }

    public void wake() {
        awake = true;
    }

    void die() {
        deathAudio.Play();
        GetComponent<SpriteRenderer>().sprite = poof;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        dead = true;
    }

    void shoot(bool goLeft)
    {
        Vector2 pos = GetComponent<Transform>().position;

        GameObject bullet = Instantiate(bulletPrefab);
        bullet.name = "EnemyBullet";

        GetComponent<SpriteRenderer>().sprite = goLeft ? left_shooting : right_shooting;
        bullet.GetComponent<Transform>().position = goLeft ? new Vector2(pos.x - 1, pos.y) : new Vector2(pos.x + 1, pos.y);
        bullet.GetComponent<BulletUpdate>().goLeft = goLeft;

        enemyShootAudio.Play();

        bullet.GetComponent<MeshRenderer>().material = enemyShootColour;

        bulletList.Add(bullet);

        if (bulletList.Count > 3)
        {
            Destroy(bulletList[0]);
            bulletList.RemoveAt(0);
        }
    }

    void updateBullets() {
        foreach (GameObject x in bulletList)
        {
            x.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            x.GetComponent<Rigidbody2D>().velocity = x.GetComponent<BulletUpdate>().goLeft ? new Vector2(-5f, 0f) : new Vector2(5f, 0f);
        }
    }

    void Update()
    {
        if (!Globals.paused)
        {
            if(!dead) GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            updateBullets();

            float deltaTime = Time.deltaTime;

            timer += deltaTime;

            if (!dead)
            {
                if (awake) GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);


                if (moveSpeed < 0 && awake)
                {
                    if (timer >= 0.3f) GetComponent<SpriteRenderer>().sprite = left_inactive;
                    else GetComponent<SpriteRenderer>().sprite = left_shooting;

                    if (timer >= 1.0f)
                    {
                        shoot(true);
                        timer = 0f;
                    }
                }
                else if (moveSpeed > 0 && awake)
                {
                    if (timer >= 0.3f) GetComponent<SpriteRenderer>().sprite = right_inactive;
                    else GetComponent<SpriteRenderer>().sprite = right_shooting;

                    if (timer >= 1.0f)
                    {
                        shoot(false);
                        timer = 0f;
                    }
                }

            }
            else
            {
                deathTimer += deltaTime;
                if (deathTimer >= 0.2f)
                {
                    Globals.score += 50;

                    int x = new System.Random().Next(1, 101);

                    if(x <= 25)
                    {
                        GameObject heart = Instantiate(heartPrefab);
                        heart.GetComponent<Transform>().position = GetComponent<Transform>().position;
                    }

                    Destroy(gameObject);
                    Destroy(wakeCircle);
                }
            }
        }else
        {
            foreach(GameObject x in bulletList)
            {
                x.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                x.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }

            if (!dead)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }
    

}
