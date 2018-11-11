﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ray_Hit : MonoBehaviour {


    float holdTime = 1.0f;
    float timer = 0;
    public Image LoadingBar;
    float currentValue;
    public float speed;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit)){
            
			timer += Time.deltaTime;

			if(hit.transform.name.Equals("Enemy")){
				HitCircleAnim(hit.transform.gameObject);
			}
		}else{
            ResetCircleAnim();
        };

	}

	void HitCircleAnim (GameObject thenDestroy) {
        if (currentValue <= 100)
        {
            currentValue += speed * Time.deltaTime;
            LoadingBar.fillAmount = currentValue / 100;
		} else {
            ResetCircleAnim();
			Destroy(thenDestroy);
		}
    }

	void ResetCircleAnim(){
        timer = 0;
        LoadingBar.fillAmount = 0f;
        currentValue = 0;
	}

    /* void SpawnNewHit(){

        Instantiate(thenDestroy);

        if (instantiatedObject.transform.position.x > bounds1 && (instantiatedObject.transform.position.z < bounds2)
 { GetRandomCoords(); InstantiateObject(); }
	}  */
}
