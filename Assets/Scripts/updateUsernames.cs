using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateUsernames : MonoBehaviour
{
    
    public void UpdatePlayerNames()
    {
        GameObject player_0 = gameObject.GetComponent<heathbarManager>().player_0;
        GameObject player_1 = gameObject.GetComponent<heathbarManager>().player_1;

        player_0.GetComponent<playerNamePicker>().UpdateName();
        player_1.GetComponent<playerNamePicker>().UpdateName();
    }
}
