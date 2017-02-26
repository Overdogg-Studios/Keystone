using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPool : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;
	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void takeDamage(int dmg) {
		currentHealth -= dmg;

		if(currentHealth < 0) {
			currentHealth = 0;
		}
		
	}

	public bool isDead() {
		if(currentHealth == 0) {
			return true;
		}
		return false;
	}
}
