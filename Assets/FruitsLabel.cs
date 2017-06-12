using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsLabel : MonoBehaviour {

    public static FruitsLabel fruitsLabel;

    UILabel label;
    public int max = 0;

	void Start () {
        fruitsLabel = this;
        label = fruitsLabel.GetComponent<UILabel>();
        label.text = "0/" + max;
	}

    public void changeLabel(int num) {
        label.text = num + "/" + max;
    }

}
