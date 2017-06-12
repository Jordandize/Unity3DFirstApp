using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life2 : MonoBehaviour {

    public static Life2 life2 = null;

    void Start() {
        life2 = this;
    }

    public void loseLife2() {
        GameObject.Destroy(this.gameObject);
    }
}
