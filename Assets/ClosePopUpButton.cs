using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePopUpButton : MonoBehaviour {

    public MyButton closeButton;

	void Start () {
		closeButton.signalOnClick.AddListener(close);
    }

    void close() {
        SettingsPopUp.instance.close();
    }

}
