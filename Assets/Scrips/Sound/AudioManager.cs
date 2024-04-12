using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            
    }

    private void Start()
    {
        PlayMusic("Theme");
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, s => s.name == name);
        if (s == null) 
        {
            Debug.Log("sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, s => s.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }

    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        Debug.Log(sfxSource.mute + " = sfx sourcer mute");
        sfxSource.mute = !sfxSource.mute;
    }

}
