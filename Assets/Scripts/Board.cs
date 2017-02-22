using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
	
	public Sprite glowSprite;
	
	private BoardTile[,] board; // col, row
	private Main main;
	private TileSpawn spawnner;
	private int[,] solution;
	private int boxCCount = 0;

	// Use this for initialization
	void Start () {
		solution = new int[9, 9];
		main = GameObject.Find("Main Camera").GetComponent<Main>();
		spawnner = GameObject.Find("TileSpawnPoints").GetComponent<TileSpawn>();
		board = new BoardTile[9, 9];
		
		// Add all tilespaces to the board.
		for(int j = 0; j < 9; j++)
		{
			for(int i = 0; i < 9; i++)
			{
				board[j, i] = GameObject.Find(((char)('A' + j)).ToString() + (i+1).ToString()).GetComponent<BoardTile>();
				board[j, i].col = j;
				board[j, i].row = i;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetSolution(char col, int row, int val)
	{
		solution[col-'A', row-1] = val;
	}
	

	// Set tile from board
	// Uses Chess Notation; please be aware that indexing starts at 1
	public void PlaceTile(char col, int row, GameObject tile)
	{
		if(col == '0' || row == 0)
			return;
		bool perm = false;
		TileMovement t = tile.GetComponent<TileMovement>();
		if(t.tag == "permanent")
			perm = true;
		BoardTile space = board[col-'A', row-1];
		if(!space.IsOccupied())
		{
			// Check if tile is correct
			if(solution[col-'A', row-1] != t.value) // Incorrect tile
			{
				//blow shit up
				t.Despawn();
				Debug.Log("YOU WERE WRONG-DIEEE");
				spawnner.FreeSpawn(t.value);
				return;
			}
			
			space.OccupySpace(tile, false);
			if(perm)
				space.Lock();
			t.transform.position = space.transform.position;
			t.space = space;
			if(!t.alreadySet) // haven't preveiously been placed
			{
				t.alreadySet = true;
				spawnner.FreeSpawn(t.value);
			}
			// TODO do other things
		}
		// just drop the tile
	}
	
	// Set tile from board space. Return true on success, false otherwise
	// 0 indexed
	public bool PlaceTile(int col, int row, GameObject tile)
	{
		TileMovement t = tile.GetComponent<TileMovement>();
		if(solution[col, row] != t.value) // Incorrect tile
		{
			//Despawn 
			t.Despawn();
			Debug.Log("YOU WERE WRONG--DIEEE");
			spawnner.FreeSpawn(t.value);
			return false;
		}
		t.space = board[col, row];
		if(!t.alreadySet)
		{
			t.alreadySet = true;
			spawnner.FreeSpawn(t.value);
		}
		SpawnCheck(col, row, t.value);
		return true;
	}
	
	public void VacateSpace(GameObject tile)
	{
		TileMovement t = tile.GetComponent<TileMovement>();
		if(tile.tag != "permanent")
		{			
			BoardTile space = board[t.col-'A', t.row];
			space.VacateSpace();
			tile.tag = "tile";
		}
	}
	
	//Check whether to spawn a powerup
	//Assumes that all tiles placed are correct
	void SpawnCheck(int col, int row, int val)
	{
		bool validRow = (row >= 0 && row < 10);
		bool validCol = (col >= 0 && col < 10);
		string log = "col:" + col + " ,row:" + row;
		// Check the tile's row
		if(validRow)
		{
			bool valid = true;
			for(int i = 0; i < 9; i++)
			{
				if(!board[i, row].IsOccupied()) // incomplete row
				{
					valid = false;
					break;
				}
			}
			if(valid)
			{
				LockTiles("row", row);
			}
		}
		// Check the tile's column
		if(validCol)
		{
			bool valid = true;
			for(int i = 0; i < 9; i++)
			{
				if(!board[col, i].IsOccupied())
				{
					valid = false;
					break;
				}
			}
			if(valid)
			{
				LockTiles("col", col);
			}
		}
		
		// Check the tile's box
		// That is, check if the box has been completed
		if(validRow && validCol)
		{
			if(col < 'D') // 1st column of boxes
			{
				if(row < 3)
				{
					if(BoxCheck(0, 0))
					{
						boxCCount++;
						LockBox(0, 0);
					}
				}
				else if(row < 6)
				{
					if(BoxCheck(0, 3))
					{
						boxCCount++;
						LockBox(0, 3);
					}
				}
				else
				{
					if(BoxCheck(0, 6))
					{
						boxCCount++;
						LockBox(0, 6);
					}
				}
			}
			else if(col < 'G') // 2nd column of boxes
			{
				if(row < 3)
				{
					if(BoxCheck(3, 0))
					{
						boxCCount++;
						LockBox(3, 0);
					}
				}
				else if(row < 6)
				{
					if(BoxCheck(3, 3))
					{
						boxCCount++;
						LockBox(3, 3);
					}
				}
				else
				{
					if(BoxCheck(3, 6))
					{
						boxCCount++;
						LockBox(3, 6);
					}
				}
			}
			else // 3rd column of boxes
			{
				if(row < 3)
				{
					if(BoxCheck(6, 0))
					{
						boxCCount++;
						LockBox(6, 0);
					}
				}
				else if(row < 6)
				{
					if(BoxCheck(6, 3))
					{
						boxCCount++;
						LockBox(6, 3);
					}
				}
				else
				{
					if(BoxCheck(6, 6))
					{
						boxCCount++;
						LockBox(6, 6);
					}
				}
			}
		}
		if(boxCCount == 9)
		{
			// TODO finish game
		}
	}
	
	/* Boxes are indexed thusly in the bool spawn array
	   0 | 1 | 2
	   3 | 4 | 5
	   6 | 7 | 8                    */
	bool BoxCheck(int startCol, int startRow)
	{
		Debug.Log("box check " + startCol + " " + startRow);
		for(int j = startCol; j < startCol+3; j++)
		{
			for(int i = startRow; i < startRow+3; i++)
			{
				if(!board[j, i].IsOccupied())
					return false;
			}
		}
		Debug.Log("it's fine");
		return true;
	}
	
	void LockTiles(string kind, int index)
	{
		Debug.Log("Locking");
		switch(kind)
		{
			case "row":
				for(int i = 0; i < 9; i++)
				{
					board[i, index].Lock();
				}
				break;
			case "col":
				for(int i = 0; i < 9; i++)
				{
					board[index, i].Lock();
				}
				break;
			default:
				break;
		}
	}
	
	void LockBox(int startCol, int startRow)
	{
		Debug.Log("Locking");
		for(int j = startCol; j < startCol+3; j++)
		{
			for(int i = startRow; i < startRow+3; i++)
			{
				board[j, i].Lock();
			}
		}
	}
	
	void pupSpawn() // TODO Spawn a powerup
	{
		print("I'm spawning a powerup!");
	}

}
