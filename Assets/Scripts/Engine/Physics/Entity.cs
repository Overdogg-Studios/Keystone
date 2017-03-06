using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Entity is designed to simulate gravity in the place of Rigidbody.
 * @type {[type]}
 */
public class Entity : MonoBehaviour {

	public bool grounded;
	public float mass;
	public float gravity;
	public float velocityX;
	public float velocityY;
	public float maxVelocityY;
	public float maxVelocityX;
	public bool isKinematic;
	private BoxCollider2D bottomCollider;
	private BoxCollider2D topCollider;
	private BoxCollider2D rightCollider;
	private BoxCollider2D leftCollider;
	public BoxCollider2D collider;
	public LayerMask groundMask;
	// Use this for initialization
	void Start () {

		collider = GetComponent<BoxCollider2D>();
		bottomCollider = gameObject.AddComponent<BoxCollider2D>();
		topCollider = gameObject.AddComponent<BoxCollider2D>();
		leftCollider = gameObject.AddComponent<BoxCollider2D>();
		rightCollider = gameObject.AddComponent<BoxCollider2D>();
		updateEdgeColliders();
		groundMask = LayerMask.GetMask("Ground");
	}
	// Update is called once per frame
	void FixedUpdate () {

		updateEdgeColliders();
		/*
		if (!isKinematic)
		{
			velocityY = velocityY - (gravity * mass);
		}

		

		if(!isGrounded()) {
			
			velocityY = velocityY - (Time.deltaTime * gravity);
			transform.position = new Vector3(transform.position.x + (velocityX), transform.position.y + (velocityY), -1); 
		}
		else {
			velocityY = 0;
			
		}
		Debug.Log(isGrounded());
		
		capVelocity();
		transform.position = new Vector3(transform.position.x + (velocityX), transform.position.y + (velocityY), -1); 
		*/
	}

	public void addForceY(float force) {
		velocityY += force;
		
	}
	public void capVelocity() {

		if (velocityY > maxVelocityY)
		{
			velocityY = maxVelocityY;
		} else if (velocityY < -maxVelocityY)

		{
			velocityY = -maxVelocityY;
		}
	}
	void OnDrawGizmos () {
		Gizmos.color = new Color(1F, 0F, 0F, 1F);
		//Right Side Collider
		
	}
	public void updateEdgeColliders() {

		Vector2 offset = new Vector2(collider.size.x / 2 + collider.offset.x, collider.offset.y);
		Vector2 size = new Vector2((collider.size.x)/10, (collider.size.y)/1.1f);
		rightCollider.offset = offset;
		rightCollider.size = size;

		offset = new Vector2(-collider.size.x / 2 + collider.offset.x, collider.offset.y);
		size = new Vector2((collider.size.x)/10, (collider.size.y)/1.1f);
		leftCollider.offset = offset;
		leftCollider.size = size;

		offset = new Vector2(collider.offset.x, collider.size.y / 2 + collider.offset.y);
		size = new Vector2((collider.size.x)/1.1f, (collider.size.y)/10f);
		topCollider.offset = offset;
		topCollider.size = size;

		offset = new Vector2(collider.offset.x, -collider.size.y / 2 + collider.offset.y);
		size = new Vector2((collider.size.x)/1.1f, (collider.size.y)/10f);
		bottomCollider.offset = offset;
		bottomCollider.size = size;
	}

	/**
	 * Check to see if the entity is touching the ground, ceiling or a wall on either side.
	 * @return true if touching, false otherwise.
	 */
	public bool isGrounded() {
		return Physics2D.OverlapBox(bottomCollider.bounds.center, bottomCollider.bounds.size, 0f, groundMask);
	}
	public bool isTouchingCeiling() {
		return Physics2D.OverlapBox(topCollider.bounds.center, topCollider.bounds.size, 0f, groundMask);
	}
	public bool isTouchingRightWall() {
		return Physics2D.OverlapBox(rightCollider.bounds.center, rightCollider.bounds.size, 0f, groundMask);
	}
	public bool isTouchingLeftWall() {
		return Physics2D.OverlapBox(leftCollider.bounds.center, leftCollider.bounds.size, 0f, groundMask);
	}
}
