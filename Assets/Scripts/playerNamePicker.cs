using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerNamePicker : MonoBehaviour
{
    public string customPlayerName;
    public string defaultPlayerName;
    public Text playerNameTag;
    public Text healthbarTag;
    public GameObject player;
    public GameObject playerSettings;
    public int playerID;

    public Color playerNameColour = Color.white;

    public void Awake()
    {
        playerSettings = GameObject.Find("playerSettings");
        if (player.GetComponent<playerStats>().playerID == 0)
        {
            healthbarTag = GameObject.Find("healthbarTag_0").GetComponent<Text>();
            defaultPlayerName = "Player 1";
            playerID = 0;
            customPlayerName = playerSettings.GetComponent<playerSettings>().customPlayer0Name;
            playerNameColour = playerSettings.GetComponent<playerSettings>().customPlayer0Colour;
        }
        else
        {
            healthbarTag = GameObject.Find("healthbarTag_1").GetComponent<Text>();
            defaultPlayerName = "Player 2";
            playerID = 1;
            customPlayerName = playerSettings.GetComponent<playerSettings>().customPlayer1Name;
            playerNameColour = playerSettings.GetComponent<playerSettings>().customPlayer1Colour;
        }

        if (customPlayerName != "")
        {
            defaultPlayerName = customPlayerName;
            playerNameTag.text = customPlayerName;
            healthbarTag.text = customPlayerName;
        }
        else
        {
            playerNameTag.text = defaultPlayerName;
            healthbarTag.text = defaultPlayerName;
        }
        playerNameTag.color = playerNameColour;
        healthbarTag.color = playerNameColour;
    }

    public void UpdateName()
    {
        playerSettings = GameObject.Find("playerSettings");
        if (player.GetComponent<playerStats>().playerID == 0)
        {
            healthbarTag = GameObject.Find("healthbarTag_0").GetComponent<Text>();
            defaultPlayerName = "Player 1";
            playerID = 0;
            customPlayerName = playerSettings.GetComponent<playerSettings>().customPlayer0Name;
            playerNameColour = playerSettings.GetComponent<playerSettings>().customPlayer0Colour;
        }
        else
        {
            healthbarTag = GameObject.Find("healthbarTag_1").GetComponent<Text>();
            defaultPlayerName = "Player 2";
            playerID = 1;
            customPlayerName = playerSettings.GetComponent<playerSettings>().customPlayer1Name;
            playerNameColour = playerSettings.GetComponent<playerSettings>().customPlayer1Colour;
        }

        if (customPlayerName != "")
        {
            defaultPlayerName = customPlayerName;
            playerNameTag.text = customPlayerName;
            healthbarTag.text = customPlayerName;
        }
        else
        {
            playerNameTag.text = defaultPlayerName;
            healthbarTag.text = defaultPlayerName;
        }
        playerNameTag.color = playerNameColour;
        healthbarTag.color = playerNameColour;
    }
}
