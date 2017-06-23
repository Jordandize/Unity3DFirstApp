using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour {

    public MyButton settingButton;

    public GameObject settingPrefab;
    
	void Start () {
        settingButton.signalOnClick.AddListener(showSettingPopUp);
    }

    void showSettingPopUp() {
        GameObject parent = UICamera.first.transform.parent.gameObject;
        NGUITools.AddChild(parent, settingPrefab);
    }

}
