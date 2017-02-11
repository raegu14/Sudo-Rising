using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour {

    public GameObject track;
    public float speed;
	public string tileType;
	public int value;
	public char col;
	public int row;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        GetComponent<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(transform.position.y * 100) - 10;
        GetComponentInChildren<MeshRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;

        if (track != null)
        {
            transform.position += (track.transform.position - transform.position).normalized * Time.deltaTime * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            //pick up tile
            //stop at tile, pick it up, and then move
            tag = "taken";
            track = col.gameObject;
            speed = track.GetComponent<EnemyMovement>().speed;
            track.GetComponent<EnemyMovement>().hasTile = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>().tiles = GameObject.FindGameObjectsWithTag("tile");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && tileType != "permanent")
        {
            col.gameObject.GetComponent<PlayerMovement>().tile = gameObject;
            col.gameObject.GetComponent<PlayerMovement>().inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && tileType != "permanent")
        {
            col.gameObject.GetComponent<PlayerMovement>().inRange = false;
        }
    }
	
	public void Snap() //Snap to closest location
	{
		float x = transform.position.x;
		float y = transform.position.y;
		float nx = x;
		float ny = y;
		
		// Column
		if(x < -2.45f){nx = x;col='Z';}
		else if(x < -1.81f){nx = -2.08f;col='A';}
		else if(x < -1.29f){nx = -1.57f;col='B';}
		else if(x < -0.78f){nx = -1.05f;col='C';}
		else if(x < -0.24f){nx = -0.52f;col='D';}
		else if(x < 0.27f){nx = 0;col='E';}
		else if(x < 0.79f){nx = 0.52f;col='F';}
		else if(x < 1.32f){nx = 1.05f;col='G';}
		else if(x < 1.85f){nx = 1.57f;col='H';}
		else if(x < 2.36f){nx = 2.08f;col='I';}
		else{nx = x;}
		
		// Row
		if(y < -0.05f){ny = y;}
		else if(y < 0.58){ny = 0.31f;row=9;}
		else if(y < 1.09){ny = 0.82f;row=8;}
		else if(y < 1.62){ny = 1.34f;row=7;}
		else if(y < 2.14){ny = 1.86f;row=6;}
		else if(y < 2.63){ny = 2.38f;row=5;}
		else if(y < 3.15){ny = 2.89f;row=4;}
		else if(y < 3.66){ny = 3.43f;row=3;}
		else if(y < 4.22){ny = 3.94f;row=2;}
		else if(y < 4.73){ny = 4.46f;row=1;}
		else{ny = y;row=0;}
		transform.position = new Vector3(nx, ny, 0);
		
	}
	
	public void Check()
	{
		if(tileType == "permanent")
			return;
		Main main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>();
		GameObject[] tiles = main.tiles;
		bool check = true;
		foreach (GameObject obj in tiles)
		{
			TileMovement t = obj.GetComponent<TileMovement>();
			if(this == t)
				continue;
			if(t.row == row && t.value == value 
			|| t.col == col && t.value == value 
			|| BoxFinder(this) == BoxFinder(t) && t.value == value)
			{
				check = false;
				break;
			}
		}
		if(check) // No conflicts
		{
			// Turn tile green
			//transform.GetChild(0).GetComponent<TextMesh>().color = Color.green;
			tileType = "set";
			main.setCount++;
		}
		else
		{
			//Turn tile red
			//transform.GetChild(0).GetComponent<TextMesh>().color = Color.red;
			tileType = "incorrect";
		}
	}
	
	bool RowCheck(TileMovement t, GameObject[] tiles)
	{
		Debug.Log(t.row);
			Debug.Log("begin check");
		foreach (GameObject obj in tiles)
		{
			TileMovement other = obj.GetComponent<TileMovement>();
			
			Debug.Log(other.row);
			if(other != t && other.row == t.row && other.value == t.value)
				return false;
		}
		return true;
	}
	bool ColCheck(TileMovement t, GameObject[] tiles)
	{
		foreach (GameObject obj in tiles)
		{
			TileMovement other = obj.GetComponent<TileMovement>();
			if(other != t && other.col == t.col && other.value == t.value)
				return false;
		}
		return true;
	}
	bool BoxCheck(TileMovement t, GameObject[] tiles)
	{
		foreach (GameObject obj in tiles)
		{
			TileMovement other = obj.GetComponent<TileMovement>();
			if(other != t && BoxFinder(t) == BoxFinder(other) && t.value == other.value)
				return false;
		}
		return true;
	}
	int BoxFinder(TileMovement t)
	{
		// May need to be more strict aout rows 
		if(t.col == 'A' || t.col == 'B' || t.col == 'C') 
		{
			if(t.row < 4)
				return 1;
			if(t.row < 7)
				return 4;
			return 7;
		}
		else if(t.col == 'D' || t.col == 'E' || t.col == 'F')
		{
			if(t.row < 4)
				return 2;
			if(t.row < 7)
				return 5;
			return 8;
		}
		else if(t.col == 'G' || t.col == 'H' || t.col == 'I')
		{
			if(t.row < 4)
				return 3;
			if(t.row < 7)
				return 6;
			return 9;
		}
		return 0;
	}
}
