using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public int playerID = 1;
    public float playerHealth = 100.0f;


    public int attackSlotsAmount = 3;
    public int attackParamAmount = 8;
    public float[,] attackSlots;
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] attack_0 = new float[8];
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] attack_1 = new float[8];
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] attack_2 = new float[8];
    public GameObject playerSettings;

    void Start()
    {
        playerSettings = GameObject.Find("playerSettings");
        if (playerID == 0)
        {
            attackSlots = playerSettings.GetComponent<playerSettings>().p0_attackSlots;
        }
        else if (playerID == 1)
        {
            attackSlots = playerSettings.GetComponent<playerSettings>().p1_attackSlots;
        }
        else
        {
            Debug.LogWarning("Unknown player attempted to parse attackSlots or attackSlots parse failed.");
        }
    }
}
