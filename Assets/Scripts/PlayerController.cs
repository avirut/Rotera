using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private float speed=7.0f;
	private float height = 45.0f;
    private Rigidbody rb;
	private Vector3 movement;
	public float time = 1.0f;
	public static float timeScale = 1;
	public static bool respawn = false;
	private bool respawn2 = false;
	private bool isMoving = false;
	public float vel;
	private float vel1;
	public bool speeding;
	private bool inTrigger;
	private string id;
	public Transform goal;
	private Vector3 startingPos;
	
	private string groundType;
	public Transform lastTouched;
	public string sideHit="";
	public static int level;
	public bool numPressed;
	public int key = 0;
	private RaycastHit MyRayHit;
	private Vector3 contactPoint;
	public int type;
	
	private int levelOffset = 1;
	private bool ready = false;
	public float flag;
	public float flag2;
	public float sizex;
	public float sizez;
	public float x;
	public float z;
	public int tempS;
	public int keyPressed;
	private Vector3 jumpVect;
	public float launchPower = 80.0f;
	public bool launched = false;
	private JumpPad touched;
	public GameObject camera;
	
	
    private void Start()
    {
		camera = GameObject.Find("Main Camera");
		camera.GetComponent<Camera>().enabled = true;
        rb = GetComponent<Rigidbody>();
		vel1 = -1.0f;
		DontDestroyOnLoad(GameObject.Find("Player"));
		DontDestroyOnLoad(GameObject.Find("Goal"));
		//DontDestroyOnLoad(GameObject.Find("Ground"));
		//DontDestroyOnLoad(GameObject.Find("Directional Light"));
		DontDestroyOnLoad(GameObject.Find("Point light"));
		goal.position = new Vector3(150.0f, -10.0099f, 150.0f);
		startingPos = new Vector3(47.87f, 4.4f, 20.0f);
		level = 0;
		Cursor.visible = false;
		numPressed = false;
		rb.position = startingPos;
		Debug.Log(rb.position);
		rb.velocity =  new Vector3(0.0f, 0.0f, 0.0f);
		wait();
    }
    public bool IsGrounded;

	void OnCollisionEnter(Collision collisionInfo)
	{
		IsGrounded = true;
		collision(collisionInfo);
	}
    void OnCollisionStay(Collision collisionInfo)
    {
        collision(collisionInfo);
    }
