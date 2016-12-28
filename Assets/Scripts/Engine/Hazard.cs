using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

	public int damage;
	public bool isEnemy; //Determines if the projectile will hurt the player, or hurt enemies.
	public bool isActive; //Determines if the projectile will effect anything at all. Non active projectiles do not deal damage to the player or enemies. 
	private PlayerController  allen;
	

	void Start() {
		
	}
	void OnTriggerEnter2D(Collider2D other) {

		if(isActive) {
			allen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
			if(other.gameObject.tag == "Player" && isEnemy) {

				allen.GetComponent<HealthPool>().takeDamage(damage);
			}
		}
		

	}
	
}
