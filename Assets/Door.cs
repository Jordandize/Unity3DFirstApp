using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour {

    public string level = "LevelMenu";

    public string openLevel = "isOpenAlsway";

    public GameObject lockObj;
    public GameObject crystObj;
    public GameObject fruitObj;

    LevelStat stats;

    bool isOpen = false;

    void Start() {
        setStats();
        setIsLocked();
        showAtributes();
    }

    void setStats() {
        string str = PlayerPrefs.GetString(level);
        stats = JsonUtility.FromJson<LevelStat>(str);
        if(stats == null) {
            stats = new LevelStat();
        }
    }

    void setIsLocked() {
        string str = PlayerPrefs.GetString(openLevel);
        LevelStat openLevelStats = JsonUtility.FromJson<LevelStat>(str);
        if(openLevelStats == null) {
            openLevelStats = new LevelStat();
        }
        isOpen = openLevelStats.levelPassed;

        if(openLevel == "isOpenAlsway")
            isOpen = true;
    }

    void showAtributes() {
        if(stats.levelPassed  ) Destroy(lockObj );
        if(!stats.hasCrystals ) Destroy(crystObj);
        if(!stats.hasAllFruits) Destroy(fruitObj);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if(rabit != null && isOpen)
            SceneManager.LoadScene(level);
    }

}
