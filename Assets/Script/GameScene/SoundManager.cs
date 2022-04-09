using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

   

    private AudioSource soundEffectSource;

    public AudioClip[] soundEffects;

    private void Awake()
    {

        soundEffectSource = GetComponent<AudioSource>();
    }

  
    
    public void PlaySFXOnce(int index, float SFXvolume)
    {
        soundEffectSource.volume = SFXvolume;
        soundEffectSource.PlayOneShot(soundEffects[index]);
    }
}
