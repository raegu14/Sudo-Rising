using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 2.0f;
    public char player = 'A';

    public bool inRange = false; //in range to pick up tile
    public GameObject tile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(transform.position.y * 100);

        if (player == 'A')
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                //attack
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                print("pressed shift");
                if (inRange)
                {
                    print("picked up");
                    tile.tag = "picked";
                    tile.GetComponent<TileMovement>().track = gameObject;
                    tile.GetComponent<TileMovement>().speed = speed;
                }
            }
        }

        else if (player == 'B')
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.RightControl))
            {
                //attack
            }
            if (Input.GetKey(KeyCode.RightShift))
            {
                //pick up item
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "tile")
        {
            //pick up tile
            col.gameObject.tag = "taken";
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>().tiles = GameObject.FindGameObjectsWithTag("tile");
        }
    }
}
