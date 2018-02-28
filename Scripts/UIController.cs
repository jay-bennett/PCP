using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject moneyText;
    public GameObject livesText;
    public GameObject scoreText;
    public GameObject[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Update()
    {
        moneyText.GetComponent<Text>().text = " $" + Globals.money;
        livesText.GetComponent<Text>().text = Globals.lives.ToString();
        scoreText.GetComponent<Text>().text = "Score: " + Globals.score;

        if (Globals.health == 3) foreach (GameObject x in hearts) x.GetComponent<Image>().sprite = fullHeart;
        else if (Globals.health == 2) {
            hearts[0].GetComponent<Image>().sprite = fullHeart;
            hearts[1].GetComponent<Image>().sprite = fullHeart;
            hearts[2].GetComponent<Image>().sprite = emptyHeart;
        } else if (Globals.health == 1) {
            hearts[0].GetComponent<Image>().sprite = fullHeart;
            hearts[1].GetComponent<Image>().sprite = emptyHeart;
            hearts[2].GetComponent<Image>().sprite = emptyHeart;
        }else if(Globals.health <= 0) foreach (GameObject x in hearts) x.GetComponent<Image>().sprite = emptyHeart;
    }

    
}
