using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGame : MonoBehaviour {

    public GameObject playerA;
    int finalIterationA = 200;
    int curIterationA = 0;

    public GameObject playerB;
    int finalIterationB = 200;
    int curIterationB = 0;

    float tA;
    float tB;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        //move playerA
        if(curIterationA == 0)
        {
            playerA.GetComponent<PlayerMovement>().enabled = false;
            playerA.GetComponent<Animator>().SetBool("Move", true);
        }
        if (curIterationA < finalIterationA)
        {
            curIterationA++;
            tA = (float)curIterationA / (float)finalIterationA;
            playerA.transform.position = new Vector3(Mathf.Lerp(-12f, -3.0f, tA), Mathf.Lerp(2f, 2f, tA), 0f);
        }
        if (curIterationA == finalIterationA)
        {
            curIterationA++;
            playerA.GetComponent<PlayerMovement>().enabled = true;
            playerA.GetComponent<Animator>().SetBool("Move", false);
        }

        //move playerB
        if (curIterationB == 0)
        {
            playerB.GetComponent<PlayerMovement>().enabled = false;
            playerB.GetComponent<Animator>().SetBool("Move", true);
        }
        if (curIterationB < finalIterationB)
        {
            curIterationB++;
            tB = (float)curIterationB / (float)finalIterationB;
            playerB.transform.position = new Vector3(Mathf.Lerp(-6f, 3.0f, tB), Mathf.Lerp(2f, 2f, tB), 0f);
        }
        if (curIterationB == finalIterationB)
        {
            curIterationB++;
            playerB.GetComponent<Animator>().SetBool("Direction", false);
            playerB.GetComponent<Animator>().SetBool("Move", false);
            playerB.GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
