using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public GameObject[] tiles;

	// Use this for initialization
	void Start () {
        tiles = GameObject.FindGameObjectsWithTag("tile");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
