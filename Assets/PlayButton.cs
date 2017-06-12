using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayButton : MonoBehaviour {

    public MyButton playButton;

	void Start () {
        playButton.signalOnClick.AddListener(play);
	}

    void play() {
        SceneManager.LoadScene("LevelMenu");
    }

}
