using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSettings : MonoBehaviour
{
    public string customPlayer0Name;
    public string customPlayer1Name;
    public Color customPlayer0Colour;
    public Color customPlayer1Colour;

    public int p0_attackSlotsAmount = 3;
    public int p0_attackParamAmount = 8;
    public float[,] p0_attackSlots;
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] p0_attack_0 = new float[8];
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] p0_attack_1 = new float[8];
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] p0_attack_2 = new float[8];
    public int[] p0_attacks_simple = new int[3];

    public int p1_attackSlotsAmount = 3;
    public int p1_attackParamAmount = 8;
    public float[,] p1_attackSlots;
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] p1_attack_0 = new float[8];
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] p1_attack_1 = new float[8];
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] p1_attack_2 = new float[8];
    public int[] p1_attacks_simple = new int[3];

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }



    public void UpdateAttackSlots()
    {
        p0_attackSlots = new float[p0_attackSlotsAmount, p0_attackParamAmount];
        p1_attackSlots = new float[p1_attackSlotsAmount, p1_attackParamAmount];
        for (int i = 0; i < p0_attackParamAmount; i++)
        {
            p0_attackSlots[0, i] = p0_attack_0[i];
            p0_attackSlots[1, i] = p0_attack_1[i];
            p0_attackSlots[2, i] = p0_attack_2[i];
        }
        for (int i = 0; i < p0_attackParamAmount; i++)
        {
            p1_attackSlots[0, i] = p1_attack_0[i];
            p1_attackSlots[1, i] = p1_attack_1[i];
            p1_attackSlots[2, i] = p1_attack_2[i];
        }
    }

}
