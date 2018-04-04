using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoTransition : MonoBehaviour {

	public AudioSource lmao;
	private int timer;
	
	// Use this for initialization
	void Start () {
		timer  = 80;
	}
	
	// Update is called once per frame
	void Update () {
		if(!lmao.isPlaying)
		{
			if(timer > 0)
			{
				timer--;
			}
			else
			{
				SceneManager.LoadScene("Menu");
			}
		}
	}
}

/* DO WE WANT TO PRESS A BUTTEN OR IS IT OKAY TO JUST CLICK ON PRESS START? QUESTION MARKS */
