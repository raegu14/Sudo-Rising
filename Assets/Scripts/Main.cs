using System.Collections;
using System.Collections.Generic;
using System.IO;  
using UnityEngine;

public class Main : MonoBehaviour {

    public GameObject[] tiles;
	public GameObject tilePrefab;

    public GameObject[] enemySpaces;
    public GameObject[] enemies;
    float spawnTimer;
    float spawnRate = 2f;
	
	public int lives = 5;

    string gameStatus;

    //timer to end game
    float timer = 500f;
	
	// UI
	public TextMesh livesUI;
	public TextMesh countUI;
	
	// Important objects to keep track of
	private Board board;
	private TileSpawn spawnner;

	// Use this for initialization
	void Start () {
        enemySpaces = GameObject.FindGameObjectsWithTag("enemyspawn");
        tiles = GameObject.FindGameObjectsWithTag("tile");
        spawnTimer = Time.time;
		board = GameObject.Find("Board").GetComponent<Board>();
		spawnner = GameObject.Find("TileSpawnPoints").GetComponent<TileSpawn>();
        ReadLevel("board");
        gameStatus = "Pregame";
    }

    // Update is called once per frame
    void Update () {
		if(gameStatus == "GameOver")
		{
			print("gameover");
		}
		else if(gameStatus == "Winner")
		{
			print("winner");
		}
		int setCount = GameObject.FindGameObjectsWithTag("set").Length;
        tiles = GameObject.FindGameObjectsWithTag("tile");
		livesUI.text = "Lives " + lives;
		countUI.text = setCount + " Correct";

        if (lives < 1)
        {
            gameStatus = "GameOver";
        }
		
	}
	void ReadLevel(string fileName) 
	{
		TextAsset levelData = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
		StringReader reader = new StringReader(levelData.text);
		if(reader == null)
		{
			Debug.Log("Can't find or read level data.");
		}
		else
		{
			string line = reader.ReadLine();
			while(line != null)
			{
				string[] coord = line.Split(':');
				if (coord.Length > 0)
				{
					string firstRead = coord[0];
					if(firstRead == "ID") //ID 
					{
						// DO SOMETHING WITH THE ID VALUE
					}
					else if(firstRead == "Difficulty") // Difficulty
					{
						// DO SOMETHING WITH THE DIFFICULTY VALUE
					}
					else // Assumed to be actual coordinates
					{
						int val = int.Parse(coord[1]);
						int row = int.Parse(firstRead[1].ToString());
						char col = firstRead[0];
						board.SetSolution(col, row, val);
						if(coord[2] == "P")
						{
							spawnner.SpawnTile(col, row, val);
						}
						
					}
				}
				line = reader.ReadLine();
			}
		}		
	}
	
	
    public string getGameStatus()
    {
        return gameStatus;
    }

}
