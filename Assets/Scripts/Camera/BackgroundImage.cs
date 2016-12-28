using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImage : MonoBehaviour {

	//public bool verticalMovement; // Whether or not the image moves vertically
	//public bool horizontalMovement; //Whether or not the image moves horizontally
	private float currentXPosition;
	private float currentYPosition;

	private float initialXPosition;
	private float initialYPosition;

	private new CameraController camera; //Accessor for main camera;
	// Use this for initialization
	void Start () {
		initialXPosition = transform.position.x;
		initialYPosition = transform.position.y;
		camera = GameObject.Find("Camera").GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Vector3 newPosition = new Vector3(initialXPosition + (camera.pos.x / transform.position.z), initialYPosition + (camera.pos.y / transform.position.z) , transform.position.z);
       	transform.position = newPosition;
	}
}
  