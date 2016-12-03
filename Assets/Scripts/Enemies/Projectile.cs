using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public int travelTime;
	public float time_passed = 0f;
	public int xDirection;
	public int yDirection;
	public float xVelocity;
	public float yVelocity;
	
	Rigidbody2D rb2D; 

	void Start() {
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>();
		rb2D = GetComponent<Rigidbody2D>();
	}
	void FixedUpdate() {

		/* NOT CURRENTLY WORKING.
		playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();


		if(playerCollider.bounds.Intersects(GetComponent<CircleCollider2D>().bounds)) {
			player.takeDamage(damage);
		}
		*/

		time_passed += Time.deltaTime;
		Vector2 moveDir = new Vector2(xDirection * xVelocity, yDirection * yVelocity);
		rb2D.velocity = moveDir;
		

		if(time_passed >= travelTime) {
			Destroy(this.gameObject);
		}
	}
	/**
	 * 
	 */
	public void make(int travelTime, int xDirection, int yDirection, float xVelocity, float yVelocity) {
		this.travelTime = travelTime;
		this.xDirection = xDirection;
		this.yDirection = yDirection;
		this.xVelocity = xVelocity;
		this.yVelocity = yVelocity;
	}
}
