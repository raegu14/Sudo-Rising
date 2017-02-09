using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public GameObject[] tiles;
    
    //timer to end game
    float timer = 500f;
    
    // week 2, ranged combat to end game

	// Use this for initialization
	void Start () {
        tiles = GameObject.FindGameObjectsWithTag("tile");
    }

    // Update is called once per frame
    void Update () {
        if(timer < Time.time)
        {
            print("gameOver");
        }
		
	}
}
