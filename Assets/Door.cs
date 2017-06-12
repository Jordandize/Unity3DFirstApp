using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour {

    public string level = "MainMenu";

    void OnTriggerEnter2D(Collider2D collider) {
        HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if(rabit != null)
            SceneManager.LoadScene(level);
    }

}
