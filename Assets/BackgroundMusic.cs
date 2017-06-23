using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    public AudioClip music = null;
    AudioSource musicSource = null;

    void Start() {
        backgroundMusic = this;
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
        musicSource.loop = true;
        if(SoundManager.instance.isMusicOn()) musicSource.Play();
    }

    public void turnOff() {
        musicSource.Stop();
    }

    public void turnOn() {
        musicSource.Play();
    }

    public static BackgroundMusic backgroundMusic = null;

}
