using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setAttackData : MonoBehaviour
{
    //if you want me to be honest this script is hell on earth
    //Ask me to come over for help if you want custom sound effects for selecting different abilities from the menu screen
    //but that would probably be a while of work to handle

    public int sad_attackSlotsAmount = 3; 
    public int sad_attackParamAmount = 8;
    public float[,] p0_attackChoices;
    public float[,] p1_attackChoices;
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] attack_sword = new float[8];
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] attack_explosion = new float[8];
    [Tooltip("0 - Scale.\n1 - Projectile Speed.\n2 - Damage.\n3 - Knockback.\n4 - Duration (s).\n5 - Cooldown (s).\n6 - Projectile/Melee (1/0).\n7 - Friendly Fire? (1/0).")]
    public float[] attack_gun = new float[8];

    public void UpdateAttackData(int playerID, int attackSlot, int attackType)
    {
        if (p0_attackChoices == null)
        {
            p0_attackChoices = new float[sad_attackSlotsAmount, sad_attackParamAmount];
        }
        if (p1_attackChoices == null)
        {
            p1_attackChoices = new float[sad_attackSlotsAmount, sad_attackParamAmount];
        }

        if (playerID == 0)
        {
            if (attackType == 0)
            {
                for (int i = 0; i < sad_attackParamAmount; i++)
                {
                    p0_attackChoices[attackSlot, i] = attack_sword[i];
                    //Debug.Log($"Set slot {attackSlot} for player 1 to Sword ({attack_sword[i]})");
                }
                GameObject.Find("playerSettings").GetComponent<playerSettings>().p0_attacks_simple[attackSlot] = 0;
            }
            if (attackType == 1)
            {
                for (int i = 0; i < sad_attackParamAmount; i++)
                {
                    p0_attackChoices[attackSlot, i] = attack_explosion[i];
                    //Debug.Log($"Set slot {attackSlot} for player 1 to Explosion ({attack_explosion[i]})");
                }
                GameObject.Find("playerSettings").GetComponent<playerSettings>().p0_attacks_simple[attackSlot] = 1;
            }
            if (attackType == 2)
            {
                for (int i = 0; i < sad_attackParamAmount; i++)
                {
                    p0_attackChoices[attackSlot, i] = attack_gun[i];
                    //Debug.Log($"Set slot {attackSlot} for player 1 to Gun ({attack_gun[i]})");
                }
                GameObject.Find("playerSettings").GetComponent<playerSettings>().p0_attacks_simple[attackSlot] = 2;
            }
            GameObject.Find("playerSettings").GetComponent<playerSettings>().p0_attackSlots = p0_attackChoices;
            for (int i = 0; i < sad_attackSlotsAmount; i++)
            {
                for (int j = 0; j < sad_attackParamAmount; j++)
                {
                    Debug.Log($"Slot: {i}, Param: {j}, Value: {GameObject.Find("playerSettings").GetComponent<playerSettings>().p0_attackSlots[i,j]}");
                }
            }
        }
        else
        {
            if (attackType == 0)
            {
                for (int i = 0; i < sad_attackParamAmount; i++)
                {
                    p1_attackChoices[attackSlot, i] = attack_sword[i];
                    //Debug.Log($"Set slot {attackSlot} for player 2 to Sword ({attack_sword[i]})");
                }
                GameObject.Find("playerSettings").GetComponent<playerSettings>().p1_attacks_simple[attackSlot] = 0;
            }
            if (attackType == 1)
            {
                for (int i = 0; i < sad_attackParamAmount; i++)
                {
                    p1_attackChoices[attackSlot, i] = attack_explosion[i];
                    //Debug.Log($"Set slot {attackSlot} for player 2 to Explosion ({attack_explosion[i]})");
                }
                GameObject.Find("playerSettings").GetComponent<playerSettings>().p1_attacks_simple[attackSlot] = 1;
            }
            if (attackType == 2)
            {
                for (int i = 0; i < sad_attackParamAmount; i++)
                {
                    p1_attackChoices[attackSlot, i] = attack_gun[i];
                    //Debug.Log($"Set slot {attackSlot} for player 2 to Gun ({attack_gun[i]})");
                }
                GameObject.Find("playerSettings").GetComponent<playerSettings>().p1_attacks_simple[attackSlot] = 2;
            }
            GameObject.Find("playerSettings").GetComponent<playerSettings>().p1_attackSlots = p1_attackChoices;
            for (int i = 0; i < sad_attackSlotsAmount; i++)
            {
                for (int j = 0; j < sad_attackParamAmount; j++)
                {
                    Debug.Log($"Slot: {i}, Param: {j}, Value: {GameObject.Find("playerSettings").GetComponent<playerSettings>().p0_attackSlots[i, j]}");
                }
            }
        }
        
        

    }
}
