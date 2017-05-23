using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController current;

    Vector3    startingPosition;

    void Awake() {
        current = this;
    }

    public void setStartPosition(Vector3 position) {
        this.startingPosition = position;
    }

    public void onRabitDeath(HeroRabit rabit) {
        rabit.transform.position = this.startingPosition;
    }

}
