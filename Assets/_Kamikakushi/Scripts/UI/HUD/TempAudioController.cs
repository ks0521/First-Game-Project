using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAudioController : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public void SetBGMVolume(float value)
    {
        bgmSource.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
    }
}
