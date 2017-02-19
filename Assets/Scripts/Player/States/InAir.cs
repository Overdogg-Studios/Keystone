using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAir : State {

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
    	if(Input.GetKey("left") && (player.baseMoveSpeed < (player.maxSpeed))) {
    		
    		
    		player.direction = LEFT;
    		if(player.baseMoveSpeed > 0) {
    			
    			player.baseMoveSpeed = player.baseMoveSpeed/10;
    		}
    		player.baseMoveSpeed = player.baseMoveSpeed - player.acceleration * Time.deltaTime;

    		if(player.isTouchingLeftWall()) {
    			player.baseMoveSpeed = 0;
    		}
    	}
    	else if ((Input.GetKey("right")) && (player.baseMoveSpeed > (LEFT * player.maxSpeed))) {
    		player.direction = RIGHT;
    		if(player.baseMoveSpeed < 0) {
    			player.baseMoveSpeed = player.baseMoveSpeed/10;
    		}
    		player.baseMoveSpeed = player.baseMoveSpeed + player.acceleration * Time.deltaTime;

    		if(player.isTouchingRightWall()) {
    			player.baseMoveSpeed = 0;
    		}

    	}
    	else {
    		
    		if(player.baseMoveSpeed > player.deceleration * Time.deltaTime) {
    			player.baseMoveSpeed = player.baseMoveSpeed - player.deceleration * Time.deltaTime;
    		}
    		else if(player.baseMoveSpeed < -player.deceleration * Time.deltaTime) {
    			player.baseMoveSpeed = player.baseMoveSpeed + player.deceleration * Time.deltaTime;
    		}
    		else {
    			player.baseMoveSpeed = 0;
    		}
    	}
    	if(player.baseMoveSpeed > player.maxSpeed) {
    		player.baseMoveSpeed = player.maxSpeed;
    	}
    	if(player.baseMoveSpeed < LEFT * player.maxSpeed ) {
    		player.baseMoveSpeed = LEFT * player.maxSpeed;
    	}
    	player.transform.position = new Vector3(player.transform.position.x + player.baseMoveSpeed * Time.deltaTime, player.transform.position.y, -1); 
    }
}