using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	// Use this for initialization
	double time1 = 0.0f;
	int time2 = 5;
	bool gotten = false;
	bool gotten2 = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gotten2 && Time.time > time1)
		{
			GetComponent<Achievement>().start();
			gotten2 = true;
		}
	}
	void OnCollisionEnter(Collision collisionInfo)
	{
		if(!gotten && collisionInfo.gameObject == GameObject.Find("Player"))
		{
			time1 = time2 + Time.time;
			gotten = true;
		}
		
	}
}
