using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawn : MonoBehaviour {

    public GameObject tilePrefab;

	public GameObject[] tileSpaces;
	public GameObject[] tiles;

    Main main;

	public int[] tileCounter;
	public int[] spawnIndexCounter;

	int activeTileCount = 0;
	int minActiveTileCount = 9;
	int maxActiveTileCount = 13;

    int aliveEnemyCount = 0;
    int minAliveEnemyCount = 9;
    int maxAliveEnemyCount = 13;
	
	int setCount = 0;


    // Use this for initialization
    void Start () {
        main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>();
		tileSpaces = GameObject.FindGameObjectsWithTag("tilespawn");
		tiles = GameObject.FindGameObjectsWithTag("tiles");

		tileCounter  = new int[10];
		spawnIndexCounter = new int[tileSpaces.Length];


	}

	// Update is called once per frame
	void Update () {
		tileSpaces = GameObject.FindGameObjectsWithTag("tilespawn");

		/*if (main.getGameStatus() == Pregame) {
			int tilesNeeded = Random.Range (minActiveTileCount, maxActiveTileCount);
			for (int i = 1; i < 9; i++) {
				if (tileCounter [i] == 0) {
					int index = Random.Range (0, tileSpaces.Length);
					tileSpaces[index].GetComponent<TileSpawn>().tag = unavailable;
					Instantiate(tiles, tileSpaces[index].transform.position, tileSpaces[index].transform.rotation);
					tileCounter[val]++;

				}
					
			}
            */



			//Loop through values tiles
			//Select random spawn location that hasn't been used
			//Animate
    }


	public void addActiveTile () {
		if (activeTileCount < maxActiveTileCount) {


			int index = Random.Range(0, tileSpaces.Length);
			//StartCoroutine(tileSpawnAnim(index));
			tileSpawnAnim(index);
			activeTileCount++;
		
		}

	}

	void SetTile(float x, float y, int val, string property)
	{
		GameObject tile = Instantiate(tilePrefab);
		tile.GetComponent<TileMovement>().tag = property;

		if(property == "permanent")
			setCount++;
		tile.transform.GetChild(0).transform.GetComponent<TextMesh>().text = val.ToString();
		tile.GetComponent<TileMovement>().value = val;
		tile.GetComponent<TileMovement>().col = '0';
		tile.GetComponent<TileMovement>().row = 0;
		tile.transform.position = new Vector3(x, y, 0);
		tileCounter[val]++;
	}


	public int getActiveTileCount() {
		return activeTileCount;
	}

	public void reduceEnemyCount(int num) {
		if(aliveEnemyCount >= num) {

			aliveEnemyCount = aliveEnemyCount - num;
		}
		return;
	}

	//IEnumerator tileSpawnAnim(int index)
	void tileSpawnAnim(int index)
	{
		Instantiate(tilePrefab, tileSpaces[index].transform.position, tileSpaces[index].transform.rotation);
	}

}

