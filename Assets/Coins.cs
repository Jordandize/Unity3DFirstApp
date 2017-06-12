using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable {

    protected  override void OnRabitHit(HeroRabit rabit) {
        CoinsLabel.coinsLabel.changeLabel(rabit.addCoin());
        this.CollectedHide();
    }

}
