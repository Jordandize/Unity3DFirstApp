using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

    Rigidbody2D myBody = null;

    public float speed     = 3f;
    public float JumpSpeed = 2f;

    bool isGrounded = false;
    bool JumpActive = false;

    float JumpTime = 0f;

    public float MaxJumpTime = 2f;
    
	void Start () {
        myBody = GetComponent<Rigidbody2D>();
        LevelController.current.setStartPosition(transform.position);
    }
	
	void Update () {
        Animator animator = GetComponent<Animator>();

        UpdateRun(animator);
        UpdateJump(animator);
    }

    void FixedUpdate() {

        FixedUpdateRun(); //speed of rabit
        FixedUpdateGround(); //or rabit on ground
        FixedUpdateJump(); //jump of rabit

    }

    void UpdateRun(Animator animator) {
        float value = Input.GetAxis("Horizontal");

        if (Mathf.Abs(value) > 0) {
            animator.SetBool("run", true);
        } else {
            animator.SetBool("run", false);
        }
    }

    void UpdateJump(Animator animator) {
        if (this.isGrounded) {
            animator.SetBool("jump", false);
        } else {
            animator.SetBool("jump", true);
        }
    }

    void FixedUpdateRun() {
        float speedKoeff = Input.GetAxis("Horizontal");

        if (Mathf.Abs(speedKoeff) > 0) {
            Vector2 newVelocity = myBody.velocity;
            newVelocity.x = speedKoeff * speed;
            myBody.velocity = newVelocity;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (speedKoeff < 0) {
            sr.flipX = true;
        } else if (speedKoeff > 0) {
            sr.flipX = false;
        }
    }

    void FixedUpdateGround() {
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");

        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (hit) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
        //for debag
        Debug.DrawLine(from, to, Color.red);
    }

    void FixedUpdateJump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            this.JumpActive = true;
        }

        if (this.JumpActive) {
            if (Input.GetButton("Jump")) {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime) {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            } else {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
    }

}
