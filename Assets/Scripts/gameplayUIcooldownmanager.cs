using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplayUIcooldownmanager : MonoBehaviour
{
    protected float p0_cooldownTimer;
    protected float p1_cooldownTimer;
    protected float p0_attackTimer;
    protected float p1_attackTimer;

    [Header("Timer Duration Logic")]
    public float p0_attackCooldown;
    public float p1_attackCooldown;
    public float p0_attackDuration;
    public float p1_attackDuration;

    [Header("Display Logic")]
    public bool p0_attacking = false;
    public bool p1_attacking = false;
    public bool p0_onCooldown = false;
    public bool p1_onCooldown = false;

    public int p0_attackNumber = 0;
    public int p1_attackNumber = 0;

    [Header("Player 2 Slot Overlays")]
    public GameObject p1_slot1Overlay;
    public GameObject p1_slot2Overlay;
    public GameObject p1_slot3Overlay;

    [Header("Player 1 Slot Overlays")]
    public GameObject p0_slot1Overlay;
    public GameObject p0_slot2Overlay;
    public GameObject p0_slot3Overlay;

    public void DisplayCooldown(int playerID, float cooldownTime, float attackTime, int attackNumber)
    {
        Debug.LogWarning("Running DisplayCooldown().");
        if (playerID == 0)
        {
            p0_attacking = true;
            p0_attackDuration = attackTime;
            p0_attackCooldown = cooldownTime;
            p0_attackNumber = attackNumber;
        }
        else
        {
            p1_attacking = true;
            p1_attackDuration = attackTime;
            p1_attackCooldown = cooldownTime;
            p1_attackNumber = attackNumber;
        }
    }

    public void Update()
    {
        RectTransform rt01 = p0_slot1Overlay.GetComponent<RectTransform>();
        RectTransform rt02 = p0_slot2Overlay.GetComponent<RectTransform>();
        RectTransform rt03 = p0_slot3Overlay.GetComponent<RectTransform>();

        RectTransform rt11 = p1_slot1Overlay.GetComponent<RectTransform>();
        RectTransform rt12 = p1_slot2Overlay.GetComponent<RectTransform>();
        RectTransform rt13 = p1_slot3Overlay.GetComponent<RectTransform>();

        if (p0_attacking)
        {
            p0_attackTimer += Time.deltaTime;
            if (p0_attackNumber == 0)
            {
                rt03.sizeDelta = new Vector2(100, 100 - (p0_attackTimer/p0_attackDuration * 100));
            }
            else if(p0_attackNumber == 1)
            {
                rt01.sizeDelta = new Vector2(100, 100 - (p0_attackTimer / p0_attackDuration * 100));
            }
            else if (p0_attackNumber == 2)
            {
                rt02.sizeDelta = new Vector2(100, 100 - (p0_attackTimer / p0_attackDuration * 100));
            }
        }
        if (p0_attackTimer >= p0_attackDuration && p0_attacking)
        {
            p0_attackTimer = 0.0f;
            p0_attacking = false;
            p0_onCooldown = true;
            if (p0_attackNumber == 0)
            {
                rt03.sizeDelta = new Vector2(100, 0);
            }
            else if (p0_attackNumber == 1)
            {
                rt01.sizeDelta = new Vector2(100, 0);
            }
            else if (p0_attackNumber == 2)
            {
                rt02.sizeDelta = new Vector2(100, 0);
            }
        }
        if (p0_onCooldown)
        {
            p0_cooldownTimer += Time.deltaTime;
            if (p0_attackNumber == 0)
            {
                rt01.sizeDelta = new Vector2(100, p0_attackTimer / p0_attackDuration * 100);
            }
            else if (p0_attackNumber == 1)
            {
                rt02.sizeDelta = new Vector2(100, p0_attackTimer / p0_attackDuration * 100);
            }
            else if (p0_attackNumber == 2)
            {
                rt03.sizeDelta = new Vector2(100, p0_attackTimer / p0_attackDuration * 100);
            }
        }
        if (p0_cooldownTimer >= p0_attackCooldown && p0_onCooldown)
        {
            p0_cooldownTimer = 0.0f;
            p0_onCooldown = false;
            if (p0_attackNumber == 0)
            {
                rt01.sizeDelta = new Vector2(100, 100);
            }
            else if (p0_attackNumber == 1)
            {
                rt02.sizeDelta = new Vector2(100, 100);
            }
            else if (p0_attackNumber == 2)
            {
                rt03.sizeDelta = new Vector2(100, 100);
            }
        }

        if (p1_attacking)
        {
            p1_attackTimer += Time.deltaTime;
            if (p1_attackNumber == 0)
            {
                rt13.sizeDelta = new Vector2(100, 100 - (p1_attackTimer / p1_attackDuration * 100));
            }
            else if (p1_attackNumber == 1)
            {
                rt11.sizeDelta = new Vector2(100, 100 - (p1_attackTimer / p1_attackDuration * 100));
            }
            else if (p1_attackNumber == 2)
            {
                rt12.sizeDelta = new Vector2(100, 100 - (p1_attackTimer / p1_attackDuration * 100));
            }
        }
        if (p1_attackTimer >= p1_attackDuration && p1_attacking)
        {
            p1_attackTimer = 0.0f;
            p1_attacking = false;
            p1_onCooldown = true;
            if (p1_attackNumber == 0)
            {
                rt13.sizeDelta = new Vector2(100, 0);
            }
            else if (p1_attackNumber == 1)
            {
                rt11.sizeDelta = new Vector2(100, 0);
            }
            else if (p1_attackNumber == 2)
            {
                rt12.sizeDelta = new Vector2(100, 0);
            }
        }
        if (p1_onCooldown)
        {
            p1_cooldownTimer += Time.deltaTime;
            if (p1_attackNumber == 0)
            {
                rt11.sizeDelta = new Vector2(100, p1_attackTimer / p1_attackDuration * 100);
            }
            else if (p1_attackNumber == 1)
            {
                rt12.sizeDelta = new Vector2(100, p1_attackTimer / p1_attackDuration * 100);
            }
            else if (p1_attackNumber == 2)
            {
                rt13.sizeDelta = new Vector2(100, p1_attackTimer / p1_attackDuration * 100);
            }
        }
        if (p1_cooldownTimer >= p1_attackCooldown && p1_onCooldown)
        {
            p1_cooldownTimer = 0.0f;
            p1_onCooldown = false;
            if (p1_attackNumber == 0)
            {
                rt11.sizeDelta = new Vector2(100, 100);
            }
            else if (p1_attackNumber == 1)
            {
                rt12.sizeDelta = new Vector2(100, 100);
            }
            else if (p1_attackNumber == 2)
            {
                rt13.sizeDelta = new Vector2(100, 100);
            }
        }
    }
}
