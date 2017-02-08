using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;
    public Vector3 direction;

    public GameObject[] tiles;
    GameObject closestTile;

    public bool hasTile;

	// Use this for initialization
	void Start ()
    {
        //find closest tile and set direction to it
        tiles = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>().tiles;
        if (tiles.Length > 0)
        {
            closestTile = tiles[0];
            direction = closestTile.transform.position - transform.position;
        }
        else
        {
            direction = -transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(transform.position.y * 100);

        if (hasTile)
        {
            //go offscreen
        }
        else
        {
            //find closest tile and set direction to it
            tiles = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>().tiles;
            if (tiles.Length > 0)
            {
                closestTile = tiles[0];
                direction = closestTile.transform.position - transform.position;
            }
            foreach (GameObject obj in tiles)
            {
                if ((obj.transform.position - transform.position).magnitude < direction.magnitude)
                {
                    closestTile = obj;
                    direction = obj.transform.position - transform.position;
                }
            }
        }

        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}