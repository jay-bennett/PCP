using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DragHandler : MonoBehaviour {
    public GameObject slot;
    public AudioSource clickAudio;

    Vector2 origPos;

    float x1;
    float x2;
    float slotX1;
    float slotX2;

    float y1;
    float y2;
    float slotY1;
    float slotY2;

    float width;
    float slotWidth;

    float height;
    float slotHeight;

    public bool moved = false;

    public GameObject otherObject;
    public GameObject buttonFunctions;

	// Use this for initialization
	void Start () {

        origPos = gameObject.transform.position;
        width = gameObject.GetComponent<RectTransform>().rect.width;
        height = gameObject.GetComponent<RectTransform>().rect.height;


        if (slot != null)
        {
            slotWidth = slot.GetComponent<RectTransform>().rect.width;
            slotHeight = gameObject.GetComponent<RectTransform>().rect.height;

            slotX1 = slot.transform.position.x - (slotWidth / 2);
            slotX2 = slot.transform.position.x + (slotWidth / 2);

            slotY1 = slot.transform.position.y - (slotHeight / 2);
            slotY2 = slot.transform.position.y + (slotHeight / 2);
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (moved == false)
        {
            if (gameObject.transform.position != slot.transform.position)
            {
                x1 = gameObject.transform.position.x - (width / 2);
                x2 = gameObject.transform.position.x + (width / 2);

                y1 = gameObject.transform.position.y - (height / 2);
                y2 = gameObject.transform.position.y + (height / 2);
                if (Globals.mouseObjectInUse == null || Globals.mouseObjectInUse == gameObject)
                {
                    if (Input.mousePosition.x >= x1 && Input.mousePosition.x <= x2 && Input.mousePosition.y >= y1 && Input.mousePosition.y <= y2)
                    {
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            Globals.mouseObjectInUse = gameObject;

                            if (Input.mousePosition.x >= slotX1 && Input.mousePosition.x <= slotX2 && Input.mousePosition.y >= slotY1 && Input.mousePosition.y <= slotY2)
                            {
                                Color32 color = new Color32(0, 0, 0, 45);
                                slot.GetComponent<Image>().color = color;
                            }
                            else
                            {
                                Color32 color = new Color32(0, 0, 0, 18);
                                slot.GetComponent<Image>().color = color;
                            }
                        }
                        else
                        {
                            Globals.mouseObjectInUse = null;

                            if (Input.mousePosition.x >= slotX1 && Input.mousePosition.x <= slotX2 && Input.mousePosition.y >= slotY1 && Input.mousePosition.y <= slotY2)
                            {
                                gameObject.transform.position = slot.transform.position;
                                clickAudio.Play();
                                moved = true;

                                if (gameObject.name == "RAM") buttonFunctions.GetComponent<PCButtonFunctions>().disableRAM();
                                else buttonFunctions.GetComponent<PCButtonFunctions>().disableCPU();

                                if(otherObject.GetComponent<DragHandler>().moved) buttonFunctions.GetComponent<PCButtonFunctions>().enableStartGameButton();
                            }
                            else
                            {
                                gameObject.transform.position = origPos;
                            }
                        }
                    }
                }

                if (Globals.mouseObjectInUse == gameObject)
                {
                    gameObject.transform.position = Input.mousePosition;
                }
            }
        }
    }
}
