using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

    public float speed = 1;

    Rigidbody2D myBody = null;
    
	void Start () {
        myBody = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
		
	}

    void FixedUpdate() {
        float speedKoeff = Input.GetAxis("Horizontal");

        if(Mathf.Abs(speedKoeff) > 0) {
            Vector2 newVelocity = myBody.velocity;
            newVelocity.x = speedKoeff * speed;
            myBody.velocity = newVelocity;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(speedKoeff < 0) {
            sr.flipX = true;
        } else if(speedKoeff > 0) {
            sr.flipX = false;
        }
    }

}
