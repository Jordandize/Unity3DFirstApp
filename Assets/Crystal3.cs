using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal3 : MonoBehaviour {

    public static Crystal3 crystal;

    void Start() {
        crystal = this;
        crystal.gameObject.GetComponent<UI2DSprite>().enabled = false;
    }

    public void findCrystal() {
        crystal.gameObject.GetComponent<UI2DSprite>().enabled = true;
    }

}
