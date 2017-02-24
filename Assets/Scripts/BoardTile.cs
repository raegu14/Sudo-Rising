using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour {
	
	public GameObject tile;
	public int col, row;
	public PlayerMovement[] ps;
	private Board board;
	private Animator anim;
	public AnimationClip boom;
	

	// Use this for initialization
	void Start () {
		board = gameObject.transform.parent.GetComponent<Board>();
		anim = gameObject.GetComponent<Animator>();
		anim.Stop();
		ps = new PlayerMovement[2];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public bool IsOccupied(){
		return tile != null;
	}
	public bool OccupySpace(GameObject t, bool needsCheck){
		tile = t;
		if(needsCheck && !board.PlaceTile(col, row, t))
		{
			Explode();
			return false;
		}
		tile.GetComponent<SpriteRenderer>().sortingOrder = -666;
		if(tile.tag != "permanent")
			tile.tag = "set";
		tile.transform.position = gameObject.transform.position;
		gameObject.GetComponent<SpriteRenderer>().sprite = null;
        return true;
	}
	public void VacateSpace(){
		tile = null;
	}
	public void Lock()
	{
		if(tile == null)
		{
			Debug.Log("WHY ARE YOU LOCKING A NONEXISTENT TILE");
			return;
		}
		if(tile.tag != "permanent")
		{			
			tile.GetComponent<SpriteRenderer>().sprite = board.spawnner.GetSprite("lock", tile.GetComponent<TileMovement>().value);
			tile.tag = "permanent";
		}
	}
	
	public void Explode()
	{
		tile = null;
		anim.Play("Boom Boom");
		foreach(PlayerMovement p in ps)
		{
			if(p != null)
				StartCoroutine(p.Knockback(gameObject.transform.position));
		}
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		// highlight when player is standing over it
		if(col.tag == "putindicator" && !IsOccupied())
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = board.glowSprite;
			PlayerMovement p = col.transform.parent.GetComponent<PlayerMovement>();
			p.boardSpace = gameObject;
			ps[p.player - 'A'] = p;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag == "putindicator" && !IsOccupied())
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = null;
			PlayerMovement p = col.transform.parent.GetComponent<PlayerMovement>();
			if(p.boardSpace == gameObject)
				p.boardSpace = null;
			ps[p.player - 'A'] = null;
		}
	}
}
