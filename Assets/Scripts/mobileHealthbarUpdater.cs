using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mobileHealthbarUpdater : MonoBehaviour
{
    public GameObject mobileHealthbar;

    public void UpdateHealthbar(float healthLevel)
    {
        mobileHealthbar.GetComponent<Slider>().value = healthLevel;
    }
}
