using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 2.0f;
    public char player = 'A';

    public bool inRange = false; //in range to pick up tile
    public GameObject tile;

    bool hasTile = false;
    float cooldown;

    string direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(transform.position.y * 100);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;

        if (transform.GetChild(0).rotation != Quaternion.identity)
        {
            transform.GetChild(0).rotation = Quaternion.identity;
        }

        if (player == 'A')
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction = "left";
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction = "right";
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                //play attack animation
                transform.GetChild(0).GetComponent<Animation>().Play();
                if(direction == "left")
                {
                    transform.GetChild(0).Rotate(new Vector3(0, 0, 90));
                }
                else
                {
                    transform.GetChild(0).Rotate(new Vector3(0, 0, -90));
                }
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //pick up item
                if (inRange && tile != null && Time.time > cooldown + 1.0f)
                {
					if(tile.GetComponent<TileMovement>().tileType != "permanent")
					{
						hasTile = true;
						cooldown = Time.time;
						tile.tag = "picked";
						tile.GetComponent<TileMovement>().track = gameObject;
						tile.GetComponent<TileMovement>().speed = speed;
					}
                    else
                    {
                        hasTile = false;
                        cooldown = Time.time;
                        tile.GetComponent<TileMovement>().track = null;
                        tile.GetComponent<TileMovement>().speed = 0;
                        tile = null;
                    }
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
                direction = "left";
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = "right";
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.RightControl))
            {
                //attack
                transform.GetChild(0).GetComponent<Animation>().Play();
                if (direction == "left")
                {
                    transform.GetChild(0).Rotate(new Vector3(0, 0, 90));
                }
                else
                {
                    transform.GetChild(0).Rotate(new Vector3(0, 0, -90));
                }
            }
            if (Input.GetKey(KeyCode.RightShift))
            {
                //pick up item
                if (inRange && tile != null && Time.time > cooldown + 1.0f)
                {
                    if (!hasTile)
                    {
						if(tile.GetComponent<TileMovement>().tileType != "permanent")
						{
							hasTile = true;
							cooldown = Time.time;
							tile.tag = "picked";
							tile.GetComponent<TileMovement>().track = gameObject;
							tile.GetComponent<TileMovement>().speed = speed;
						}
                    }
                    else
                    {
                        hasTile = false;
                        cooldown = Time.time;
                        tile.GetComponent<TileMovement>().track = null;
                        tile.GetComponent<TileMovement>().speed = 0;
                        tile = null;
                    }
                }
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
