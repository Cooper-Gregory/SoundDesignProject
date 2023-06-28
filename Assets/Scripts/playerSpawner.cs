using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerSpawner : MonoBehaviour
{
    public GameObject player_0;
    public GameObject player_1;
    public GameObject player_0_spawn;
    public GameObject player_1_spawn;

    public void OnPlayerConnected(PlayerInput player)
    {
        //You could put a sound effect for players joining the game in here
        if (player.GetComponent<playerStats>().playerID == 0)
        {
            player_0 = player.gameObject;
        }
        else if (player.GetComponent<playerStats>().playerID == 1)
        {
            player_1 = player.gameObject;
        }
        else
        {
            Debug.LogWarning("Unknown player detected.");
        }
    }

    public void PlayerSpawn()
    {
        if (player_0 != null)
        {
            player_0.transform.position = player_0_spawn.transform.position;
        }
        if (player_1 != null)
        {
            player_1.transform.position = player_1_spawn.transform.position;
        }
    }
}
