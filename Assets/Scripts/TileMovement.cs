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
	public int spawn;
	private TileSpawn spawnner;
	private Main main;
	
	public AudioSource audio;
	public AudioClip rightSound;
	public AudioClip wrongSound;

    // Use this for initialization
    void Start() {
		spawnner = GameObject.Find("TileSpawnPoints").GetComponent<TileSpawn>();
		main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>();
    }

    // Update is called once per frame
    void Update() {
		if(gameObject.tag != "permanent")
			GetComponent<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(transform.position.y * 100) - 10;
        GetComponentInChildren<MeshRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;

        if (track != null)
        {
            transform.position += (track.transform.position - transform.position).normalized * Time.deltaTime * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (tag == "set") {
			EnemyMovement enemy = col.GetComponent<EnemyMovement>();
            if (col.gameObject.tag == "enemy" && enemy.heldTile == null)
            {
                //pick up tile
                tag = "taken";
                track = col.gameObject;
				enemy.heldTile = this.gameObject;
                speed = track.GetComponent<EnemyMovement>().speed;
                track.GetComponent<EnemyMovement>().hasTile = true;
                main.tiles = GameObject.FindGameObjectsWithTag("tile");
            }
        }
		if (col.tag == "wall")
		{
			Despawn();
		}
		UpdateSpawn(spawn);
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player" && gameObject.tag != "permanent")
        {
            col.gameObject.GetComponent<PlayerMovement>().tile = gameObject;
            col.gameObject.GetComponent<PlayerMovement>().inRange = true;
        }
		UpdateSpawn(spawn);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && gameObject.tag != "permanent")
        {
            col.gameObject.GetComponent<PlayerMovement>().inRange = false;
        }
		UpdateSpawn(spawn);
    }
	
	public bool Snap() //Snap to closest location
	{
		float x = transform.position.x;
		float y = transform.position.y;
		float nx = x;
		float ny = y;
		
		// Column
		if(x < -2.45f){nx = x;col='0';}
		else if(x < -1.81f){nx = -2.08f;col='A';}
		else if(x < -1.29f){nx = -1.57f;col='B';}
		else if(x < -0.78f){nx = -1.05f;col='C';}
		else if(x < -0.24f){nx = -0.52f;col='D';}
		else if(x < 0.27f){nx = 0;col='E';}
		else if(x < 0.79f){nx = 0.52f;col='F';}
		else if(x < 1.32f){nx = 1.05f;col='G';}
		else if(x < 1.85f){nx = 1.57f;col='H';}
		else if(x < 2.36f){nx = 2.08f;col='I';}
		else{nx = x;col='0';}
		
		// Row
		if(y < -0.05f){ny = y;row=0;}
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
		if(main.noSnap[col - 'A', row])
			return false;
		transform.position = new Vector3(nx, ny, 0);
		return true;
		
	}
	
	public void Check()
	{
		if(gameObject.tag == "permanent")
			return;
		gameObject.tag = "tile";
		if(col != '0' && row != 0)
		{
			if(main.solution[col-'A', row] == value)
			{
				gameObject.tag = "set";
				audio.clip = rightSound;
				audio.Play();
			}
			else
			{
				audio.clip = wrongSound;
				audio.Play();
				main.lives--;
			}
		}
		else
		{
			//Tile was dropped in an area that doesn't matter i guess
		}
	}
	
	public void Despawn()
	{
		Debug.Log("lol");
		spawnner.tileCounter[value]--;
		Destroy(this.gameObject);
	}
	public void UpdateSpawn(int index)
	{
		if(spawn >= 0)
		{
			spawnner.spawnIndexMarker[index] = false;
			spawn = -1;
		}
	}
}
