using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour {
	
	public GameObject tile;
	public int col, row;
	private Board board;
	private PlayerMovement[] ps;

	// Use this for initialization
	void Start () {
		board = gameObject.transform.parent.GetComponent<Board>();
		ps = new PlayerMovement[2];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public bool IsOccupied(){
		return tile != null;
	}
	public bool OccupySpace(GameObject t, bool needsCheck){
		if(needsCheck && !board.PlaceTile(col, row, t))
			return false;
		tile = t;
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
		tile.tag = "permanent";
		//tile.GetComponent<SpriteRenderer>().sprite = board.GetSprite("lock", tile.GetComponent<TileMovement>().value);
	}
	
	public void Explode()
	{
		Debug.Log("explode");
		foreach(PlayerMovement p in ps)
			p.Knockback(gameObject.transform.position);
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
