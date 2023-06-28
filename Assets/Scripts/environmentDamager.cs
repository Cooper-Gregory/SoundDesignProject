using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class environmentDamager : MonoBehaviour
{
    //I'd recommend placing damage audio within the heathbarManager.TakeDamage() function, but feel free to place it in here if you'd like.
    //If you want, you could also edit TakeDamage() to accept another argument to determine what source the damage comes from
    //Doing that would allow you to organise all audio calls within one function, while customising the different sounds that can play
    //from taking damage. Should be fairly trivial to implement if you need a hand with it.

    [Header("Attack Defaults")]
    public float attackDamage = 10.0f;
    public float attackKnockback = 0.01f;
    public float damagerCooldown = 1.0f;

    [Header("Attack Logic")]
    public Vector3 heading;
    public bool damageDealt_p0 = false;
    public bool damageDealt_p1 = false;
    public GameObject knockbackCenter;

    protected float damageDealt_p0_timer;
    protected float damageDealt_p1_timer;

    public void OnTriggerStay2D(Collider2D other)
    {
        GameObject gameManager = GameObject.Find("gameManager");

        var heading = other.transform.position - knockbackCenter.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        if (other.tag == "Player")
        {
            int otherID = other.GetComponent<playerStats>().playerID;
            if (damageDealt_p0 == false && otherID == 0)
            {
                gameManager.GetComponent<heathbarManager>().TakeDamage(attackDamage, otherID);
                damageDealt_p0 = true;
                Debug.Log($"Damaged {other.name} by {attackDamage}!");
            }
            if (damageDealt_p1 == false && otherID == 1)
            {
                gameManager.GetComponent<heathbarManager>().TakeDamage(attackDamage, otherID);
                damageDealt_p1 = true;
                Debug.Log($"Damaged {other.name} by {attackDamage}!");
            }
            other.attachedRigidbody.AddForce(direction * attackKnockback);
        }
    }
    public void Update()
    {
        if (damageDealt_p0)
        {
            damageDealt_p0_timer += Time.deltaTime;
        }
        if (damageDealt_p0_timer >= damagerCooldown)
        {
            damageDealt_p0_timer = 0.0f;
            damageDealt_p0 = false;
        }

        if (damageDealt_p1)
        {
            damageDealt_p1_timer += Time.deltaTime;
        }
        if (damageDealt_p1_timer >= damagerCooldown)
        {
            damageDealt_p1_timer = 0.0f;
            damageDealt_p1 = false;
        }
    }
}
