using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal1 : MonoBehaviour {

    public static Crystal1 crystal;

	void Start () {
        crystal = this;
        crystal.gameObject.GetComponent<UI2DSprite>().enabled = false;
	}

    public void findCrystal() {
        crystal.gameObject.GetComponent<UI2DSprite>().enabled = true;
    }

}
