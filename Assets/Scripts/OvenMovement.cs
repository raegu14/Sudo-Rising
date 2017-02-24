using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenMovement : MonoBehaviour
{

    public float speed;
    public Vector3 direction;

    public GameObject[] tiles;
    GameObject closestTile;

    public bool hasTile;
    public GameObject heldTile;

    bool calledEat = false;

    EnemySpawn enemySpawn;

    bool death = false;
    Animator anim;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        enemySpawn = GameObject.Find("EnemySpawnPoints").GetComponent<EnemySpawn>();
        //find closest tile and set direction to it
        tiles = GameObject.FindGameObjectsWithTag("set");
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
    void Update()
    {
        if (hasTile)
        {
            if (!calledEat)
            {
                anim.SetBool("hasTile", true);
                StartCoroutine(waitToDestroy());
            }
            direction = new Vector3();
            calledEat = true;
        }
        else if (!death)
        {
            direction = new Vector3(-1, -1, 0).normalized;
            //find closest tile and set direction to it
            tiles = GameObject.FindGameObjectsWithTag("set");
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
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
        else
        {
            anim.SetBool("Died", true);
        }
        if (direction.x < 0)
        {
            anim.SetBool("Left", true);
        }
        else
        {
            anim.SetBool("Left", false);
        }
    }


    public void die()
    {
        //tile stops moving
        if (heldTile != null)
        {
            heldTile.GetComponent<TileMovement>().track = null;
            heldTile.tag = "tile";
            heldTile.transform.position = gameObject.transform.position;
            heldTile = null;
        }
        //enemy dies after 1 second, but loses rigidbody
        Destroy(GetComponent<Rigidbody2D>());
        death = true;

        //reduce enemy count
        enemySpawn.reduceEnemyCount(1);

        //direction = new Vector3();
        //play death animation
        StartCoroutine(deathAnim());
    }


    IEnumerator deathAnim()
    {
        GetComponent<AudioSource>().Play();
        anim.SetBool("Died", true);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(5f);
        anim.SetBool("hasTile", false);
        Destroy(heldTile);
        hasTile = false;
        calledEat = false;
    }
}