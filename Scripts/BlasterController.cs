using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterController : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Sprite defaultSprite;
    public Sprite shootingSprite;
    public float delay = 0f;
    public AudioSource enemyShootAudio;
    bool delayed;

    List<GameObject> bullets = new List<GameObject>();
    float timer = 0f;

    void updateBullets()
    {
        foreach (GameObject x in bullets)
        {
            x.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            x.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5f);
        }
    }

    void shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.GetComponent<Transform>().position = new Vector2(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y + 0.8f);
        GetComponent<SpriteRenderer>().sprite = shootingSprite;
        bullets.Add(newBullet);
        enemyShootAudio.Play();

        if (bullets.Count > 3)
        {
            Destroy(bullets[0]);
            bullets.RemoveAt(0);
        }
    }

    void Update()
    {
        if (!Globals.paused)
        {
            timer += Time.deltaTime;

            if (timer >= delay && !delayed)
            {
                delayed = true;
                timer = 0f;
            }

            if (delayed)
            {
                updateBullets();

                if (timer >= 0.3f) GetComponent<SpriteRenderer>().sprite = defaultSprite;

                if (timer >= 1.0f)
                {
                    shoot();
                    timer = 0f;
                }
            }
        }
        else
        {
            foreach (GameObject x in bullets)
            {
                x.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                x.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0f);
            }
        }
    }
}