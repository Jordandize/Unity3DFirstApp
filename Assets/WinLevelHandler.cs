using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevelHandler : MonoBehaviour {

    public AudioClip winClip = null;

    public UI2DSprite[] crystals;

    public UILabel fruitsLabel;
    public UILabel coinsLabel;

    AudioSource winSource;

	void Start () {
        winSource = gameObject.AddComponent<AudioSource>();
        winSource.clip = winClip;
        winSource.Play();
        setCrystals();
        setFruitsLabel();
        setCoinsLabel();
        saveStats();
	}

    void setCrystals() {
        for(int i = 0; i < crystals.Length; i++) {
            crystals[i].GetComponent<UI2DSprite>().enabled = HeroRabit.lastRabit.hasCrystal(i);
        }
    }

    void setFruitsLabel() {
        fruitsLabel.text = HeroRabit.lastRabit.getFruits().ToString() + '/' + LevelController.current.maxFruits.ToString();
    }

    void setCoinsLabel() {
        coinsLabel.text = "+" + HeroRabit.lastRabit.getCoins().ToString();
    }

    void saveStats() {
        HeroRabit.lastRabit.saveStats();
        HeroRabit.lastRabit.saveCoins();
    }

}
