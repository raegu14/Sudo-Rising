<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public bool pregame = true;
    public float timer;

    public GameObject calmSound;
    public bool calmSoundStop = false;

    public AudioClip normal;
    public AudioClip fast;
    public AudioSource aud;
    private bool normalSpeed = true;

	public GameObject[] enemySpaces;
	public GameObject[] enemies;
	public GameObject enemy0;
	public GameObject enemy1;
    public GameObject enemy2;

	int aliveEnemyCount = 0;
	int maxAliveEnemyCount = 7;
	float spawnTimer;
	float spawnRate = 2f;

	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
		enemySpaces = GameObject.FindGameObjectsWithTag("enemyspawn");
		spawnTimer = Time.time;
		enemies = new GameObject[3];
		enemies[0] = enemy0;
		enemies[1] = enemy1;
        enemies[2] = enemy2;
	}
	
	// Update is called once per frame
	void Update () {

        if (!pregame && Time.time > timer)
        {
            /*if (!calmSoundStop)
            {
                calmSoundStop = true;
                calmSound.GetComponent<AudioSource>().Pause();
                GetComponent<AudioSource>().Play();
            } */
            //Periodic Spawn with Enemy Max and small Random component
            if (spawnTimer + spawnRate < Time.time
                && aliveEnemyCount < maxAliveEnemyCount
                && Random.Range(0, 100) < 70)
            {
                spawnTimer = Time.time;
                int index = Random.Range(0, 20);
                int enemyType = Random.Range(0, 3); //TODO establish more logic for enemyType
                Instantiate(enemies[enemyType], enemySpaces[index].transform.position, enemySpaces[index].transform.rotation);
                aliveEnemyCount++;
            }

            if (aliveEnemyCount > maxAliveEnemyCount)
            {
                if (normalSpeed)
                {
                    normalSpeed = false;
                    aud.clip = fast;
                    aud.Play();
                    aud.loop = true;
                }
            }

            if (aliveEnemyCount <= maxAliveEnemyCount / 2)
            {
                if (!normalSpeed)
                {
                    normalSpeed = true;
                    aud.clip = normal;
                    aud.Play();
                    aud.loop = true;
                }
            }
        }
        else if (pregame)
        {
            timer = Time.time + 5f;
        }
    }


	public int getEnemyCount() {
		return aliveEnemyCount;
	}

	public void reduceEnemyCount(int num) {
		if(aliveEnemyCount >= num) {
			
			aliveEnemyCount = aliveEnemyCount - num;
		}
		return;
	}
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public bool pregame = true;
    public float timer;

    public AudioClip normal;
    public AudioClip fast;
    private AudioSource aud;
    private bool normalSpeed = true;

	public GameObject[] enemySpaces;
	public GameObject[] enemies;
	public GameObject enemy0;
	public GameObject enemy1;
	int aliveEnemyCount = 0;
	int maxAliveEnemyCount = 5;
	float spawnTimer;
	float spawnRate = 2f;

	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
		enemySpaces = GameObject.FindGameObjectsWithTag("enemyspawn");
		spawnTimer = Time.time;
		enemies = new GameObject[2];
		enemies[0] = enemy0;
		enemies[1] = enemy1;
	}
	
	// Update is called once per frame
	void Update () {

        if (!pregame && Time.time > timer)
        {

            //Periodic Spawn with Enemy Max and small Random component
            if (spawnTimer + spawnRate < Time.time
                && aliveEnemyCount < maxAliveEnemyCount
                && Random.Range(0, 100) < 70)
            {
                spawnTimer = Time.time;
                int index = Random.Range(0, 20);
                int enemyType = Random.Range(0, 2); //TODO establish more logic for enemyType
                Instantiate(enemies[enemyType], enemySpaces[index].transform.position, enemySpaces[index].transform.rotation);
                aliveEnemyCount++;
            }

            if (aliveEnemyCount > maxAliveEnemyCount)
            {
                if (normalSpeed)
                {
                    normalSpeed = false;
                    aud.clip = fast;
                    aud.Play();
                    aud.loop = true;
                }
            }

            if (aliveEnemyCount <= maxAliveEnemyCount / 2)
            {
                if (!normalSpeed)
                {
                    normalSpeed = true;
                    aud.clip = normal;
                    aud.Play();
                    aud.loop = true;
                }
            }
        }
        else if (pregame)
        {
            timer = Time.time + 5f;
        }
    }


	public int getEnemyCount() {
		return aliveEnemyCount;
	}

	public void reduceEnemyCount(int num) {
		if(aliveEnemyCount >= num) {
			
			aliveEnemyCount = aliveEnemyCount - num;
		}
		return;
	}
}
>>>>>>> 317fdfc599ed86cfd8576b5de162c94105288a31
