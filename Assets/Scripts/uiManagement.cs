using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManagement : MonoBehaviour
{
    [Header("Selected Slots")]
    public bool p1s1 = false;
    public bool p1s2 = false;
    public bool p1s3 = false;

    public bool p2s1 = false;
    public bool p2s2 = false;
    public bool p2s3 = false;
    public GameObject playButton;


    [Header("Player Settings Object")]
    public GameObject playerSettings;

    [Header("Custom Name UIs")]
    public GameObject player1UIInput;
    public GameObject player2UIInput;

    [Header("Player 1 Ability Slots")]
    public GameObject player1Slot1Dropdown;
    public GameObject player1Slot2Dropdown;
    public GameObject player1Slot3Dropdown;

    [Header("Player 2 Ability Slots")]
    public GameObject player2Slot1Dropdown;
    public GameObject player2Slot2Dropdown;
    public GameObject player2Slot3Dropdown;

    public void OnPlayer1NameUpdated()
    {
        playerSettings = GameObject.Find("playerSettings");
        playerSettings.GetComponent<playerSettings>().customPlayer0Name = player1UIInput.GetComponent<InputField>().text;
    }
    public void OnPlayer2NameUpdated()
    {
        playerSettings = GameObject.Find("playerSettings");
        playerSettings.GetComponent<playerSettings>().customPlayer1Name = player2UIInput.GetComponent<InputField>().text;
    }

    public void OnPlayer1AbilityChoice(int slotNumber)
    {
        //God forgive me for this hack, for I have committed great sin on this unholy day
        if (slotNumber == 0)
        {
            if (player1Slot1Dropdown.GetComponent<Dropdown>().value == 0)
            {
                p1s1 = false;
            }
            else
            {
                gameObject.GetComponent<setAttackData>().UpdateAttackData(0, slotNumber, player1Slot1Dropdown.GetComponent<Dropdown>().value - 1);
                p1s1 = true;
            }
        }
        if (slotNumber == 1)
        {
            if (player1Slot2Dropdown.GetComponent<Dropdown>().value == 0)
            {
                p1s2 = false;
            }
            else
            {
                gameObject.GetComponent<setAttackData>().UpdateAttackData(0, slotNumber, player1Slot2Dropdown.GetComponent<Dropdown>().value - 1);
                p1s2 = true;
            }
        }
        if (slotNumber == 2)
        {
            if (player1Slot3Dropdown.GetComponent<Dropdown>().value == 0)
            {
                p1s3 = false;
            }
            else
            {
                gameObject.GetComponent<setAttackData>().UpdateAttackData(0, slotNumber, player1Slot3Dropdown.GetComponent<Dropdown>().value - 1);
                p1s3 = true;
            }
        }
        playButtonUpdate();
    }

    public void OnPlayer2AbilityChoice(int slotNumber)
    {
        //God forgive me for this hack, for I have committed great sin on this unholy day 2 electric boogaloo
        if (slotNumber == 0)
        {
            if (player2Slot1Dropdown.GetComponent<Dropdown>().value == 0)
            {
                p2s1 = false;
            }
            else
            {
                gameObject.GetComponent<setAttackData>().UpdateAttackData(1, slotNumber, player2Slot1Dropdown.GetComponent<Dropdown>().value - 1);
                p2s1 = true;
            }
        }
        if (slotNumber == 1)
        {
            if (player2Slot2Dropdown.GetComponent<Dropdown>().value == 0)
            {
                p2s2 = false;
            }
            else
            {
                gameObject.GetComponent<setAttackData>().UpdateAttackData(1, slotNumber, player2Slot2Dropdown.GetComponent<Dropdown>().value - 1);
                p2s2 = true;
            }
        }
        if (slotNumber == 2)
        {
            if (player2Slot3Dropdown.GetComponent<Dropdown>().value == 0)
            {
                p2s3 = false;
            }
            else
            {
                gameObject.GetComponent<setAttackData>().UpdateAttackData(1, slotNumber, player2Slot3Dropdown.GetComponent<Dropdown>().value - 1);
                p2s3 = true;
            }
        }
        playButtonUpdate();
    }

    public void playButtonUpdate()
    {
        if (p1s1 && p1s2 && p1s3 && p2s1 && p2s2 && p2s3 && gameObject.GetComponent<loadScene>().sceneSelected)
        {
            playButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            playButton.GetComponent<Button>().interactable = false;
        }
    }
}
