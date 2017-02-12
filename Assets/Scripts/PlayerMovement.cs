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

    string xMove = "not moving";
    string yMove = "not moving";

    string direction;
	private int speedTimer = 0, attackTimer = 0;
	private bool speedPowerup = false, attackPowerup = false;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sortingOrder = 1000;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
    }

    // Update is called once per frame
    void Update () {
		// Handle powerups
		if(speedTimer > 0)
		{
			speedTimer--;
		}
		else if(!speedPowerup)
		{
			speed = 2.0f;
		}
		/* STUB FOR ATTACK POWERUPS
		if(attackTimer > 0)
		{
			attackTimer--;
		}
		else if(!attackPowerup)
		{
			
		}
		*/
        
        //reset weapons
        if (transform.GetChild(0).rotation != Quaternion.identity)
        {
            transform.GetChild(0).rotation = Quaternion.identity;
        }

        if (player == 'A')
        {
            //move vertically
            if (yMove == "not moving")
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    yMove = "Up";
                    transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    yMove = "Down";
                    transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
                }
            }

            if(yMove == "Up")
            {
                if (Input.GetKeyUp(KeyCode.W))
                {
                    if (Input.GetKey(KeyCode.S))
                    {
                        yMove = "Down";
                        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
                    }
                    else
                    {
                        yMove = "not moving";
                    }
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
                }
            }

            if (yMove == "Down")
            {
                if (Input.GetKeyUp(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        yMove = "Up";
                        transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
                    }
                    else
                    {
                        yMove = "not moving";
                    }
                }
                else
                {
                    transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
                }
            }


            if (xMove == "not moving")
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    xMove = "Left";
                    transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    xMove = "Right";
                    transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
                }
            }

            if (xMove == "Left")
            {
                if (Input.GetKeyUp(KeyCode.A))
                {
                    if (Input.GetKey(KeyCode.D))
                    {
                        xMove = "Right";
                        transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
                    }
                    else
                    {
                        xMove = "not moving";
                    }
                }
                else
                {
                    transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
                }
            }

            if (xMove == "Right")
            {
                if (Input.GetKeyUp(KeyCode.D))
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        xMove = "Left";
                        transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
                    }
                    else
                    {
                        xMove = "not moving";
                    }
                }
                else
                {
                    transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
                }
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
            if (Input.GetKey(KeyCode.E))
            {
                //pick up item
                if (inRange && tile != null && Time.time > cooldown + 1.0f)
                {
					if(!hasTile)
					{
						if(tile.tag != "permanent")
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
						tile.GetComponent<TileMovement>().Snap();
						tile.GetComponent<TileMovement>().Check();
						tile = null;
					}
				}
            }
        }

        else if (player == 'B')
        {
            //move vertically
            if (yMove == "not moving")
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    yMove = "Up";
                    transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    yMove = "Down";
                    transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
                }
            }

            if (yMove == "Up")
            {
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        yMove = "Down";
                        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
                    }
                    else
                    {
                        yMove = "not moving";
                    }
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
                }
            }

            if (yMove == "Down")
            {
                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        yMove = "Up";
                        transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
                    }
                    else
                    {
                        yMove = "not moving";
                    }
                }
                else
                {
                    transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
                }
            }


            if (xMove == "not moving")
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    xMove = "Left";
                    transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    xMove = "Right";
                    transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
                }
            }

            if (xMove == "Left")
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        xMove = "Right";
                        transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
                    }
                    else
                    {
                        xMove = "not moving";
                    }
                }
                else
                {
                    transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
                }
            }

            if (xMove == "Right")
            {
                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        xMove = "Left";
                        transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
                    }
                    else
                    {
                        xMove = "not moving";
                    }
                }
                else
                {
                    transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
                }
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
						if(tile.tag != "permanent")
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
						tile.GetComponent<TileMovement>().Snap();
						tile.GetComponent<TileMovement>().Check();
                        tile = null;
                    }
                }
            }
        }
    }
	
	public void Powerup(string pType, float multiplier)
	{
		if(pType == "speed")
		{
			speed *= multiplier;
			speedTimer = 1000;
			speedPowerup = true;
		}
		// TODO add other powerups
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "tile")
        {
            //pick up tile
            col.gameObject.tag = "taken";
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>().tiles = GameObject.FindGameObjectsWithTag("tile");
			// TODO add tile row/column reclassing
        }
    }
}
