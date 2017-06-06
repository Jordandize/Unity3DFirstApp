using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

    public static HeroRabit lastRabit = null;

    Rigidbody2D myBody = null;
    Transform heroParent = null;

    public float speed     = 3f;
    public float JumpSpeed = 2f;

    bool isGrounded = false;
    bool JumpActive = false;
    bool isLikeHalk = false;
    bool isRabitDie = false;

    Vector3 originScale;

    float JumpTime = 0f;

    public float MaxJumpTime = 2f;

    public Vector3 btnLft() {
        BoxCollider2D boxcol = this.GetComponent<BoxCollider2D>();

        Vector3 world = transform.TransformPoint(boxcol.offset);
        float rbot = world.y - (boxcol.size.y / 2f);
        float rlef = world.x - (boxcol.size.x / 2f);

        return new Vector3(rlef, rbot, 0f);
    }

    void Awake() {
        lastRabit = this;
    }

    void Start () {
        myBody = GetComponent<Rigidbody2D>();
        this.heroParent = this.transform.parent;
        originScale = this.transform.localScale;
        LevelController.current.setStartPosition(transform.position);
    }
	
	void Update () {
        if(isRabitDie) return;

        Animator animator = GetComponent<Animator>();
        UpdateRun(animator);
        UpdateJump(animator);
    }

    void FixedUpdate() {

        if(isRabitDie) return;

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
        Vector3 to = transform.position + Vector3.down * 0.05f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");

        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (hit) {
            isGrounded = true;

            if(hit.transform != null
                && hit.transform.GetComponent<MovingPlatform>() != null) {
                setNewParent(this.transform, hit.transform);
            }
        } else {
            isGrounded = false;
            setNewParent(this.transform, this.heroParent);
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

    static void setNewParent(Transform obj, Transform newParent) {
        if(obj.transform.parent != newParent) {
            Vector3 pos = obj.transform.position;
            obj.transform.parent = newParent;
            obj.transform.position = pos;
        }
    }

    public void eatMushroom() {
        isLikeHalk = true;
        this.transform.localScale *= 1.7f;
    }

    public void stepOnBomb() {
        if(isLikeHalk) {
            isLikeHalk = false;
            this.transform.localScale = originScale;
        } else {
            deadRabit();
        }
    }

    public void deadRabit() {
        if(!isRabitDie)
            StartCoroutine(rebirthLater());
    }

    IEnumerator rebirthLater() {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("die", true);
        isRabitDie = true;
        

        yield return new WaitForSeconds(2f);

        isRabitDie = false;
        animator.SetBool("die", false);
        LevelController.current.onRabitDeath(this);
    }
}
