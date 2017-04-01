using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * State the player is in during most parts of the game. Contains basic move, jump and shoot functionality.
 * @type State
 */
public class Default : State {

    public const string name = "Default";
    private float direction = 0;
    private bool newJump;
    private bool holdingJump;
	private PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	public override void Update () {


        direction = 0;
		changeState();
        newJump = false;
        holdingJump = false;

        //Get Direction
        if (Input.GetKey("left")) {
            direction = Constant.LEFT;
        }
        if (Input.GetKey("right")) {
            direction = Constant.RIGHT;
        }
        //Get Jumps
        if (Input.GetKeyDown("space")) {
            newJump = true;           
        }
        if (Input.GetKey("space")) {
            holdingJump = true;         
        }
        //In Air
		if(!player.entity.isGrounded() ) {
            Debug.Log(player.entity.rb.velocity.y);
            if(player.entity.rb.velocity.y <= 0) {
            	player.animator.SetInteger("State", 5);
            }
            else {
            	player.animator.SetInteger("State", 2);
            }
        }
        //Running
        else if (direction != 0) {
            player.animator.SetInteger("State", 1);

            

        }
        else {
            if(player.entity.horizontalSpeed == 0) {
            	player.animator.SetInteger("State", 0);
	        }
        }

        player.entity.jump(newJump, holdingJump);
		player.entity.move(direction);
		player.flipSprite();
		player.shoot();
        player.entity.flipSprite();
        
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