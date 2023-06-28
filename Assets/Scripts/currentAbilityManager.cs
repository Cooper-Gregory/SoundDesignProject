using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentAbilityManager : MonoBehaviour
{
    public Image currentAbility;
    public Sprite swordIcon;
    public Sprite explosionIcon;
    public Sprite gunIcon;
    public Sprite gunIconRight;
    public Image swordHand;
    public Image explosionHand;
    public Image gunHand;
    public GameObject gunHandObj;
    public GameObject pivot;
    public float testRot1 = 225.0f;
    public float testRot2 = 45.0f;

    public void UpdateActiveAbility(int activeAbility)
    {
        if (activeAbility == 0)
        {
            currentAbility.sprite = swordIcon;
            currentAbility.color = new Color(1, 1, 1, 1);
            swordHand.enabled = true;
            explosionHand.enabled = false;
            gunHand.enabled = false;
        }
        else if (activeAbility == 1)
        {
            currentAbility.sprite = explosionIcon;
            currentAbility.color = new Color(1, 1, 1, 1);
            swordHand.enabled = false;
            explosionHand.enabled = true;
            gunHand.enabled = false;
        }
        else if (activeAbility == 2)
        {
            currentAbility.sprite = gunIcon;
            currentAbility.color = new Color(1, 1, 1, 1);
            swordHand.enabled = false;
            explosionHand.enabled = false;
            gunHand.enabled = true;
        }
        else if (activeAbility == 3)
        {
            currentAbility.sprite = null;
            currentAbility.color = new Color(0, 0, 0, 0);
            swordHand.enabled = false;
            explosionHand.enabled = false;
            gunHand.enabled = false;
        }
        else
        {
            Debug.LogWarning($"Dude, you messed up. {activeAbility}");
        }
    }

    public void Update()
    {
        if (pivot.transform.rotation.z > 0)
        {
            gunHand.sprite = gunIconRight;
        }
        else
        {
            gunHand.sprite = gunIcon;
        }
    }
}
