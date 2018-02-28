using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayCollider : MonoBehaviour {

    public GameObject displayText;
    public string textToDisplay;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player") {
            displayText.GetComponent<Text>().text = textToDisplay;
            displayText.SetActive(true);
            if(gameObject.name.Contains("Tool"))
                Globals.atToolbox = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            displayText.SetActive(false);
            if (gameObject.name.Contains("Tool"))
                Globals.atToolbox = false;
        }
    }
}
