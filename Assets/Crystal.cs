using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {

    public int type = 0;

    protected override void OnRabitHit(HeroRabit rabit) {
        showCrystal(rabit.addCrystal(type));
        this.CollectedHide();
    }

    void showCrystal(int type) {
             if(type == 1) Crystal1.crystal.findCrystal();
        else if(type == 2) Crystal2.crystal.findCrystal();
        else if(type == 3) Crystal3.crystal.findCrystal();
    }

}
