using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitDoor : MonoBehaviour {

    public GameObject winPrefab;

    public string level = "LevelMenu";

    void OnTriggerEnter2D(Collider2D collider) {
        HeroRabit rabit = collider.GetComponent<HeroRabit>();

        if(rabit != null) {
            GameObject parent = UICamera.first.transform.parent.gameObject;
            NGUITools.AddChild(parent, winPrefab);
            HeroRabit.lastRabit.enabled = false;
            Destroy(HeroRabit.lastRabit.GetComponent<Rigidbody2D>());
        }
    }

}
