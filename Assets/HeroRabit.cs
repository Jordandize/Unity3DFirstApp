using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

    public static HeroRabit lastRabit = null;

    public AudioClip walkSound = null;
    public AudioClip diedSound = null;
    public AudioClip landSound = null;

    public GameObject losePrefab;

    public float speed     = 3f;
    public float JumpSpeed = 2f;

    Rigidbody2D myBody   = null;
    Transform heroParent = null;

    AudioSource walkSource = null;
    AudioSource diedSource = null;
    AudioSource landSource = null;

    bool isGrounded = false;
    bool JumpActive = false;
    bool isLikeHalk = false;
    bool isRabitDie = false;

    Vector3 originScale;

    float JumpTime = 0f;

    public float MaxJumpTime = 2f;

    int coins  = 0;
    int fruits = 0;
    int lifes  = 3;

    bool[] crystals = new bool[3];

    LevelStat stats = new LevelStat();

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
        walkSource = gameObject.AddComponent<AudioSource>();
        diedSource = gameObject.AddComponent<AudioSource>();
        landSource = gameObject.AddComponent<AudioSource>();
        walkSource.clip = walkSound;
        diedSource.clip = diedSound;
        landSource.clip = landSound;
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
        removeLife();
        Animator animator = GetComponent<Animator>();
        animator.SetBool("die", true);
        isRabitDie = true;
        
        yield return new WaitForSeconds(2f);

        if(checkLife()) {
            isRabitDie = false;
            isLikeHalk = false;
            animator.SetBool("die", false);
            this.transform.localScale = originScale;
            LevelController.current.onRabitDeath(this);
        } else {
            HeroRabit.lastRabit.isRabitDie = true;
        }
    }

    public int addCoin() {
        return ++coins;
    }

    public int addFruit() {
        return ++fruits;
    }

    public int addCrystal(int type) {
        if(type < 1 || type > 3) return type;
        crystals[type - 1] = true;
        return type;
    }

    public void removeLife() {
        if(lifes == 3) {
            Life3.life3.loseLife3();
        } else if(lifes == 2) {
            Life2.life2.loseLife2();
        } else if(lifes == 1) {
            Life1.life1.loseLife1();
        }
        lifes--;
    }

    public bool isDie() {
        return isRabitDie;
    }

    public bool checkLife() {
        if(lifes < 1) {
            GameObject parent = UICamera.first.transform.parent.gameObject;
            NGUITools.AddChild(parent, losePrefab);

            HeroRabit.lastRabit.enabled = false;
            Destroy(HeroRabit.lastRabit.GetComponent<Rigidbody2D>());
            return false;
        }
        return true;
    }

    public int getFruits() {
        return fruits;
    }

    public int getCoins() {
        return coins;
    }

    public bool hasCrystal(int i) {
        return crystals[i];
    }

    public void saveStats() {
        string level = SceneManager.GetActiveScene().name;

        string lastLevelStr = PlayerPrefs.GetString(level);
        LevelStat lastStats = JsonUtility.FromJson<LevelStat>(lastLevelStr);
        if(lastStats == null) {
            lastStats = new LevelStat();
        }

        if(LevelController.current.maxFruits == fruits)
            stats.hasAllFruits = (true || lastStats.hasAllFruits);

        bool hasCrystals = true;
        for(int i = 0; i < crystals.Length && hasCrystals; i++) {
            if(!crystals[i]) hasCrystals = false;
        }
        stats.hasCrystals = (hasCrystals || lastStats.hasCrystals);

        if(!isRabitDie)
            stats.levelPassed = (true || lastStats.levelPassed);

        string str = JsonUtility.ToJson(stats);
        PlayerPrefs.SetString(level, str);
    }

    public void saveCoins() {
        int globalCoins = PlayerPrefs.GetInt("coins");
        globalCoins += coins;

        PlayerPrefs.SetInt("coins", globalCoins);
    }

}
