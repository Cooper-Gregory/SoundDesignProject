using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class heathbarManager : MonoBehaviour
{
    public GameObject player_0;
    public GameObject player_1;
    public Slider healthBar_0;
    public Slider healthBar_1;

    public GameObject deathOverlay;
    public Text deathText;
    public string winText;

    public AudioSource damageGrunt1;
    public AudioSource damageGrunt2;

    public void OnPlayerConnected(PlayerInput player)
    {
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

    public void TakeDamage(float damage, int playerID)
    {
        if (playerID == 0)
        {
            damageGrunt1.Play();
            float maxHealth = 100.0f;
            player_0.GetComponent<playerStats>().playerHealth -= damage;
            float playerHealth = player_0.GetComponent<playerStats>().playerHealth;
            healthBar_0.value = playerHealth / maxHealth;
            Debug.Log(healthBar_0.value);
            player_0.GetComponent<mobileHealthbarUpdater>().UpdateHealthbar(healthBar_0.value);
            //Here's where players take damage, so you could totally put damage audio effects here too.
            if (player_0.GetComponent<playerStats>().playerHealth <= 0.0f)
            {
                player_0.SetActive(false);
                deathOverlay.SetActive(true);
                winText = $"{player_1.GetComponent<playerNamePicker>().defaultPlayerName} has Won!";
                deathText.text = winText;
                //This is where players die, so you could put audio here if you want.
            }
        }
        else if (playerID == 1)
        {
            damageGrunt2.Play();
            float maxHealth = 100.0f;
            player_1.GetComponent<playerStats>().playerHealth -= damage;
            float playerHealth = player_1.GetComponent<playerStats>().playerHealth;
            healthBar_1.value = playerHealth / maxHealth;
            Debug.Log(healthBar_1.value);
            player_1.GetComponent<mobileHealthbarUpdater>().UpdateHealthbar(healthBar_1.value);
            //You would need to add damage effects here too...
            if (player_1.GetComponent<playerStats>().playerHealth <= 0.0f)
            {
                player_1.SetActive(false);
                deathOverlay.SetActive(true);
                winText = $"{player_0.GetComponent<playerNamePicker>().defaultPlayerName} has Won!";
                deathText.text = winText;
                //...and death effects here, if that's how you handle things.
            }
        }
        else
        {
            Debug.LogWarning("Unknown player took damage");
        }
    }
    public void HealPlayer(float healing, int playerID)
    {
        //This is basically an inverse of the TakeDamage() function, so placing healing effects here too would work correctly.
        if (playerID == 0)
        {
            float maxHealth = 100.0f;
            player_0.GetComponent<playerStats>().playerHealth += healing;
            float playerHealth = player_0.GetComponent<playerStats>().playerHealth;
            healthBar_0.value = playerHealth / maxHealth;
            Debug.Log(healthBar_0.value);
            player_0.GetComponent<mobileHealthbarUpdater>().UpdateHealthbar(healthBar_0.value);
            if (player_0.GetComponent<playerStats>().playerHealth > 100.0f)
            {
                player_0.GetComponent<playerStats>().playerHealth = 100.0f;
            }
        }
        else if (playerID == 1)
        {
            float maxHealth = 100.0f;
            player_1.GetComponent<playerStats>().playerHealth += healing;
            float playerHealth = player_1.GetComponent<playerStats>().playerHealth;
            healthBar_1.value = playerHealth / maxHealth;
            Debug.Log(healthBar_1.value);
            player_1.GetComponent<mobileHealthbarUpdater>().UpdateHealthbar(healthBar_1.value);
            if (player_1.GetComponent<playerStats>().playerHealth > 100.0f)
            {
                player_1.GetComponent<playerStats>().playerHealth = 100.0f;
            }
        }
        else
        {
            Debug.LogWarning("Unknown player took damage");
        }
    }
}
