﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class RayHit : MonoBehaviour
{
    public delegate void EnemyHit();
    public delegate void StartHit();
    public delegate void RestartHit();
    public event EnemyHit EnemyHitEvent;
    public event StartHit StartHitEvent;
    public event RestartHit RestartHitEvent;


    float currentValue;
    private float speed = 100;
    public Image loadingBar;

    void Start()
    {
        this.loadingBar = GameObject.Find("UISelectionBar").GetComponent<Image>();
        //sw.StartStopWatch();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Camera main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (Physics.Raycast(main.transform.position, main.transform.forward, out hit))
        {
            if (hit.transform.tag.Equals("Enemy") && hit.transform.GetComponent<AudioSource>().enabled == true)
            {
                if (EnemyHitEvent != null)
                {

                    if (currentValue <= 100)
                    {
                        currentValue += speed * Time.deltaTime;
                        loadingBar.fillAmount = currentValue / 100;
                    }
                    else
                    {
                        EnemyHitEvent();
                        ResetCircleAnim();
                    }
                }
            } else if (hit.transform.name.Equals("Start"))
            {
                if (StartHitEvent != null)
                {
                    if (currentValue <= 100)
                    {
                        currentValue += speed * Time.deltaTime;
                        loadingBar.fillAmount = currentValue / 100;
                    }
                    else
                    {
                        StartHitEvent();
                        ResetCircleAnim();
                    }
                }
            }else if (hit.transform.name.Equals("Restart"))
            {
                if (StartHitEvent != null)
                {
                    if (currentValue <= 100)
                    {
                        currentValue += speed * Time.deltaTime;
                        loadingBar.fillAmount = currentValue / 100;
                    }
                    else
                    {
                        RestartHitEvent();
                        ResetCircleAnim();
                    }
                }
            }
            else
            {
                ResetCircleAnim();
            }
        }else
        {
            ResetCircleAnim();
        }
    }

    public void HitCircleAnim(GameObject hit)
    {
        if (currentValue <= 100)
        {
            currentValue += speed * Time.deltaTime;
            loadingBar.fillAmount = currentValue / 100;
        }
        else
        {
            ResetCircleAnim();
            //sw.ResetAndStartStopWatch();
        }


    }

    public void ResetCircleAnim()
    {
        if (loadingBar)
        {

            loadingBar.fillAmount = 0f;
        }
        currentValue = 0;
    }

}
