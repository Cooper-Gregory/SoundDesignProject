using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocityTracker : MonoBehaviour
{
    [Header("Velocity Tracking Settings")]
    public Rigidbody2D rigid;
    public float tickSpeed = 0.25f;
    public Vector2 lastStoredVelocity = new Vector2(0.0f, 0.0f);

    protected float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tickSpeed)
        {
            timer = 0.0f;
            lastStoredVelocity = rigid.velocity;
        }
    }
}
