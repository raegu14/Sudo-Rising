using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour {

    GameObject cam;

    int counter;

    int finalIteration = 20;
    int curIteration = 20;

    int offset;
    float prevSize;
    float camSize;
    float prevX;
    float camX;
    float prevY;
    float camY;

	// Use this for initialization
	void Awake () {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.GetComponent<Camera>().orthographicSize = 2f;
        cam.transform.position = new Vector3(-5.65f, 13.5f, -10f);
        prevSize = 2f;
        prevX = -5.65f;
        prevY = 13.5f;
        offset = 200;
        camSize = prevSize;
        camX = prevX;
        camY = prevY;
    }

    // Update is called once per frame
    void Update () {
        curIteration++;
        if (curIteration < finalIteration)
        {
            float t = (float)curIteration / (float)finalIteration;
            cam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(prevSize, camSize, t);
            cam.transform.position = new Vector3(Mathf.Lerp(prevX, camX, t), Mathf.Lerp(prevY, camY, t), -10f);
        }
        
        if (curIteration == finalIteration + offset)
        {
            next();
            curIteration = 0;
            prevSize = camSize;
            prevX = camX;
            prevY = camY;
        }

        if (counter >= 7)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("Level");
            }
        }

        
    }

    public void next()
    {
        counter++;
        switch (counter){
            case 1:
                offset = 100;
                curIteration = 0;
                camSize = 2f;
                camX = -5.5f;
                camY = 10.75f;
                break;
            case 2:
                offset = 200;
                curIteration = 0;
                camSize = 2.5f;
                camX = -4.75f;
                camY = 7.5f;
                break;
            case 3:
                offset = 200;
                curIteration = 0;
                camSize = 2f;
                camX = 0f;
                camY = 13.5f;
                break;
            case 4:
                offset = 200;
                curIteration = 0;
                camSize = 2f;
                camX = 5.75f;
                camY = 13.5f;
                break;
            case 5:
                offset = 200;
                curIteration = 0;
                camSize = 2.75f;
                camX = 0.75f;
                camY = 9.3f;
                break;
            case 6:
                offset = 200;
                curIteration = 0;
                camSize = 2.75f;
                camX = 6.25f;
                camY = 9.75f;
                break;
            case 7:
                offset = 3;
                curIteration = 0;
                camSize = 5.4f;
                camX = 0f;
                camY = 0f;
                break;
            default:
                break;
        }
    }
}
