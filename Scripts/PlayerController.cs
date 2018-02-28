using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Sprite char_0;
    public Sprite char_1;
    public Sprite char_2;

    public Vector2 getPosition() { return GetComponent<Rigidbody2D>().position; }
    public float getPosX() { return GetComponent<Rigidbody2D>().position.x; }
    public float getPosY() { return GetComponent<Rigidbody2D>().position.y; }

    public Vector2 getVelocity() { return GetComponent<Rigidbody2D>().velocity; }
    public float getVelX() { return GetComponent<Rigidbody2D>().velocity.x; }
    public float getVelY() { return GetComponent<Rigidbody2D>().velocity.y; }

    public void setPosition(float x, float y) { GetComponent<Rigidbody2D>().position = new Vector2(x, y); }
    public void setPosition(Vector2 pos) { GetComponent<Rigidbody2D>().position = pos; }
    public void setVelocity(float x, float y) { GetComponent<Rigidbody2D>().velocity = new Vector2(x, y); }
    public void setVelocity(Vector2 vel) { GetComponent<Rigidbody2D>().velocity = vel; }

    public GameObject bulletPrefabLeft;
    public GameObject bulletPrefabRight;
    public List<GameObject> bulletList = new List<GameObject>();

    public GameObject pcObject;
    public GameObject[] playerObjects;

    public GameObject currentCheckpoint;

    public GameObject velytext;

    public AudioSource jumpAudio;
    public AudioSource shootAudio;
    public AudioSource hurtAudio;
    public AudioSource deathAudio;
    public AudioSource toolboxAudio;

    Vector2 origPos;

    bool invincible;
    float timer = 0f;
    float flickerTimer = 0f;

    void Start() {
        origPos = GetComponent<Transform>().position;
        Globals.init();
    }

    void shoot()
    {
        if (GetComponent<SpriteRenderer>().sprite == char_1 || GetComponent<SpriteRenderer>().sprite == char_2)
        {
            GameObject bullet = null;

            if (GetComponent<SpriteRenderer>().sprite == char_2)
            {
                bullet = Instantiate(bulletPrefabRight);
                bullet.GetComponent<Transform>().position = new Vector2(getPosX() + 1.0f, getPosY() - 0.54f);
            }
            else if (GetComponent<SpriteRenderer>().sprite == char_1)
            {
                bullet = Instantiate(bulletPrefabLeft);
                bullet.GetComponent<Transform>().position = new Vector2(getPosX() - 1.0f, getPosY() - 0.54f);
            }

            bulletList.Add(bullet);
            shootAudio.Play();

            if (bulletList.Count > Globals.ramCapacity)
            {
                Destroy(bulletList[0]);
                bulletList.RemoveAt(0);
            }
        }

    }

    void updateBullets()
    {
        foreach(GameObject x in bulletList)
        {
            x.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            x.GetComponent<Rigidbody2D>().velocity = x.GetComponent<BulletUpdate>().goLeft ? new Vector2(-Globals.bulletSpeed, 0f) : new Vector2(Globals.bulletSpeed, 0f);
        }
    }

    void openToolbox()
    {
        toolboxAudio.Play();
        Globals.paused = true;
        pcObject.SetActive(true);
        pcObject.GetComponentInChildren<PCButtonFunctions>().reset();

        foreach(GameObject x in playerObjects) x.SetActive(false);

        foreach (GameObject x in bulletList)
        {
            x.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            x.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        gameObject.SetActive(false);
    }

    void Update()
    {

        velytext.GetComponent<Text>().text = getVelY().ToString();
        if (Input.GetKeyDown(KeyCode.Escape)) FindObjectOfType<PauseController>().togglePause();

        if (!Globals.paused)
        {

            if (invincible)
            {
                float deltaTime = Time.deltaTime;
                flickerTimer += deltaTime;
                timer += deltaTime;
                if (flickerTimer >= 0.25f)
                {
                    GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
                    flickerTimer = 0f;
                }

                if (timer >= 2.5f)
                {
                    invincible = false;
                    timer = 0f;
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }

            updateBullets();

            if (getVelY() == 0f) Globals.jumped = false;

            if (Input.GetKey(KeyCode.Space) && !Globals.jumped)
            {
                setVelocity(getVelX(), Globals.jumpHeight);
                jumpAudio.Play();
                Globals.jumped = true;
            }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                shoot();
            }

            if (Input.GetKeyDown(KeyCode.F) && Globals.atToolbox)
            {
                openToolbox();
            }

            if (Input.GetKey(KeyCode.D))
            {
                setVelocity(Globals.moveSpeed, getVelY());
                GetComponent<SpriteRenderer>().sprite = char_2;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                setVelocity(-Globals.moveSpeed, getVelY());
                GetComponent<SpriteRenderer>().sprite = char_1;
            }
            else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                setVelocity(0, getVelY());
                GetComponent<SpriteRenderer>().sprite = char_0;
            }
        }
        else
        {
            foreach(GameObject x in bulletList) {
                x.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                x.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }

    public void killPlayer() {
        deathAudio.Play();
        Globals.lives -= 1;
        Globals.health = 3;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -0.1f);
        if (currentCheckpoint == null) GetComponent<Transform>().position = origPos;
        else GetComponent<Transform>().position = currentCheckpoint.GetComponent<CheckpointHandler>().spawnLocation;

        if(Globals.lives == 0)
        {
            FindObjectOfType<PauseController>().togglePause(true);
        }
    }

    public void hurtPlayer() {
        if (!invincible) {
            Globals.health -= 1;
            
            if (Globals.health <= 0) {
                killPlayer();
            } else {
                invincible = true;
                hurtAudio.Play();
            }
        }
    }

    public void freeze()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void freezeNotY()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public void unfreeze()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void setDefaultSprite()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = char_0;
    }

    public void clearBullets()
    {
        bulletList.Clear();
    }
}