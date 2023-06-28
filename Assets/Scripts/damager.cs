using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damager : MonoBehaviour
{
    [Header("Attack Defaults")]
    public float attackRange = 3.0f;
    public float attackDamage = 10.0f;
    public float attackKnockback = 0.01f;
    public float friendlyFireMultiplier = 0.33f;

    [Header("Attack Settings")]
    public int playerID = 0;
    public GameObject player;
    public GameObject testDisplay;
    public GameObject knockbackCenter;
    public LayerMask playerMask;
    public GameObject opponent;

    [Header("Attack Logic")]
    public Vector3 heading;
    public bool damageDealt = false;
    public bool player2NotPresent = false;
    public bool friendlyFire = false;

    public void OnTriggerStay2D(Collider2D other)
    {
        GameObject gameManager = GameObject.Find("gameManager");
        OpponentCheck();

        var heading = other.transform.position - knockbackCenter.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        if (player2NotPresent)
        {
            player2NotPresent = false;
        }
        else
        {
            RaycastHit2D wallCheck = Physics2D.Raycast(knockbackCenter.transform.position, (opponent.transform.position - knockbackCenter.transform.position), playerMask);

            //Debug.Log($"Raycast results: {wallCheck.collider.name}");

            if (wallCheck.collider != null && wallCheck.collider == other && (other.tag == "Player" || other.tag == "PlayerAttack"))
            {
                if (other.tag == "PlayerAttack" && other.transform.parent.gameObject == opponent)
                {
                    if (damageDealt == false)
                    {
                        int otherID = opponent.GetComponent<playerStats>().playerID;
                        gameManager.GetComponent<heathbarManager>().TakeDamage(attackDamage, otherID);
                        damageDealt = true;
                        //Debug.Log($"Damaged {other.name} by {attackDamage}!");
                        //I wouldn't put audio here, probably throw it in TakeDamage() under heathbarManager.
                    }
                    opponent.GetComponent<Rigidbody2D>().AddForce(direction * attackKnockback);
                }
                else if (!(other.GetComponent<playerStats>().playerID == playerID))
                {
                    if (damageDealt == false)
                    {
                        int otherID = other.GetComponent<playerStats>().playerID;
                        gameManager.GetComponent<heathbarManager>().TakeDamage(attackDamage, otherID);
                        damageDealt = true;
                        //Debug.Log($"Damaged {other.name} by {attackDamage}!");
                    }
                    other.attachedRigidbody.AddForce(direction * attackKnockback);
                }
            }
        }

        if (friendlyFire)
        {
            player.GetComponent<Rigidbody2D>().AddForce(-direction * attackKnockback * friendlyFireMultiplier);
        }
    }

    public void OpponentCheck()
    {
        GameObject gameManager = GameObject.Find("gameManager");
        if (gameManager.GetComponent<heathbarManager>().player_1 == null || gameManager.GetComponent<heathbarManager>().player_0 == null)
        {
            Debug.LogWarning("Player 2 has not joined yet so the raycast was unsuccessful.");
            player2NotPresent = true;
        }
        else if (playerID == 0)
        {
            opponent = gameManager.GetComponent<heathbarManager>().player_1;
        }
        else
        {
            opponent = gameManager.GetComponent<heathbarManager>().player_0;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(knockbackCenter.transform.position, opponent.transform.position);
    }
}
