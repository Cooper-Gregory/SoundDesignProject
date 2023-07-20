using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSounds : MonoBehaviour
{
    public AudioSource buttonhover;
    public AudioSource buttonpressed;
    // Start is called before the first frame update
    public void onbuttonhover()
    {
        buttonhover.Play();
    }

    // Update is called once per frame
    public void onbuttonpressed()
    {
        buttonpressed.Play();
    }
}
