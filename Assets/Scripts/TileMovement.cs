using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour {

    public GameObject track;
	public BoardTile space;
	public string tileType;
	public int value;
	public char col;
	public int row;
	public int spawn;
	public bool alreadySet = false;
	private TileSpawn spawnner;
	private Main main;
	
	public AudioSource audio;
	public AudioClip rightSound;
	public AudioClip wrongSound;
	
	private Vector3 offset;

    // Use this for initialization
    void Start() {
		spawnner = GameObject.Find("TileSpawnPoints").GetComponent<TileSpawn>();
		main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>();
		offset = new Vector3(0, 0.8f, 0);
    }

    // Update is called once per frame
    void Update() {
		if(gameObject.tag != "permanent")
			GetComponent<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(transform.position.y * 100) - 10;

        if (track != null)
        {
            transform.position = (track.transform.position + offset);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (tag == "set")
        {
            EnemyMovement enemy = col.GetComponent<EnemyMovement>();
            OvenMovement oven = col.GetComponent<OvenMovement>();

            if (col.gameObject.tag == "enemy" && enemy.heldTile == null)
            {
                //pick up tile
                space.VacateSpace();
                tag = "taken";
                track = col.gameObject;
                enemy.heldTile = this.gameObject;
                track.GetComponent<EnemyMovement>().hasTile = true;
                main.tiles = GameObject.FindGameObjectsWithTag("tile");
            }
            else if (col.gameObject.tag == "oven" && oven.heldTile == null)
            {
                space.VacateSpace();
                tag = "taken";
                track = col.gameObject;
                oven.heldTile = this.gameObject;
                track.GetComponent<OvenMovement>().hasTile = true;
                main.tiles = GameObject.FindGameObjectsWithTag("tile");
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player" && gameObject.tag != "permanent")
        {
            if (!col.gameObject.GetComponent<PlayerMovement>().hasTile)
            {
                col.gameObject.GetComponent<PlayerMovement>().tile = gameObject;
                col.gameObject.GetComponent<PlayerMovement>().inRange = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && gameObject.tag != "permanent")
        {
            col.gameObject.GetComponent<PlayerMovement>().inRange = false;
        }
    }
	
	public void Check()
	{
		if(gameObject.tag == "permanent")
			return;
		gameObject.tag = "tile";
		/* TODO replace lol
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
		*/
	}
	
	public void Despawn()
	{
		spawnner.tileCounter[value]--;
		UpdateSpawn(spawn);
		// DO OTHER THINGS
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
