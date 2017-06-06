using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3 moveBy = new Vector3(2, 0);
    public Vector3 speed  = new Vector3(2, 0);
    public float pause = 1f;

    Vector3 pointA;
    Vector3 pointB;

    bool isMovingToA = false;
    bool isPaused    = false;
    float timeToWait = 0;

	void Start () {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + moveBy;
    }
	
	void Update () {
        Vector3 myPosition = this.transform.position;

        Vector3 target;
        if(isMovingToA) {
            target = this.pointA;
        } else {
            target = this.pointB;
        }

        if(isArrived(myPosition, target)) {
            isMovingToA = (isMovingToA) ? false : true;
            speed *= -1;
            isPaused = true;
            timeToWait = pause;
        }

        Vector3 destination = target - myPosition;
        destination.z = 0;

        if(!isPaused) {
            this.transform.position += speed * Time.deltaTime;
        } else {
            timeToWait -= Time.deltaTime;
            if(timeToWait <= 0) {
                isPaused = false;
            }
        }
    }

    bool isArrived(Vector3 pos, Vector3 target) {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }

}
