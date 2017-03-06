using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * State the player is in during most parts of the game. Contains basic move, jump and shoot functionality.
 * @type State
 */
public class Default : State {

    public const string name = "Default";

	private PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	public override void Update () {

		changeState();

		if(!player.entity.isGrounded() ) {
            
            if(player.rb2D.velocity.y < 0) {
            	player.animator.SetInteger("State", 5);
            }
            else {
            	player.animator.SetInteger("State", 2);
            }
        }
        else if (Input.GetKey("left") || Input.GetKey("right")) {
            if(Input.GetKey("left shift")) {
                player.animator.SetInteger("State", 1);
                player.currentSprintMultiplier = player.sprintMultiplier;
            }
            else {
                player.animator.SetInteger("State", 4);
                player.currentSprintMultiplier = 1;
            }
        }
        else {
            if(player.horizontalSpeed == 0) {
            	player.animator.SetInteger("State", 0);
	        }
	        else {
	            player.animator.SetInteger("State", 4);
	        }
        }

		player.move();
		player.jump();
		player.flipSprite();
		player.shoot();
        
	}
    public override string ToString() {
        return name;
    } 
    public override void changeState() {

    	if(player.healthPool.isDead()) {
    		player.currentState = new Frozen();
    	}
    }
}