using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour {

	// Use this for initialization
	public float dist = 100;
	private float start;
	private bool way = true;
	void Start () {
		start = dist;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(dist < 0 || dist > start)
		{
			way = !way;
			if(way)
				dist-=2.0f;
			else
				dist+=2.0f;
		}
		if(way)
		{
			dist = dist - Time.timeScale;
		}
		else
		{
			dist = dist + Time.timeScale;
		}
		Vector3 temp = new Vector3(0.0f, 1.1f, 5*dist/(1.0f*start));
		transform.position = temp;
	}
}