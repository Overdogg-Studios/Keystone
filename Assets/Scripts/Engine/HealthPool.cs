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

    /**
     * Makes the current entity take damage
     * @param dmg The amount of damage to take
     */
	public void takeDamage(int dmg) {
		currentHealth -= dmg;
	}
}
