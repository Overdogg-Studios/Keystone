using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float travelTime;
	public float timePassed;
	public float xDirection;
	public float yDirection;
	public float xVelocity;
	public float yVelocity;
	
	Rigidbody2D rb2D; 

	void Start() {
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>();
		rb2D = GetComponent<Rigidbody2D>();
		timePassed = 0f;
	}
	void FixedUpdate() {

		/* NOT CURRENTLY WORKING.
		playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();


		if(playerCollider.bounds.Intersects(GetComponent<CircleCollider2D>().bounds)) {
			player.takeDamage(damage);
		}
		*/

		timePassed += Time.deltaTime;
		Vector2 moveDir = new Vector2(xDirection * xVelocity, yDirection * yVelocity);
		rb2D.velocity = moveDir;
		

		if(timePassed >= travelTime) {
			Destroy(this.gameObject);
		}
	}
	/**
	 * 
	 */
	public void make(float travelTime, float xDirection, float yDirection, float xVelocity, float yVelocity) {
		this.travelTime = travelTime;
		this.xDirection = xDirection;
		this.yDirection = yDirection;
		this.xVelocity = xVelocity;
		this.yVelocity = yVelocity;
	}
}
