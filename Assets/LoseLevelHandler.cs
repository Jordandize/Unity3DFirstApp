using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseLevelHandler : MonoBehaviour {

    public AudioClip loseClip = null;

    public UI2DSprite[] crystals;

    AudioSource loseSource;

    void Start () {
        loseSource = gameObject.AddComponent<AudioSource>();
        loseSource.clip = loseClip;
        loseSource.Play();
        setCrystals();
        saveStats();
    }

    void setCrystals() {
        for(int i = 0; i < crystals.Length; i++) {
            crystals[i].GetComponent<UI2DSprite>().enabled = HeroRabit.lastRabit.hasCrystal(i);
        }
    }

    void saveStats() {
        HeroRabit.lastRabit.saveCoins();
    }

}
