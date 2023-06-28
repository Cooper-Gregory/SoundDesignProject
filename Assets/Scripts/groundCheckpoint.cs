using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheckpoint : MonoBehaviour
{
    public GameObject player;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "WorldGeometry" || other.tag == "Player")
        {
            //This would be a good place to call sfx for landing on the ground, probably in a switch case like from movementController.
            //Note that this *could* fail to activate in some edge cases which is why the ground check is in OnTriggerStay
            //putting audio inside OnTriggerStay would also work, but you'd need a boolean (toggled off in OnTriggerExit) or it'll play the audio every frame
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "WorldGeometry" || other.tag == "Player")
        {
            player.GetComponent<movementController>().onGround = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "WorldGeometry" || other.tag == "Player")
        {
            player.GetComponent<movementController>().onGround = false;
        }
    }
}
