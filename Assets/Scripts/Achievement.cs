using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour {

	// Use this for initialization
	public PlayerController player;
	
	AudioSource a;
	void Start () 
	{
		a = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	public void start()
	{
		a.Play();
		//player = GameObject.Find("Player");
		player.resetLevels();
	}
}
