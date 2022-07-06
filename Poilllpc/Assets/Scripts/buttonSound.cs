using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonSound : MonoBehaviour
{
    public AudioSource audioPip;
    public AudioClip pip;
    //public Button button;

    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SoundOnClick);
    }


    public void SoundOnClick()
    {
        audioPip.PlayOneShot(pip);
    }

}
