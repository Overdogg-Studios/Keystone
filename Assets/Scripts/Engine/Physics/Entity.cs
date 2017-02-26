using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Entity is designed to simulate gravity in the place of Rigidbody.
 * @type {[type]}
 */
public class Entity : MonoBehaviour {

	public float gravity;
	public float mass;
	public float terminalVelocity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, transform.position.y - gravity, -1); 
	}
}
