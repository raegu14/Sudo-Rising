using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

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
		enemySpaces = GameObject.FindGameObjectsWithTag("enemyspawn");
		spawnTimer = Time.time;
		enemies = new GameObject[2];
		enemies[0] = enemy0;
		enemies[1] = enemy1;
	}
	
	// Update is called once per frame
	void Update () {

		//Periodic Spawn with Enemy Max and small Random component
		if(spawnTimer + spawnRate < Time.time 
			&& aliveEnemyCount<maxAliveEnemyCount
			&& Random.Range(0,100) < 90)
		{
			spawnTimer = Time.time;
			int index = Random.Range(0, 20);
			int enemyType = Random.Range(0, 2); //TODO establish more logic for enemyType
			Instantiate(enemies[enemyType], enemySpaces[index].transform.position, enemySpaces[index].transform.rotation);
			aliveEnemyCount++;
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
