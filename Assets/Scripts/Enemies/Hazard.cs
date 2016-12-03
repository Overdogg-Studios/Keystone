using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

	public int damage;
	private PlayerController2D  allen;
	

	void Start() {
		allen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>();
	}
	void OnTriggerEnter2D(Collider2D other) {

		Debug.Log("WE DID DAMAGE");
		if(other.gameObject.tag == "Player") {

			allen.takeDamage(damage);
		}
	}
	
}
