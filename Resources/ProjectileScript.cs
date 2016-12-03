using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	public int damage;
	
	public int travel_time;
	public float time_passed = 0f;
	public int xDirection;
	public int yDirection;
	public float xVelocity;
	public float yVelocity;
	private PlayerController2D  allen;
	
	Rigidbody2D rb2D; 

	void Start() {
		allen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>();
		rb2D = GetComponent<Rigidbody2D>();
	}
	void OnTriggerEnter2D(Collider2D other) {

		Debug.Log("WE DID DAMAGE");
		if(other.gameObject.tag == "Player") {

			allen.takeDamage(damage);
		}
	}
	void Update() {
		time_passed += Time.deltaTime;
		Vector2 moveDir = new Vector2(xDirection * xVelocity, yDirection * yVelocity);
		rb2D.velocity = moveDir;
		

		if(time_passed >= travel_time) {
			Destroy(this.gameObject);
		}
	}
}
