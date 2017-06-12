using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {

    protected override void OnRabitHit(HeroRabit rabit) {
        FruitsLabel.fruitsLabel.changeLabel(rabit.addFruit());
        this.CollectedHide();
    }

}
