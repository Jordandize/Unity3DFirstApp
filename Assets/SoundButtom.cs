using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtom : MonoBehaviour {

    public MyButton soundButton;

    bool on;

    void Start () {
        soundButton.signalOnClick.AddListener(change);
        on = SoundManager.instance.isSoundOn();
        gameObject.GetComponent<UI2DSprite>().enabled = on;
    }

    public void change() {
        on = !on;
        gameObject.GetComponent<UI2DSprite>().enabled = on;
        SoundManager.instance.setSoundOn(on);
    }

}
