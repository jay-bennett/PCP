using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCButtonFunctions : MonoBehaviour {

    public AudioSource toolboxAudio;
    public AudioSource powerupAudio;

    public GameObject moneyText;

    public GameObject csText;
    public GameObject coreText;
    public GameObject ramText;
    public GameObject ramSpeedText;

    public GameObject csCostText;
    public GameObject coreCostText;
    public GameObject ramCostText;
    public GameObject ramSpeedCostText;

    public GameObject cpu;
    public GameObject ram;

    public Sprite cpu2;
    public Sprite cpu3;

    public Sprite ram2;
    public Sprite ram3;

    public GameObject csArrow;
    public GameObject coreArrow;
    public GameObject ramArrow;
    public GameObject ramSpeedArrow;

    public GameObject separator;

    public GameObject goButton;

    public GameObject buildAPCObject;

    public GameObject[] maingameObjects;

    public GameObject csExtraText;
    public GameObject coreExtraText;
    public GameObject ramExtraText;
    public GameObject ramSpeedExtraText;

    public GameObject cpuSlot;
    public GameObject ramSlot;

    float csCost = 0f;
    float coreCost = 0f;
    float ramCost = 0f;
    float ramSpeedCost = 0f;

    Dictionary<float, float> clockSpeeds = new Dictionary<float, float>();
    Dictionary<int, float> ramSpeeds = new Dictionary<int, float>();

    void Start()
    {

        clockSpeeds.Add(1.0f, 8.5f);
        clockSpeeds.Add(1.5f, 9.5f);
        clockSpeeds.Add(2.0f, 10.5f);
        clockSpeeds.Add(2.5f, 11.5f);
        clockSpeeds.Add(3.0f, 12.5f);
        clockSpeeds.Add(3.5f, 13.5f);
        clockSpeeds.Add(4.0f, 14.5f);

        ramSpeeds.Add(800, 9.5f);
        ramSpeeds.Add(1333, 10.5f);
        ramSpeeds.Add(1600, 11.5f);
        ramSpeeds.Add(1866, 12.5f);
        ramSpeeds.Add(2133, 13.5f);
        ramSpeeds.Add(2400, 14.5f);
        ramSpeeds.Add(3200, 15.5f);


    }

    void Update() {
        refreshValues();
        refreshCosts();
    }


    void refreshCosts()
    {

        if (Globals.clockSpeed == 1.0f) csCost = 10f;
        if (Globals.clockSpeed == 1.5f) csCost = 20f;
        if (Globals.clockSpeed == 2.0f) csCost = 30f;
        if (Globals.clockSpeed == 2.5f) csCost = 40f;
        if (Globals.clockSpeed == 3.0f) csCost = 50f;
        if (Globals.clockSpeed == 3.5f) csCost = 60f;
        if (Globals.clockSpeed == 4.0f) csCost = 70f;
        csCostText.GetComponent<Text>().text = "$" + csCost;

        if (Globals.coreCount == 1) coreCost = 50f;
        if (Globals.coreCount == 2) coreCost = 75f;
        if (Globals.coreCount == 3) coreCost = 100f;
        if (Globals.coreCount == 4) coreCost = 125f;
        coreCostText.GetComponent<Text>().text = "$" + coreCost;

        if (Globals.ramCapacity == 2) ramCost = 10f;
        if (Globals.ramCapacity == 4) ramCost = 20f;
        if (Globals.ramCapacity == 6) ramCost = 30f;
        if (Globals.ramCapacity == 8) ramCost = 40f;
        if (Globals.ramCapacity == 12) ramCost = 50f;
        if (Globals.ramCapacity == 14) ramCost = 60f;
        if (Globals.ramCapacity == 16) ramCost = 70f;
        ramCostText.GetComponent<Text>().text = "$" + ramCost;

        if (Globals.ramSpeed == 800) ramSpeedCost = 10f;
        if (Globals.ramSpeed == 1333) ramSpeedCost = 20f;
        if (Globals.ramSpeed == 1600) ramSpeedCost = 30f;
        if (Globals.ramSpeed == 1866) ramSpeedCost = 40f;
        if (Globals.ramSpeed == 2133) ramSpeedCost = 50f;
        if (Globals.ramSpeed == 2400) ramSpeedCost = 60f;
        if (Globals.ramSpeed == 3200) ramSpeedCost = 70f;
        ramSpeedCostText.GetComponent<Text>().text = "$" + ramSpeedCost;
    }

    void refreshValues()
    {
        moneyText.GetComponent<Text>().text = "Money: $" + Globals.money;

        if (Globals.clockSpeed.ToString()[Globals.clockSpeed.ToString().Length - 1] != '5')
        {
            csText.GetComponent<Text>().text = "Clock Speed: " + Globals.clockSpeed + ".0GHz\n(Movement Speed)";
        }
        else
        {
            csText.GetComponent<Text>().text = "Clock Speed: " + Globals.clockSpeed + "GHz\n(Movement Speed)";
        }
        coreText.GetComponent<Text>().text = "Cores: " + Globals.coreCount + "\n(Damage Per Bullet)";
        ramText.GetComponent<Text>().text = "RAM Size: " + Globals.ramCapacity + "GB\n(Bullets at a time)";
        ramSpeedText.GetComponent<Text>().text = "RAM Speed: " + Globals.ramSpeed + "MHz\n(Bullet Speed)";
    }

    public void upgradeClockSpeed() {
        float cost = csCost;

        if(Globals.money >= cost)
        {
            powerupAudio.Play();
            Globals.money -= (int) cost;

            if (Globals.clockSpeed == 1.0f) { Globals.clockSpeed = 1.5f; Globals.moveSpeed = clockSpeeds[1.5f]; }
            else if (Globals.clockSpeed == 1.5f) { Globals.clockSpeed = 2.0f; Globals.moveSpeed = clockSpeeds[2.0f]; }
            else if (Globals.clockSpeed == 2.0f) { Globals.clockSpeed = 2.5f; Globals.moveSpeed = clockSpeeds[2.5f]; }
            else if (Globals.clockSpeed == 2.5f) { Globals.clockSpeed = 3.0f; Globals.moveSpeed = clockSpeeds[3.0f]; }
            else if (Globals.clockSpeed == 3.0f) { Globals.clockSpeed = 3.5f; Globals.moveSpeed = clockSpeeds[3.5f]; }
            else if (Globals.clockSpeed == 3.5f) {
                Globals.clockSpeed = 4.0f;
                Globals.moveSpeed = clockSpeeds[4.0f];
                csArrow.GetComponent<Image>().color = new Color32(101, 101, 101, 255);
                csArrow.GetComponent<Button>().enabled = false;
                csCostText.SetActive(false);
            }

        }
        
    }

    public void upgradeCores()
    {
        float cost = coreCost;

        if (Globals.money >= cost)
        {
            powerupAudio.Play();
            Globals.money -= (int)cost;

            if (Globals.coreCount == 1) { Globals.coreCount = 2; cpu.GetComponent<Image>().sprite = cpu2; }
            else if (Globals.coreCount == 2) Globals.coreCount = 3;
            else if (Globals.coreCount == 3)
            {
                Globals.coreCount = 4;
                cpu.GetComponent<Image>().sprite = cpu3;
                coreArrow.GetComponent<Image>().color = new Color32(101, 101, 101, 255);
                coreArrow.GetComponent<Button>().enabled = false;
                coreCostText.SetActive(false);

            }

        }

    }

    public void upgradeRAMCapacity()
    {
        float cost = ramCost;

        if (Globals.money >= cost)
        {
            powerupAudio.Play();
            Globals.money -= (int)cost;

            if (Globals.ramCapacity == 2) Globals.ramCapacity = 4;
            else if (Globals.ramCapacity == 4) Globals.ramCapacity = 6;
            else if (Globals.ramCapacity == 6) { Globals.ramCapacity = 8; ram.GetComponent<Image>().sprite = ram2; }
            else if (Globals.ramCapacity == 8) Globals.ramCapacity = 12;
            else if (Globals.ramCapacity == 12) Globals.ramCapacity = 14;
            else if (Globals.ramCapacity == 14) {
                Globals.ramCapacity = 16;
                ram.GetComponent<Image>().sprite = ram3;
                ramArrow.GetComponent<Image>().color = new Color32(101, 101, 101, 255);
                ramArrow.GetComponent<Button>().enabled = false;
                ramCostText.SetActive(false);
            }

        }
    }

    public void upgradeRAMSpeed()
    {
        float cost = ramSpeedCost;

        if (Globals.money >= cost)
        {
            powerupAudio.Play();
            Globals.money -= (int)cost;

            if (Globals.ramSpeed == 800) { Globals.ramSpeed = 1333; Globals.bulletSpeed = ramSpeeds[1333]; }
            else if (Globals.ramSpeed == 1333) { Globals.ramSpeed = 1600; Globals.bulletSpeed = ramSpeeds[1600]; }
            else if (Globals.ramSpeed == 1600) { Globals.ramSpeed = 1866; Globals.bulletSpeed = ramSpeeds[1866]; }
            else if (Globals.ramSpeed == 1866) { Globals.ramSpeed = 2133; Globals.bulletSpeed = ramSpeeds[2133]; }
            else if (Globals.ramSpeed == 2133) { Globals.ramSpeed = 2400; Globals.bulletSpeed = ramSpeeds[2400]; }
            else if (Globals.ramSpeed == 2400) {
                Globals.ramSpeed = 3200;
                Globals.bulletSpeed = ramSpeeds[3200];
                ramSpeedArrow.GetComponent<Image>().color = new Color32(101, 101, 101, 255);
                ramSpeedArrow.GetComponent<Button>().enabled = false;
                ramSpeedCostText.SetActive(false);
            }

        }
    }

    public void enableStartGameButton()
    {
        csText.SetActive(false);
        coreText.SetActive(false);
        ramText.SetActive(false);
        ramSpeedText.SetActive(false);

        csArrow.SetActive(false);
        coreArrow.SetActive(false);
        ramArrow.SetActive(false);
        ramSpeedArrow.SetActive(false);

        csCostText.SetActive(false);
        coreCostText.SetActive(false);
        ramCostText.SetActive(false);
        ramSpeedCostText.SetActive(false);

        csExtraText.SetActive(false);
        coreExtraText.SetActive(false);
        ramExtraText.SetActive(false);
        ramSpeedExtraText.SetActive(false);

        separator.SetActive(false);

        goButton.SetActive(true);
    }

   public void go()
    {
        toolboxAudio.Play();
        buildAPCObject.SetActive(false);
        foreach(GameObject x in maingameObjects) { x.SetActive(true); }
        Globals.paused = false;
    }

    public void disableCPU()
    {
        csArrow.GetComponent<Image>().color = new Color32(101, 101, 101, 255);
        csArrow.GetComponent<Button>().enabled = false;
        csCostText.SetActive(false);

        coreArrow.GetComponent<Image>().color = new Color32(101, 101, 101, 255);
        coreArrow.GetComponent<Button>().enabled = false;
        coreCostText.SetActive(false);
    }

    public void disableRAM()
    {
        ramArrow.GetComponent<Image>().color = new Color32(101, 101, 101, 255);
        ramArrow.GetComponent<Button>().enabled = false;
        ramCostText.SetActive(false);

        ramSpeedArrow.GetComponent<Image>().color = new Color32(101, 101, 101, 255);
        ramSpeedArrow.GetComponent<Button>().enabled = false;
        ramSpeedCostText.SetActive(false);
    }

    public void reset()
    {
        goButton.SetActive(false);

        cpu.GetComponent<Transform>().localPosition = new Vector2(21f, -14f);
        ram.GetComponent<Transform>().localPosition = new Vector2(461f, -13.9f);

        cpu.GetComponent<DragHandler>().moved = false;
        ram.GetComponent<DragHandler>().moved = false;

        csText.SetActive(true);
        csExtraText.SetActive(true);
        coreText.SetActive(true);
        coreExtraText.SetActive(true);
        ramText.SetActive(true);
        ramExtraText.SetActive(true);
        ramSpeedText.SetActive(true);
        ramSpeedExtraText.SetActive(true);

        csArrow.SetActive(true);
        coreArrow.SetActive(true);
        ramArrow.SetActive(true);
        ramSpeedArrow.SetActive(true);

        cpuSlot.GetComponent<Image>().color = new Color32(0, 0, 0, 18);
        ramSlot.GetComponent<Image>().color = new Color32(0, 0, 0, 18);

        if(Globals.clockSpeed != 4.0f) {
            csCostText.SetActive(true);
            csArrow.GetComponent<Button>().enabled = true;
            csArrow.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        if(Globals.coreCount != 4) {
            coreCostText.SetActive(true);
            coreArrow.GetComponent<Button>().enabled = true;
            coreArrow.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        if(Globals.ramCapacity != 16) {
            ramCostText.SetActive(true);
            ramArrow.GetComponent<Button>().enabled = true;
            ramArrow.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        if(Globals.ramSpeed != 3200) {
            ramSpeedCostText.SetActive(true);
            ramSpeedArrow.GetComponent<Button>().enabled = true;
            ramSpeedArrow.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        
    }

}
