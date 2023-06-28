using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class shortRangeKick : MonoBehaviour
{
    public GameObject parent;
    public int playerID;
    public GameObject otherPlayer;
    public bool kicked = false;
    public float kickMultiplier = 0.01f;
    public bool kickOnCooldown = false;
    public float kickCooldown = 0.5f;
    protected float kickCooldownTimer = 0.0f;

    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        playerID = parent.GetComponent<playerStats>().playerID;
    }

    private void Update()
    {
        if (kickOnCooldown)
        {
            kickCooldownTimer += Time.deltaTime;
        }
        if (kickCooldownTimer >= kickCooldown)
        {
            kickOnCooldown = false;
            kickCooldownTimer = 0.0f;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            otherPlayer = other.gameObject;
        }
        else
        {
            
        }
    }

    public void OnTriggerExit2D()
    {
        otherPlayer = null;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            kicked = true;
            KickEnemy();
        }
        else
        {
            kicked = false;
        }
    }

    public void KickEnemy()
    {
        if (otherPlayer != null && !kickOnCooldown)
        {
            var heading = otherPlayer.transform.position - gameObject.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;

            otherPlayer.GetComponent<Rigidbody2D>().AddForce(direction * kickMultiplier / 10.0f, ForceMode2D.Impulse);
            kickOnCooldown = true;
        }
    }
}
