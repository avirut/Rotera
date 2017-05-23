using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	public void start()
	{
		GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
		SceneManager.LoadScene(1);
		GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
	}
	// Update is called once per frame
	void Update () {
		
	}
}