using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

	public int damage;
	private PlayerController  allen;
	

	void Start() {
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		allen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		Debug.Log("WE DID DAMAGE");
		if(other.gameObject.tag == "Player") {

			allen.takeDamage(damage);
		}
	}
	
}
