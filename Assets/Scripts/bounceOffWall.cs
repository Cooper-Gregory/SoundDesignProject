using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceOffWall : MonoBehaviour
{
    public float reboundVelocity = 7.0f; //The Speed which the player has to be travelling in order to bounce off a wall.
    public float damageVelocity = 14.0f; //The Speed at which the player has to be travelling in order to take damage from hitting the wall.
    public float returnVelocity = 0.6f; //The speed multiplier that is applied to the bounce velocity when the player has hit a wall at the right speed
    public float wallDamageMultiplier = 0.5f;

    public GameObject gameManager;
    public GameObject player;

    public AudioSource wallrebound;

    //When collision is detected, do some fun math
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.collider.gameObject;
        player = other;

        //Finds the rotation of the face which the player contacts and turns it into a quaternion for later
        ContactPoint2D contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector2.up, contact.normal);

        //Makes sure the other collider is actually a player
        if (other.tag == "Player")
        {
            //Finds the current velocity of the player (or rather, the velocity from within the last 250ms) and splits it into X and Y velocities for math
            Vector2 playerInitialVelocity = other.GetComponent<velocityTracker>().lastStoredVelocity;
            float playerXVelocity = playerInitialVelocity.x;
            float playerYVelocity = playerInitialVelocity.y;

            //Removes the signs from the player's velocity and finds the diagonal velocity using pythagoras
            int absoluteXVelocity = Mathf.Abs((int)playerXVelocity);
            int absoluteYVelocity = Mathf.Abs((int)playerYVelocity);
            float playerTotalVelocity = Mathf.Sqrt(absoluteXVelocity * absoluteXVelocity + absoluteYVelocity * absoluteYVelocity);

            //Debug.Log(playerTotalVelocity + " split vel: " + playerInitialVelocity);
            //Debug.Log("xrot: " + rot.x * 180 + "zrot: " + rot.z);

            //Checks the player's velocity against the rebound and damage velocities, then executes the functions
            if (playerTotalVelocity > reboundVelocity)
            {
                Rebound(playerTotalVelocity, playerXVelocity, playerYVelocity, rot, other);
            }
            if (playerTotalVelocity > damageVelocity)
            {
                Damage(playerTotalVelocity);
            }
        }
    }

    public void Rebound(float velTotal, float velX, float velY, Quaternion surfaceRotation, GameObject player)
    {
        //initiates the variables for the velocity to set and the rigidbody of the player
        Vector2 newVelocity = new Vector2(0.0f, 0.0f);
        Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();

        //Checks the rotation of the quaternion from earlier, then inverts the correct axis and applies the velocity reduction
        if (surfaceRotation.z == 0.0f && surfaceRotation.x == 0f)
        {
            //Debug.Log("Roof!");
            newVelocity = new Vector2(velX * returnVelocity, velY * returnVelocity * -1f);
            rigid.velocity = (newVelocity);
            wallrebound.Play();
        }
        else if (surfaceRotation.z == 0.0f && surfaceRotation.x == 1f) 
        {
            //Debug.Log("Floor!");
            newVelocity = new Vector2(velX * returnVelocity, velY * returnVelocity * -1f);
            rigid.velocity = (newVelocity);
            wallrebound.Play();
        }
        else if (surfaceRotation.z < 0.0f)
        {
            //Debug.Log("Right Wall!");
            newVelocity = new Vector2(velX * returnVelocity * -1f, velY * returnVelocity);
            rigid.velocity = (newVelocity);
            wallrebound.Play();
        }
        else if (surfaceRotation.z > 0.0f)
        {
            //Debug.Log("Left Wall!");
            newVelocity = new Vector2(velX * returnVelocity * -1f, velY * returnVelocity);
            rigid.velocity = (newVelocity);
            wallrebound.Play();
        }
        //Debug.Log("New Velocity: " + newVelocity);
        //Debug.Log("Player Velocity: " + rigid.velocity);
    }

    public void Damage(float velTotal)
    {
        float velocityDamage = velTotal * wallDamageMultiplier;
        gameManager.GetComponent<heathbarManager>().TakeDamage(velocityDamage, player.GetComponent<playerStats>().playerID);
        //You could put audio for damaging the player from bouncing off a wall here, but I'd recommend putting it under TakeDamage()
        //Optionally, you could also add another arg to TakeDamage() which determines the audio type it plays when you inflict damage on the player.
    }
}
