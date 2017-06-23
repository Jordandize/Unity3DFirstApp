using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    bool soundOn = true;
    bool musicOn = true;

    public bool isSoundOn() {
        return this.soundOn;
    }

    public bool isMusicOn() {
        return this.musicOn;
    }

    public void setSoundOn(bool val) {
        this.soundOn = val;
        PlayerPrefs.SetInt("sound", this.soundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void setMusicOn(bool val) {
        this.musicOn = val;
        PlayerPrefs.SetInt("music", this.musicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    SoundManager() {
        soundOn = PlayerPrefs.GetInt("sound", 1) == 1;
        musicOn = PlayerPrefs.GetInt("music", 1) == 1;
    }

    public static SoundManager instance = new SoundManager();

}
