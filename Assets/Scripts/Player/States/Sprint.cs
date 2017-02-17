using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : State {

	public const float LEFT = -1.0f;
	public const float RIGHT = 1.0f;

	private PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	// Use this for initializatio
	// Update is called once per frame
	public override void Update () {
		move();
	}
	public void move() {

    	if(Input.GetKey("left") || Input.GetKey("right")) {
    		if(player.animator.GetInteger("State") != 1) {
    			player.animator.SetInteger("State", 4);
    		}
    	}
    	else {
    		player.animator.SetInteger("State", 0);
    	}
    	if(Input.GetKey("left") && (player.speed < (player.maxSpeed * player.currentSprintMultiplier))) {
    		
    		
    		player.direction = LEFT;
    		if(player.speed > 0) {
    			
    			player.speed = player.speed/10;
    		}
    		player.speed = player.speed - player.acceleration * Time.deltaTime * player.currentSprintMultiplier;

    		if(player.isTouchingLeftWall()) {
    			player.speed = 0;
    		}
    	}
    	else if ((Input.GetKey("right")) && (player.speed > (LEFT * player.maxSpeed * player.currentSprintMultiplier))) {
    		player.direction = RIGHT;
    		if(player.speed < 0) {
    			player.speed = player.speed/10;
    		}
    		player.speed = player.speed + player.acceleration * Time.deltaTime * player.currentSprintMultiplier;

    		if(player.isTouchingRightWall()) {
    			player.speed = 0;
    		}

    	}
    	else {
    		
    		if(player.speed > player.deceleration * Time.deltaTime) {
    			player.speed = player.speed - player.deceleration * Time.deltaTime * player.currentSprintMultiplier;
    		}
    		else if(player.speed < -player.deceleration * Time.deltaTime) {
    			player.speed = player.speed + player.deceleration * Time.deltaTime * player.currentSprintMultiplier;
    		}
    		else {
    			player.speed = 0;
    		}
    	}
    	if(player.speed > player.maxSpeed * player.currentSprintMultiplier) {
    		player.speed = player.maxSpeed * player.currentSprintMultiplier;
    	}
    	if(player.speed < LEFT * player.maxSpeed * player.currentSprintMultiplier) {
    		player.speed = LEFT * player.maxSpeed * player.currentSprintMultiplier;
    	}
    	player.transform.position = new Vector3(player.transform.position.x + player.speed * Time.deltaTime, player.transform.position.y, -1); 
    }
}
