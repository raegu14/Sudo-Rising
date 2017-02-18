using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawn : MonoBehaviour {

    public GameObject tilePrefab;

	public GameObject[] tileSpaces;
	public GameObject[] tiles;

    Main main;

	public int[] tileCounter;
	public bool[] spawnIndexMarker;

	int activeTileCount = 0;
	int minActiveTileCount = 9;
	int maxActiveTileCount = 13;

    int aliveEnemyCount = 0;
    int minAliveEnemyCount = 9;
    int maxAliveEnemyCount = 13;
	
	int setCount = 0;
	int limit = 10;
	int totalSpawns;


    // Use this for initialization
    void Start () {
        main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>();
		tileSpaces = GameObject.FindGameObjectsWithTag("tilespawn");
		tiles = GameObject.FindGameObjectsWithTag("tile");
		totalSpawns = main.setTiles;

		tileCounter  = new int[10];
		spawnIndexMarker = new bool[tileSpaces.Length];
	}

	// Update is called once per frame
	void Update () {
		tiles = GameObject.FindGameObjectsWithTag("tile");
		activeTileCount = tiles.Length;
		
		if(limit*4 < totalSpawns)
			limit *= 2;

        if (main.getGameStatus() == "Pregame" && activeTileCount < minActiveTileCount)
        {
            for (int i = 1; i < 10; i++)
            {
                if (tileCounter[i] < limit && activeTileCount < maxActiveTileCount)
                {
                    int index = Random.Range(0, tileSpaces.Length);
					if(!spawnIndexMarker[index])
					{
						spawnIndexMarker[index] = true;
						Vector3 loc = tileSpaces[index].transform.position;
						SetTile(loc.x, loc.y, i, "tile", index);
						tileCounter[i]++;
						activeTileCount++;
						totalSpawns++;
					}
                }

            }
        }
    }


	public void addActiveTile () {
		if (activeTileCount < maxActiveTileCount) {


			int index = Random.Range(0, tileSpaces.Length);
			//StartCoroutine(tileSpawnAnim(index));
			tileSpawnAnim(index);
			activeTileCount++;
		
		}

	}

	void SetTile(float x, float y, int val, string property, int spawnIndex)
	{
		GameObject tile = Instantiate(tilePrefab);
		tile.GetComponent<TileMovement>().tag = property;
		tile.GetComponent<TileMovement>().spawn = spawnIndex;

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

