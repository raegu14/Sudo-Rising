using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
	
	public string powerType = "speed";
	public float multiplier = 2.0f;
	public int spawn;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			col.GetComponent<PlayerMovement>().Powerup(powerType, multiplier);
			GameObject.Find("TileSpawnPoints").GetComponent<TileSpawn>().spawnIndexMarker[spawn] = false;
			Destroy(gameObject);
		}
	}
}
