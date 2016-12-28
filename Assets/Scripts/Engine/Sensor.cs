using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script that is attached to anything that needs to detect if the player is in a given location.
 */
public class Sensor : MonoBehaviour {

	public bool isActive; //Determines if the sensor is currently looking for a target.
	public bool isEnemy; //If true, triggers when a player enters its collider.
	public bool playerDetected; //Determines if the player is currently within the sensors collider.
	

	void Start() {
		playerDetected = false;
	}
	void OnTriggerEnter2D(Collider2D other) {

		if(isActive) {
			
			if(other.gameObject.name == "Player") {

				playerDetected = true;
			}
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if(isActive) {
			
			if(other.gameObject.name == "Player") {

				playerDetected = false;
			}
		}
	}
}
