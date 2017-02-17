using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
	
	private string powerType = "speed";
	public float multiplier = 2.0f;

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
			Destroy(gameObject);
		}
	}
}
