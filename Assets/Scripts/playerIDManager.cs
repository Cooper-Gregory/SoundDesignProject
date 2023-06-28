using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerIDManager : MonoBehaviour
{
    List<int> playerIDList = new List<int>();
    public void OnPlayerConnected(PlayerInput player)
    {
        Debug.Log("first call!");
        playerIDList.Add(playerIDList.Count);
        Debug.Log($"second call! {playerIDList.Count} {playerIDList[playerIDList.Count - 1]}");
        player.GetComponent<playerStats>().playerID = playerIDList[playerIDList.Last()];

    }
}
