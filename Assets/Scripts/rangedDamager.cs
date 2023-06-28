using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedDamager : MonoBehaviour
{
    [Header("Attack Defaults")]
    public float attackRange = 3.0f;
    public float attackDamage = 10.0f;
    public float attackKnockback = 0.01f;
    public float friendlyFireMultiplier = 0.33f;
    public float attackDuration = 5.0f;

    [Header("Attack Settings")]
    public int playerID = 0;
    public GameObject player;
    public GameObject knockbackCenter;
    public LayerMask playerMask;
    public GameObject opponent;

    [Header("Attack Logic")]
    public Vector3 heading;
    public int collisionCount;
    public bool damageDealt = false;
    public bool player2NotPresent = false;
    public bool friendlyFire = false;
    protected float attackDurationTimer;

    public void Update()
    {
        OpponentCheck();
        attackDurationTimer += Time.deltaTime;
        if (attackDurationTimer >= attackDuration)
        {
            attackDurationTimer = 0.0f;
            Destroy(gameObject);
        }
        if (collisionCount >= 1)
        {
            Destroy(gameObject);
        }
        RaycastHit2D wallCheck = Physics2D.Raycast(knockbackCenter.transform.position, (opponent.transform.position - knockbackCenter.transform.position), playerMask);
        if (wallCheck.collider != null && wallCheck.collider == opponent)
        {
            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(opponent.transform.position.x - transform.position.x, opponent.transform.position.y - transform.position.y) * 5.0f;
        }
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        GameObject gameManager = GameObject.Find("gameManager");
        

        var heading = other.transform.position - knockbackCenter.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        collisionCount++;

        if (player2NotPresent)
        {
            player2NotPresent = false;
        }
        else
        {

            if (other.gameObject.tag == "Player")
            {
                if (!(other.gameObject.GetComponent<playerStats>().playerID == playerID))
                {
                    if (damageDealt == false)
                    {
                        int otherID = other.gameObject.GetComponent<playerStats>().playerID;
                        gameManager.GetComponent<heathbarManager>().TakeDamage(attackDamage, otherID);
                        damageDealt = true;
                        //Debug.Log($"Damaged {other.gameObject.name} by {attackDamage}!");
                        //I wouldn't put audio here, probably throw it in TakeDamage() under heathbarManager.
                    }
                    opponent.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<velocityTracker>().lastStoredVelocity.normalized * attackKnockback);
                }
            }
            else if (other.gameObject.tag == "PlayerAttack")
            {
                    if (damageDealt == false)
                    {
                        int otherID = opponent.gameObject.GetComponent<playerStats>().playerID;
                        gameManager.GetComponent<heathbarManager>().TakeDamage(attackDamage, otherID);
                        damageDealt = true;
                        //Debug.Log($"Damaged {other.gameObject.name} by {attackDamage}!");
                    }
                    opponent.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<velocityTracker>().lastStoredVelocity.normalized * attackKnockback);
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
            Debug.LogWarning("Player 2 has not joined yet so damage cannot be dealt.");
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
