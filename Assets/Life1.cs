using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life1 : MonoBehaviour {

    public static Life1 life1 = null;

    void Start() {
        life1 = this;
    }

    public void loseLife1() {
        GameObject.Destroy(this.gameObject);
    }

}
