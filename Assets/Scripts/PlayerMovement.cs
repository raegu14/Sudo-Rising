using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 2.0f;
    public char player = 'A';
	public int health = 200;

    public bool inRange = false; //in range to pick up tile
    public GameObject tile;
	public GameObject boardSpace;

    public GameObject enemySpawn;

    private Animator anim;

    public bool hasTile = false;
    float cooldown;

    float atkTimer;

	private TileSpawn spawnner;

    private string direction;
    string xMove = "not moving";
    string yMove = "not moving";

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
		boardSpace = null;
		spawnner = GameObject.Find("TileSpawnPoints").GetComponent<TileSpawn>();
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
            direction = "Right";
        }
        else
        {
            Up = KeyCode.I;
            Down = KeyCode.K;
            Left = KeyCode.J;
            Right = KeyCode.L;
            PickUp = KeyCode.U;
            Attack = KeyCode.M;
            direction = "Right";
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
		else
		{
			speedPowerup = false;
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
                direction = "Left";
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
            }
            else if (Input.GetKeyDown(Right))
            {
                xMove = "Right";
                direction = "Right";
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
                    direction = "Right";
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
                    direction = "Left";
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
       
        if (Input.GetKeyDown(PickUp))
        {
            //pick up item
            if (tile != null && !hasTile && inRange)
            {
                if (tile.tag != "permanent")
                {
					TileMovement t = tile.GetComponent<TileMovement>();
                    hasTile = true;
                    tile.GetComponent<SpriteRenderer>().sprite = spawnner.GetSprite("lit", t.value);
                    t.track = gameObject;
					t.UpdateSpawn(t.spawn);
                }
            }

            else if (hasTile)
            {
				TileMovement t = tile.GetComponent<TileMovement>();
                hasTile = false;
                tile.GetComponent<SpriteRenderer>().sprite = spawnner.GetSprite("norm", t.value);
                t.track = null;
                if (boardSpace == null)
                {
                    tile.transform.position = transform.position;
                }
                else
                {
                    bool set = boardSpace.GetComponent<BoardTile>().OccupySpace(tile, true);
                    enemySpawn.GetComponent<EnemySpawn>().pregame = enemySpawn.GetComponent<EnemySpawn>().pregame & !set;
                }
                tile = null;
            }
        }

        if(atkTimer < Time.time)
        {
            //reset weapons
            transform.GetChild(0).rotation = Quaternion.identity;
            transform.GetChild(0).gameObject.layer = 12;
        }

        if (Input.GetKeyDown(Attack) && !hasTile && atkTimer < Time.time)
        {
            //play attack animation
            atkTimer = Time.time + 0.5f;
            transform.GetChild(0).gameObject.layer = 10;
            anim.SetTrigger("Attack");
            if (direction == "Left")
            {
                transform.GetChild(0).Rotate(new Vector3(0, 0, 90));
            }
            else
            {
                transform.GetChild(0).Rotate(new Vector3(0, 0, -90));
            }
        }
        else
        {
            if (xMove == "not moving" && yMove == "not moving")
            {
                anim.SetBool("Move", false);
            }
            else if (direction == "Left")
            {
                anim.SetBool("Move", true);
                anim.SetBool("Direction", false);
            }
            else if (direction == "Right")
            {
                anim.SetBool("Move", true);
                anim.SetBool("Direction", true);
            }
        }
    }
	
	public void Powerup(string pType, float multiplier)
	{
		if(pType == "speed")
		{
			speed *= multiplier;
			speedTimer = 100;
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
        }
    }

    private IEnumerator paused()
    {
        yield return new WaitForSeconds(1f);
    }
	
	public IEnumerator Knockback(Vector3 exPos)
	{
		TakeDamage(20);
		Vector2 newPos = gameObject.transform.position - exPos;
		Vector2 one = new Vector2(0, 0.5f);
		// TODO play anim
		Rigidbody2D r = gameObject.GetComponent<Rigidbody2D>();
		Vector2 force = newPos - one;
		force.Normalize();
		r.AddForce(25*force, ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.25f);
		r.velocity = Vector3.zero;
	}
	
	public void TakeDamage(int damage)
	{
		health = health - damage;
		// TODO set health
		if(health <= 0)
		{
			GameObject.Find("MainCamera").GetComponent<Main>().setGameStatus("GameOver");
		}
	}
}
