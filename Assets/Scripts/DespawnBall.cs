using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Collections.Generic.List<T>;

public class DespawnBall : MonoBehaviour {

	// Use this for initialization
	int hits = 0;
	float dist;
	Rigidbody player;
	Rigidbody rb;
	void Start () {
		player = GameObject.Find("Player").GetComponent<Rigidbody>();
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(hits>1)
		{
			Destroy(gameObject);
		}
		if(Vector3.Distance(rb.position,player.position)>220.0)
		{
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.gameObject.tag!="Enemy")
			hits++;
	}
	
	public static void despawnBalls()
	{
		for(int i = 0; i < CannonShot.balls.Count; i++)
			{
				try
				{
					Destroy(CannonShot.balls[i]);
				}
				catch(System.Exception){}
			}
			CannonShot.balls.Clear();
	}
}
