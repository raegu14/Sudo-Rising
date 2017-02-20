using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class spawns all tiles, including those on the board.
public class TileSpawn : MonoBehaviour {

    public GameObject tilePrefab;
	public GameObject tileSpritesParent;
	public GameObject litSpritesParent;
	public GameObject permSpritesParent;
	public Sprite[] tileSprites;
	private Sprite[] litSprites;
	private Sprite[] permSprites;

	public GameObject[] tileSpaces;
	public GameObject[] tiles;

    private Main main;
	private Board board;
	

	public int[] tileCounter;
	private bool[] spawned;
	public bool[] spawnIndexMarker;

	int activeTileCount = 0;
	int maxActiveTileCount = 9;
	
	int setCount = 0;
	int totalSpawns;


    // Use this for initialization
    void Start () {
        main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>();
		board = GameObject.Find("Board").GetComponent<Board>();
		tileSpaces = GameObject.FindGameObjectsWithTag("tilespawn");
		tiles = GameObject.FindGameObjectsWithTag("tile");

		tileCounter  = new int[10];
		spawnIndexMarker = new bool[tileSpaces.Length];
		spawned = new bool[10];
		
		tileSprites = new Sprite[9];
		litSprites = new Sprite[9];
		permSprites = new Sprite[9];
		for(int i = 0; i < 9; i++)
		{
			tileSprites[i] = tileSpritesParent.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
			litSprites[i] = litSpritesParent.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
			permSprites[i] = permSpritesParent.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
		}
	}

	// Update is called once per frame
	void Update () {
		tiles = GameObject.FindGameObjectsWithTag("tile");
		activeTileCount = tiles.Length;
	
        if (main.getGameStatus() == "Pregame" && activeTileCount < maxActiveTileCount)
        {
            for (int i = 1; i < 10; i++)
            {
                if (!spawned[i] && tileCounter[i] < 10 && activeTileCount < maxActiveTileCount)
                {
                    int index = Random.Range(0, tileSpaces.Length);
					if(!spawnIndexMarker[index])
					{
						spawnIndexMarker[index] = true;
						Vector3 loc = tileSpaces[index].transform.position;
						SpawnTile(loc.x, loc.y, i, index);
						tileCounter[i]++;
						activeTileCount++;
						spawned[i] = true;
					}
                }

            }
        }
    }
	
	public void FreeSpawn(int val)
	{
		spawned[val] = false;
	}


	public void addActiveTile () {
		if (activeTileCount < maxActiveTileCount) {


			int index = Random.Range(0, tileSpaces.Length);
			//StartCoroutine(tileSpawnAnim(index));
			tileSpawnAnim(index);
			activeTileCount++;
		
		}

	}
	
	//set permanent tile
	public void SpawnTile(char col, int row, int value)
	{
		GameObject tile = Instantiate(tilePrefab);
		tile.GetComponent<SpriteRenderer>().sprite = permSprites[value-1];
		tile.GetComponent<TileMovement>().tag = "permanent";
		tile.GetComponent<TileMovement>().value = value;
		board.PlaceTile(col, row, tile);
		tileCounter[value]++;
	}
	
	//spawn a basic tile with the value
	void SpawnTile(float x, float y, int val, int spawnIndex)
	{
		GameObject tile = Instantiate(tilePrefab);
		tile.GetComponent<SpriteRenderer>().sprite = tileSprites[val-1];
		tile.GetComponent<TileMovement>().spawn = spawnIndex;
		tile.GetComponent<TileMovement>().value = val;
		tile.GetComponent<TileMovement>().col = '0';
		tile.GetComponent<TileMovement>().row = 0;
		tile.transform.position = new Vector3(x, y, 0);
		tileCounter[val]++;
	}


	public int getActiveTileCount() {
		return activeTileCount;
	}


	//IEnumerator tileSpawnAnim(int index)
	void tileSpawnAnim(int index)
	{
		Instantiate(tilePrefab, tileSpaces[index].transform.position, tileSpaces[index].transform.rotation);
	}
	
	public Sprite GetSprite(string spriteType, int value)
	{
		if(value < 1 || value > 9)
		{
			Debug.Log("why you try to spawn invalid tiles :c");
		}
		switch(spriteType)
		{
			case "lit":
				return litSprites[value-1];
			case "perm":
				return permSprites[value-1];
			default:
				return tileSprites[value-1];
		}
	}
}

