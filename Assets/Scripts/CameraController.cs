using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float yaw = 0.0f;
    public float pitch;
	
	private float xOffset;
	public float yOffset;
	private float zOffset;
	private float dist;
	private bool temp;
    private Vector3 offset;
	public bool thirdPerson = true;
	
	

    void Start()
    {
		GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
        offset = transform.position - player.transform.position;
		dist = offset.magnitude;
		DontDestroyOnLoad(GameObject.Find("Main Camera"));
		xOffset = offset.x;
		yOffset = offset.y; //constant
		zOffset = offset.z;
		pitch = Mathf.Atan(0/40+1.0f/Mathf.Sqrt(3)-0.25f)/Mathf.PI*180.0f;
    }

    void LateUpdate()
    {
		if(thirdPerson)
		{
			Vector3 off = new Vector3(-dist * Mathf.Sin(yaw*Mathf.Deg2Rad) * Mathf.Cos(pitch*Mathf.Deg2Rad), Mathf.Sin(pitch*Mathf.Deg2Rad) * dist, -dist * Mathf.Cos(yaw*Mathf.Deg2Rad) * Mathf.Cos(pitch*Mathf.Deg2Rad));
			transform.position = player.transform.position + off;
		}
		else
		{
			transform.position = player.transform.position;
		}
    }

    void Update () 
    {
		int add;
		if(thirdPerson)
			add = 0;
		else
			add = 20;
		if(Input.GetKeyDown(KeyCode.E))
			thirdPerson = !thirdPerson;
		if(PlayerController.level!=5)
		{
			temp = true;
		}
		RaycastHit hit;
		if(thirdPerson)
		{
		float pit;
		if(Physics.Raycast(player.transform.position,-Vector3.up,out hit))
		{
			pit = Mathf.Atan(hit.distance/40+1.0f/Mathf.Sqrt(3)-0.25f)/Mathf.PI*180.0f;
		}
		else
		{
			pit = 90.0f;
		}
		
		float cameraSpeed = 0.25f;
		if(pitch<pit)
			pitch+=cameraSpeed;
		if(pitch>pit)
			pitch-=cameraSpeed;
		}
        yaw += speedH * Input.GetAxis("Mouse X");
        //pitch -= speedV * Input.GetAxis("Mouse Y");
		if(Input.GetKeyDown(KeyCode.R)||Input.GetKeyDown(KeyCode.C))
			yaw = 0;
		if(temp && PlayerController.level == 5)
		{
			temp = false;
			yaw = 270;
		}
        transform.eulerAngles = new Vector3(pitch - add, yaw, 0.0f);
    }
}