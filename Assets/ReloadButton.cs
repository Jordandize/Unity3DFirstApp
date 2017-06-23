using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReloadButton : MonoBehaviour {

    public MyButton reloadButton;

	void Start () {
        reloadButton.signalOnClick.AddListener(reloadScene);
	}

    void reloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
