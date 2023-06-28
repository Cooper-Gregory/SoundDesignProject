using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockbackField : MonoBehaviour
{
    public float knockbackPerFrame = 0.00005f;
    public bool fieldEnabled = true;
    public void OnTriggerStay2D(Collider2D other)
    {
        var heading = other.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        if (fieldEnabled)
        {
            other.attachedRigidbody.AddForce(direction * knockbackPerFrame);
        }
    }

    public void ToggleEnabled()
    {
        if (fieldEnabled)
        {
            fieldEnabled = false;
        }
        else
        {
            fieldEnabled = true;
        }
    }
}
