﻿using UnityEngine;

public class GuardSoundHandler : MonoBehaviour
{


    private AudioSource _audioSource;

	void Awake ()
	{
	    _audioSource = GetComponent<AudioSource>();
	}
	
	
    public void PlaySound(string soundName, float volume)
    {
        _audioSource.clip = Resources.Load<AudioClip>(soundName);
        _audioSource.volume = volume;
        _audioSource.Play();
    }

    
}
