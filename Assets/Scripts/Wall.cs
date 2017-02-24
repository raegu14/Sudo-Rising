using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "enemy")
		{
			EnemyMovement e = col.GetComponent<EnemyMovement>();
			if(e.pass == 0)
				e.pass++;
			else
				e.Despawn();
		}
	}
}
