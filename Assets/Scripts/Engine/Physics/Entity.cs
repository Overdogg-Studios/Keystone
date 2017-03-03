using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Entity is designed to simulate gravity in the place of Rigidbody.
 * @type {[type]}
 */
public class Entity : MonoBehaviour {

	public bool grounded;
	public float gravity;
	public float velocityX;
	public float velocityY;
	public float terminalVelocity;
	public Collider2D collider;
	ContactBox groundContactBox;
	public LayerMask groundMask;
	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider2D>();
		groundContactBox = transform.Find("Contact Boxes/Ground Contact Box").GetComponent<ContactBox>();
		groundMask = LayerMask.GetMask("Ground");
	}
	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("WOAH");
	}
	// Update is called once per frame
	void FixedUpdate () {
		Debug.Log(isGrounded());
		if(!isGrounded()) {
			
			velocityY = velocityY - (Time.deltaTime * gravity);
			transform.position = new Vector3(transform.position.x + (velocityX), transform.position.y + (velocityY), -1); 
		}
		else {
			velocityY = 0;
			
		}
		//Debug.Log(collider.bounds);
	}
	public bool isGrounded() {
		

		return Physics2D.OverlapBox(groundContactBox.position, new Vector2(groundContactBox.x, groundContactBox.y), 0f, groundMask);
	}

	public void addForceY(float force) {
		velocityY += force;
		
	}
}
