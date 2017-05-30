using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour {

    public float slowdown = 0.5f;

    Vector3 lastPosition;
    float height;

    void Awake() {
        lastPosition = Camera.main.transform.position;
        height = this.transform.position.y;
    }

    void LateUpdate() {
        Vector3 new_position = Camera.main.transform.position;
        Vector3 diff = new_position - lastPosition;

        lastPosition = new_position;

        Vector3 my_pos = this.transform.position;
        my_pos += slowdown * diff;
        my_pos.y = height;

        this.transform.position = my_pos;
    }
}
