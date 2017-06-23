using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopUp : MonoBehaviour {

    public static SettingsPopUp instance;

    void Start() {
        instance = this;
    }

    public void close() {
        Destroy(instance.gameObject);
    }

}
