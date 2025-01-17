using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager Instance;
    

    private void Awake() {
        Instance = this;
        foreach (Sound sound in sounds){
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
        PlayBackground("BackgroundDrone1");
    }

    public void Play(string name){
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        
        //walking pitch variation
        if(name.Equals("WalkingStone")){
            float range = sound.source.pitch;
            sound.source.pitch = Mathf.Clamp(
                UnityEngine.Random.Range(range-.1f,range+.11f),
                .75f,
                1f
            );
        }
        sound.source.Play();
    }

    public void PlayBackground(string name){
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Play();
    }

    public void Stop(string name){
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Stop();
    }
}
