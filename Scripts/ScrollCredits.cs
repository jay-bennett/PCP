using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScrollCredits : MonoBehaviour
{

    public Text text;
    public GameObject text2;
    public GameObject text3;

    void Awake()
    {
            text2.SetActive(false);
            text3.SetActive(false);
    }

    void Update()
    {
        if (text.GetComponent<Transform>().position.y > Screen.height * 2.34)
        {
            text2.SetActive(true);
            text3.SetActive(true);
        }
        else if (text.GetComponent<Transform>().position.y <= Screen.height * 2.34)
        {
            text.GetComponent<Transform>().position = new Vector2(text.GetComponent<Transform>().position.x, text.GetComponent<Transform>().position.y + 0.35f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("main");
        }
    }
}