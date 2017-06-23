using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour {

    public HeroRabit rabit = null;
   
	void Update () {
        Debug.Log(rabit);

        Transform rabitTransform = rabit.transform;
        Transform cameraTransform = this.transform;
       
        Vector3 rabitPosition = rabitTransform.position;
        Vector3 cameraPosition = cameraTransform.position;
        
        cameraPosition.x = rabitPosition.x;
        cameraPosition.y = rabitPosition.y;
        
        cameraTransform.position = cameraPosition;
    }
}
