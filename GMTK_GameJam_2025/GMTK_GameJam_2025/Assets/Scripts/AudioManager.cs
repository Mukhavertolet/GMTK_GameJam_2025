using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            s.source.loop = s.loop;
        }
    }

    public void Start()
    {
        Play("Theme");
    }

    public void Play(string name, float pitch)
    {
        Sound s = Array.Find(sounds, Sound => Sound.sname == name);
        if (s != null)
        {
            s.pitch = pitch;
            s.source.Play();
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.sname == name);
        if (s != null)
        {
            s.source.Play();
        }
    }
}
