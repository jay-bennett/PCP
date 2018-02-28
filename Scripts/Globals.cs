using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour {

    public static GameObject mouseObjectInUse = null;
    public static bool splashPlayed = false;

    public static bool jumped = false;
    public static float jumpHeight = 12f;
    public static int money = 30;

    public static float moveSpeed = 8.5f;
    public static float clockSpeed = 1.0f;
    public static int coreCount = 1;
    public static int ramCapacity = 2;
    public static int ramSpeed = 800;
    public static float bulletSpeed = 9.5f;

    public static int currentLevel = 0;

    public static int health = 3;

    public static int score = 0;

    public static int lives = 3;

    public static bool atToolbox = false;

    public static bool paused = false;

    public static Dictionary<int, Vector2> levelDict = new Dictionary<int, Vector2>();

    static List<GameObject> levelList = new List<GameObject>();

    public static void init()
    {
        if (levelDict.Count == 0)
        {
            levelDict.Add(1, new Vector2(165, -13));
            levelDict.Add(2, new Vector2(300, -13));

            levelList.Add(GameObject.Find("Level 0"));
            levelList.Add(GameObject.Find("Level 1"));
            levelList.Add(GameObject.Find("Level 2"));
        }

        money = 30;
        moveSpeed = 8.5f;
        clockSpeed = 1.0f;
        coreCount = 1;
        ramCapacity = 2;
        ramSpeed = 800;
        bulletSpeed = 9.5f;

        currentLevel = 0;
        health = 3;
        score = 0;
        lives = 3;
        atToolbox = false;
        paused = false;

        levelList[0].SetActive(true);
        levelList[1].SetActive(false);
        levelList[2].SetActive(false);
    }

    //Level stuff
    public static void loadLevel(int level) {

        GameObject.Find("Level " + level.ToString()).SetActive(true);
        FindObjectOfType<PlayerController>().setPosition(levelDict[level].x, levelDict[level].y);
        health = 3;
    }

    public static void loadNextLevel() {
        currentLevel += 1;

        if (currentLevel == 3) winGame();
        else
        {
            levelList[currentLevel - 1].SetActive(false);
            levelList[currentLevel].SetActive(true);

            loadLevel(currentLevel);
            FindObjectOfType<PlayerController>().clearBullets();
        }
    }

    public static void winGame()
    {
        SceneManager.LoadScene("credits");
    }
}
