﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 2.0f;
    public char player = 'A';

    public bool inRange = false; //in range to pick up tile
    public GameObject tile;

    public Sprite leftMove;
    public Sprite rightMove;
    private Animator anim;

    bool hasTile = false;
    float cooldown;

    string xMove = "not moving";
    string yMove = "not moving";

    string direction = "MovingLeft";

	private int speedTimer = 0, attackTimer = 0;
	private bool speedPowerup = false, attackPowerup = false;

    KeyCode Up;
    KeyCode Down;
    KeyCode Left;
    KeyCode Right;
    KeyCode PickUp;
    KeyCode Attack;

    // Use this for initialization
    void Start () {
        GetComponent<SpriteRenderer>().sortingOrder = 1000;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
        anim = GetComponent<Animator>();

        if (player == 'A')
        {
            Up = KeyCode.W;
            Down = KeyCode.S;
            Left = KeyCode.A;
            Right = KeyCode.D;
            PickUp = KeyCode.E;
            Attack = KeyCode.Space;
        }
        else
        {
            Up = KeyCode.UpArrow;
            Down = KeyCode.DownArrow;
            Left = KeyCode.LeftArrow;
            Right = KeyCode.RightArrow;
            PickUp = KeyCode.RightAlt;
            Attack = KeyCode.RightShift;
        }
    }

    // Update is called once per frame
    void Update () {
        // Handle powerups
        if (speedTimer > 0)
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
        transform.GetChild(0).rotation = Quaternion.identity;
        transform.GetChild(0).gameObject.layer = 8;

        //move vertically
        if (yMove == "not moving")
        {
            if (Input.GetKeyDown(Up))
            {
                yMove = "Up";
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
            }
            else if (Input.GetKeyDown(Down))
            {
                yMove = "Down";
                transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
            }
        }

        if (yMove == "Up")
        {
            if (Input.GetKeyUp(Up))
            {
                if (Input.GetKey(Down))
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
            if (Input.GetKeyUp(Down))
            {
                if (Input.GetKey(Up))
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
            if (Input.GetKeyDown(Left))
            {
                xMove = "Left";
                direction = "MovingLeft";
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
            }
            else if (Input.GetKeyDown(Right))
            {
                xMove = "Right";
                direction = "MovingRight";
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
            }
        }

        if (xMove == "Left")
        {
            if (Input.GetKeyUp(Left))
            {
                if (Input.GetKey(Right))
                {
                    xMove = "Right";
                    direction = "MovingRight";
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
            if (Input.GetKeyUp(Right))
            {
                if (Input.GetKey(Left))
                {
                    xMove = "Left";
                    direction = "MovingLeft";
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
        if (xMove == "not moving" && yMove == "not moving")
        {
            anim.SetBool("MovingRight", false);
            anim.SetBool("MovingLeft", false);
            anim.enabled = false;
            if(direction == "MovingLeft")
            {
                GetComponent<SpriteRenderer>().sprite = leftMove;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = rightMove;
            }
        }
        else if (direction == "MovingLeft")
        {
            anim.enabled = true;
            anim.SetBool("MovingRight", false);
            anim.SetBool("MovingLeft", true);
        }
        else if (direction == "MovingRight")
        {
            anim.enabled = true;
            anim.SetBool("MovingRight", true);
            anim.SetBool("MovingLeft", false);
        }

        //attack
        if (Input.GetKey(Attack))
        {
            //play attack animation
            transform.GetChild(0).GetComponent<Animation>().Play();
            transform.GetChild(0).gameObject.layer = 10;
            if (direction == "MovingLeft")
            {
                transform.GetChild(0).Rotate(new Vector3(0, 0, 90));
            }
            else
            {
                transform.GetChild(0).Rotate(new Vector3(0, 0, -90));
            }
        }

        if (Input.GetKey(PickUp)) {

            //pick up item
            if (inRange && tile != null && Time.time > cooldown + 1.0f)
            {
                if (!hasTile)
                {
                    if (tile.tag != "permanent")
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
