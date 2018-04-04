<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

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
			col.GetComponent<EnemyMovement>().die();
		}
        else if (col.gameObject.tag == "oven")
        {
            col.GetComponent<OvenMovement>().die();
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

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
			col.GetComponent<EnemyMovement>().die();
		}
        else if (col.gameObject.tag == "oven")
        {
            col.GetComponent<OvenMovement>().die();
        }
    }
}
>>>>>>> 317fdfc599ed86cfd8576b5de162c94105288a31
