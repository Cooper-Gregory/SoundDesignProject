using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameplayUI_abilitySlots : MonoBehaviour
{
    public GameObject playerSettings;
    public Image slotIcon;
    public int slotNumber;
    public int slotPID;

    public Sprite swordSprite;
    public Sprite exploSprite;
    public Sprite gunSprite;

    public void Start()
    {
        playerSettings = GameObject.Find("playerSettings");
        if (slotPID == 0)
        {
            if (slotNumber != null)
            {
                if (playerSettings.GetComponent<playerSettings>().p0_attacks_simple[slotNumber] == 0)
                {
                    slotIcon.sprite = swordSprite;
                }
                else if (playerSettings.GetComponent<playerSettings>().p0_attacks_simple[slotNumber] == 1)
                {
                    slotIcon.sprite = exploSprite;
                }
                else if (playerSettings.GetComponent<playerSettings>().p0_attacks_simple[slotNumber] == 2)
                {
                    slotIcon.sprite = gunSprite;
                }
                else
                {
                    Debug.LogWarning("What attack iz dat?");
                }
            }
            else
            {
                Debug.LogWarning("Oopsie daisy, you didn't apply a slotNumber!");
            }
        }
        else if (slotPID == 1)
        {
            if (slotNumber != null)
            {
                if (playerSettings.GetComponent<playerSettings>().p1_attacks_simple[slotNumber] == 0)
                {
                    slotIcon.sprite = swordSprite;
                }
                else if (playerSettings.GetComponent<playerSettings>().p1_attacks_simple[slotNumber] == 1)
                {
                    slotIcon.sprite = exploSprite;
                }
                else if (playerSettings.GetComponent<playerSettings>().p1_attacks_simple[slotNumber] == 2)
                {
                    slotIcon.sprite = gunSprite;
                }
                else
                {
                    Debug.LogWarning("What attack iz dat?");
                }
            }
            else
            {
                Debug.LogWarning("Oopsie daisy, you didn't apply a slotNumber!");
            }
        }
    }
}
