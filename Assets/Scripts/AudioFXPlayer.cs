using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXPlayer : MonoBehaviour
{
    public static AudioFXPlayer Instance;
    private AudioSource audio;
    private void Awake()
    {
        Instance = this;
        audio = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip, float volume)
    {
        audio.PlayOneShot(clip, volume);
    }
}
