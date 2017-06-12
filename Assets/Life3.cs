using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life3 : MonoBehaviour {

    public static Life3 life3 = null;

    void Start() {
        life3 = this;
    }

    public void loseLife3() {
        GameObject.Destroy(this.gameObject);
    }

}
