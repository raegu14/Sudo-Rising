using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour {

    public GameObject track;
    public float speed;
    //public Vector3 direction;

    // Use this for initialization
    void Start() {
        //direction = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        GetComponent<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(transform.position.y * 100);

        if (track != null)
        {
            transform.position += (track.transform.position - transform.position).normalized * Time.deltaTime * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            //pick up tile
            //stop at tile, pick it up, and then move
            tag = "taken";
            track = col.gameObject;
            speed = track.GetComponent<EnemyMovement>().speed;
            //track = enemy.GetComponent<EnemyMovement>().direction.normalized * Time.deltaTime * enemy.GetComponent<EnemyMovement>().speed;
            track.GetComponent<EnemyMovement>().hasTile = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>().tiles = GameObject.FindGameObjectsWithTag("tile");
        }
/*        else if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerMovement>().inRange = true;
        }*/
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerMovement>().inRange = true;
            col.gameObject.GetComponent<PlayerMovement>().tile = gameObject;

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerMovement>().inRange = false;
        }
    }
}
