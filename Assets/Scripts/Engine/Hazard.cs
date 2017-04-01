using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

	public int damage;
	public bool isEnemy; //Determines if the projectile will hurt the player, or hurt enemies.
	public bool isActive; //Determines if the projectile will effect anything at all. Non active projectiles do not deal damage to the player or enemies. 
	private PlayerController  player;
	private HealthPool enemy;
	
	void Start() {
		
	}
	void OnTriggerEnter2D(Collider2D other) {

		if(isActive) {
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
			enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<HealthPool>();
			if(other.gameObject.tag == "Player" && isEnemy) {

				player.GetComponent<HealthPool>().takeDamage(damage);
			}
			if(other.gameObject.tag == "Enemy" && !isEnemy) {
				enemy.takeDamage(damage);
				Destroy(this.gameObject);
			}
		}
		

	}
	
}
