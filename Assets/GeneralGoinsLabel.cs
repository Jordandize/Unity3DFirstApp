using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGoinsLabel : MonoBehaviour {

    public static GeneralGoinsLabel coinsLabel;

    UILabel label;

    void Start() {
        coinsLabel = this;
        label = coinsLabel.GetComponent<UILabel>();
        label.text = "0000";
        changeLabel(PlayerPrefs.GetInt("coins"));
    }

    public void changeLabel(int num) {
        string newLabel = "";
        int length = num.ToString().Length;

        if(length == 1) {
            newLabel = "000" + num;
        } else if(length == 2) {
            newLabel = "00" + num;
        } else if(length == 3) {
            newLabel = "0" + num;
        } else {
            newLabel = "" + num;
        }

        label.text = newLabel;
    }

}
