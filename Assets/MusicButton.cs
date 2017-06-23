using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour {
    
    public MyButton musicButton;

    bool on = true;

    void Start() {
        musicButton.signalOnClick.AddListener(change);
        on = SoundManager.instance.isMusicOn();
        gameObject.GetComponent<UI2DSprite>().enabled = on;
    }

    public void change() {
        on = !on;
        gameObject.GetComponent<UI2DSprite>().enabled = on;
        SoundManager.instance.setMusicOn(on);
        if(on) BackgroundMusic.backgroundMusic.turnOn();
        else BackgroundMusic.backgroundMusic.turnOff();
    }

}
