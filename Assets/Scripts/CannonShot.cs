using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class CannonShot : MonoBehaviour
{
	public GameObject proj;
    private Rigidbody projectile;
    public float speed;
	public GameObject player;
	public float fireType;
	private float lastTime;
	public float despawnTimeSeconds;
	public static List<GameObject> balls;
	public GameObject original;
    // Update is called once per frame
	void Start()
	{
		player = GameObject.Find("Player");
		projectile = proj.GetComponent<Rigidbody>();
		if(projectile==null)
		{
			projectile = proj.AddComponent<Rigidbody>();
		}
		balls = new List<GameObject>();
	}
	
    void Update ()
    {
		if(balls.Contains(original))
			balls.Remove(original);
		checkForDestruction();
        if (fireType==-1.0f&&Input.GetButtonDown("Fire1"))
        {
			shoot();
        }
		else if(fireType<=0.0f)
		{
			shoot();
		}
		else if(fireType<=Time.time-lastTime)
		{
			shoot();
		}
    }
	
	public void shoot()
	{
		GameObject body = transform.GetChild(0).gameObject;
		Transform btransform = body.GetComponent<Transform>();
        GameObject instantiatedProjectileGO = (GameObject)Instantiate(proj,btransform.position + btransform.rotation*new Vector3(1.4f , 0.0f, 1.0f),Quaternion.Euler(0,0,0));
		instantiatedProjectileGO.AddComponent<DespawnBall>();
		balls.Add(instantiatedProjectileGO);
		Rigidbody instantiatedProjectile = instantiatedProjectileGO.GetComponent<Rigidbody>();
		Vector3 shotToPlayer = player.transform.position - instantiatedProjectile.transform.position;
        //instantiatedProjectile.velocity = btransform.rotation * new Vector3(speed,0,0);
		instantiatedProjectile.velocity = shotToPlayer * 0.8f;
		lastTime = Time.time;
	}
	
	private void checkForDestruction()
	{
		if (projectile.transform.position.y > 30)
		{
			Destroy(proj);
		}
	}	
	
	void OnCollisionEnter(Collision collisionInfo)
	{
		Destroy(proj);
	}
}