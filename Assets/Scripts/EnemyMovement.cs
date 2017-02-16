using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;
    public Vector3 direction;

    public GameObject[] tiles;
    GameObject closestTile;

    public bool hasTile;
	public GameObject heldTile;
	
	EnemySpawn enemySpawn;

    bool death = false;

	// Use this for initialization
	void Start ()
    {
		enemySpawn = GameObject.Find("EnemySpawnPoints").GetComponent<EnemySpawn>();
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
        if (hasTile)
        {
            //go offscreen
        }
        else if (!death)
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

    void OnCollisionEnter2D(Collision2D col)
    {
        print(col.gameObject.tag);
        if (col.collider.gameObject.tag == "weapon")
        {
            //tile stops moving
			if(heldTile != null)
			{
				heldTile.GetComponent<TileMovement>().track = null;
				heldTile.tag = "tile";
				heldTile = null;
			}
            //enemy dies after 1 second, but loses rigidbody
            Destroy(GetComponent<Rigidbody2D>());
            death = true;

			//reduce enemy count
			enemySpawn.reduceEnemyCount(1);

            direction = new Vector3();
            //play death animation
            StartCoroutine(deathAnim());
        }
    }
	

    IEnumerator deathAnim()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}