using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heathPack : MonoBehaviour
{
    public GameObject gameManager;

    public void OnAwake()
    {
        GameObject gameManager = GameObject.Find("gameManager");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            int otherID = other.gameObject.GetComponent<playerStats>().playerID;
            gameManager.GetComponent<heathbarManager>().HealPlayer(30.0f, otherID);
            gameObject.SetActive(false);
            //I'd put any health pack audio within HealPlayer() under heathbarManager, but feel free to put it here if that's how you fly.
        }
    }
}