IEnumerator waitSeconds(int i)
   {
       yield return new WaitForSeconds(i);
   }
	public void collision(Collision collisionInfo)
	{
		IsGrounded = true;
		Color red = new Color(1f,0f,0f,1f);
		if(collisionInfo.gameObject.tag=="Enemy")
		{
			respawn = true;
			respawn2 = true;
			DespawnBall.despawnBalls();
			rb.position = startingPos;
			rb.velocity =  new Vector3(0.0f, 0.0f, 0.0f);
			waitSeconds(1);
			//load(level);
		}
		
		if(collisionInfo.gameObject.tag == "Jumper")
		{
			touched = (JumpPad)collisionInfo.gameObject.GetComponent(typeof(JumpPad));
			launchPower = touched.height;
			launch();
		}
		
		groundType = collisionInfo.gameObject.tag;
		lastTouched = collisionInfo.gameObject.GetComponent<Transform>();
		
		Vector3 collisionPoint = collisionInfo.contacts[0].point;
		Vector3 myPos = transform.position;
		jumpVect = myPos - collisionPoint;
	}
	
	private void wait()
	{
		//load(0);
		ready = true;
	}
    void OnCollisionExit(Collision collisionInfo)
    {
        //IsGrounded = false;
		launched = false;
    }
	
	private void launch()
	{
		rb.AddForce(lastTouched.up*launchPower*speed);
		launched = true;
	}
	
	private void Update()
	{
		/*if(Input.GetKey("1")||Input.GetKey("2")||Input.GetKey("3")||Input.GetKey("4")||Input.GetKey("5")||Input.GetKey("6")||Input.GetKey("7")||Input.GetKey("8")||Input.GetKey("9"))
		{
			numPressed = true;
		}
		else
		{
			numPressed = false;
		}
		 for(int i = 1; i<=9&&numPressed; i++)
		 {
			 if(Input.GetKey(""+i))
				 key = i;
		 }
		 if(numPressed)
		 {
			 load(key);
			 keyPressed = key;
		 }*/
		 if(Input.GetKeyDown("r"))
		{
			respawn = true;
			respawn2 = true;
			DespawnBall.despawnBalls();
			transform.position = startingPos;
			rb.velocity = new Vector3(0.0f,0.0f,0.0f);
		}
		if((Input.GetKeyDown("space")&&IsGrounded)||Input.GetKey("w")||Input.GetKey("a")||Input.GetKey("s")||Input.GetKey("d"))
		{
			
			if(timeScale <= 0.95f&&speeding)
				timeScale += 0.05f;
			isMoving = true;
		}
		else
		{
			isMoving = false;
		}
		
	}
	
    private void FixedUpdate()
    {
		if(ready)
		{
		GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
		speeding = (rb.velocity.magnitude>vel1||rb.velocity.magnitude==0.0f);
		vel1 = rb.velocity.magnitude;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical );
		movement = Camera.main.transform.TransformDirection(movement);
        rb.AddForce(movement * speed * timeScale);
        if (Input.GetKeyDown("space")&&IsGrounded)
        {
			IsGrounded = false;
			Vector3 up = new Vector3(0.0f, 1.0f, 0.0f) * speed * height;
			float tempTime = Time.timeScale;
			Time.timeScale = 1.0f;
			rb.AddForce(((jumpVect.normalized*speed*height + up * 1.4f) * 1.0f/1.8f)/Time.timeScale);
			Time.timeScale = tempTime;
        }
		if(!isMoving)
		{
			if(rb.velocity.magnitude <= 3.5f)
			{
				timeScale = rb.velocity.magnitude / 3.5f;
			}
			else
			{
				timeScale = 1.0f;
			}
			if(timeScale <= 0.05f)
			{
				timeScale = 0.04f;
			}
		}
		
		if((!speeding)&&rb.velocity.magnitude<1.8f)
			rb.velocity = new Vector3(0.0f,0.0f,0.0f);
		if((Input.GetKeyDown("space")&&IsGrounded)||Input.GetKey("w")||Input.GetKey("a")||Input.GetKey("s")||Input.GetKey("d")&&speeding)
		{
			timeScale = 1.0f;
		}
		Time.timeScale = timeScale+0.001f;
		Time.fixedDeltaTime = timeScale * 0.02f;
		if(!isMoving)
			timeScale = 1.0f;
		time = Time.timeScale;
		vel = rb.velocity.magnitude;
		}
		if(respawn2)
		{
			respawn2 = false;
		}
		if(respawn&&!respawn2)
		{
			respawn = false;
		}
    }
	
	/*void OnTriggerEnter(Collider other)
	{
		
		id = other.tag;
		inTrigger = true;
		if(other.tag == "Finish")
		{
			if(level == 1)
			{
				load(2);
			}
			else if(level == 2)
			{
				load(3);
			}
			else if(level == 3)
			{
				load(4);
			}
			else if(level == 4)
			{
				load(5);
			}
			else if(level == 5)
			{
				load(6);
			}
			else if(level == 6)
			{
				load(7);
			}
			else if(level == 7)
			{
				load(8);
			}
			else if(level == 8)
			{
				load(1);
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		inTrigger = false;
	}
*/
	public void startLoad()
	{
		
	}
	public void endLoad()
	{
		
	}
	/*
	public void load(int i)
	{
		bool succ = false;
		int rot = 0;
		switch(i)
		{
			case 8:
				goal.position = new Vector3(8.75f, -0.0099f, 5.82f);
				startingPos = new Vector3(0.0f, 5.5f, 0.0f);
				rot = 0;
				level = 8;
				succ = true;
				break;
			case 2:
				startingPos = new Vector3(0.0f, 0.0f, 0.0f);
				goal.position = new Vector3(1.82f, -0.0099f, 6.75f);
				level = 2;
				rot = 0;
				succ = true;
				break;
			case 3:
				goal.position = new Vector3(0.0f, -0.0099f, 4.5f);
				startingPos = new Vector3(0.0f, 0.5f, -10.0f);
				level = 3;
				rot = 0;
				succ = true;
				break;
			case 4:
				goal.position = new Vector3(0.0f, -0.0099f, 0.0f);
				startingPos = new Vector3(0.0f, 0.5f, -10.0f);
				level = 4;
				rot = 0;
				succ = true;
				break;
			case 5:
				goal.position = new Vector3(-10.0f, -0.0099f, 0.0f);
				startingPos = new Vector3(0.0f, 0.5f, 0.0f);
				level = 5;
				rot = 0;
				succ = true;
				break;
			case 6:
				goal.position = new Vector3(0.0f, -0.0099f, 4.5f);
				startingPos = new Vector3(0.0f, 0.5f, 0.0f);
				level = 6;
				rot = 0;
				succ = true;
				break;
			case 7:
				goal.position = new Vector3(0.0f, -0.0099f, 4.5f);
				startingPos = new Vector3(250.0f, 0.5f, 250.0f);
				level = 7;
				rot = 0;
				succ = true;
				break;
			case 0:
				goal.position = new Vector3(150.0f, -10.0099f, 150.0f);
				startingPos = new Vector3(47.87f, 4.5f, 20.0f);
				level = 0;
				rot = 0;
				succ = true;
				break;
			default:
				break;
		}
		if(succ)
		{
			Debug.Log(level+"");
			camera.GetComponent<Camera>().enabled = false;
			startLoad();
			SceneManager.LoadScene(level + levelOffset);
			rb.position = startingPos;
			rb.velocity =  new Vector3(0.0f, 0.0f, 0.0f);
			endLoad();
			camera.GetComponent<Camera>().enabled = true;
		}
		
	}*/
}