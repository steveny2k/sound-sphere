﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    public Transform enemy;


    System.Random r = new System.Random();
    List<GameObject> enemies = new List<GameObject>();
    int lastSoundSourceIndex = -1;

    public void InitializeEnemyList(GameObject enemy)
    {
        float scaling = 8;
        Vector3[] pts = PointsOnSphere(12);
        enemies = new List<GameObject>();
        int i = 0;

        foreach (Vector3 value in pts)
        {
            GameObject e = Instantiate(enemy, value * scaling, Quaternion.identity);
            enemies.Add(e);
            i++;
        }
    }

    public void showEnemies(bool show)
    {
        enemies.ForEach(x =>
        {
            x.SetActive(show);
        });
    }

    public void muteAllEnemies()
    {
        enemies.ForEach(x =>
        {
            x.transform.GetComponent<AudioSource>().enabled = false;
        });
    }

    public void initializeRandomAudioSource(TestCase.FoundObject foundObject = null)
    {
        muteAllEnemies();

        var enemyIndex = r.Next(enemies.Count - 1);
        while(enemyIndex == lastSoundSourceIndex)
        {
            enemyIndex = r.Next(enemies.Count - 1);
        }

        var enemy = enemies[r.Next(enemies.Count - 1)];

        while(foundObject != null && enemy.transform.position == foundObject.getPosition())
        {
            enemy = enemies[r.Next(enemies.Count - 1)];
        }

        lastSoundSourceIndex = enemyIndex;
        enemy.transform.GetComponent<AudioSource>().enabled = true;
    }

    public void setEnemyDisplay(EnemyDisplay display)
    {
        bool thisIsDumb = true;
        enemies.ForEach(enemy =>
        {
            enemy.GetComponent<Enemy>().setDisplay(display, thisIsDumb);
            thisIsDumb = !thisIsDumb;
        });
    }

    private Vector3[] PointsOnSphere(int n)
    {
        List<Vector3> upts = new List<Vector3>();
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / n;
        float x = 0;
        float y = 0;
        float z = 0;
        float r = 0;
        float phi = 0;

        for (var k = 0; k < n; k++)
        {
            y = k * off - 1 + (off / 2);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            upts.Add(new Vector3(x, y, z));
        }
        Vector3[] pts = upts.ToArray();
        return pts;
    }

    public GameObject getCurrentEnemy()
    {
        GameObject curEnemy = null;
        enemies.ForEach(x =>
        {
            if (x.transform.GetComponent<AudioSource>().enabled == true)
            {
                curEnemy = x;
            };
        });
        return curEnemy;
    }
}
