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
    	if(Input.GetKey("left") && (player.baseMoveSpeed < (player.maxSpeed * player.sprintMultiplier))) {
    		
    		
    		player.direction = LEFT;
    		if(player.baseMoveSpeed > 0) {
    			
    			player.baseMoveSpeed = player.baseMoveSpeed/10;
    		}
    		player.baseMoveSpeed = player.baseMoveSpeed - player.acceleration * Time.deltaTime * player.sprintMultiplier;

    		if(player.isTouchingLeftWall()) {
    			player.baseMoveSpeed = 0;
    		}
    	}
    	else if ((Input.GetKey("right")) && (player.baseMoveSpeed > (LEFT * player.maxSpeed * player.sprintMultiplier))) {
    		player.direction = RIGHT;
    		if(player.baseMoveSpeed < 0) {
    			player.baseMoveSpeed = player.baseMoveSpeed/10;
    		}
    		player.baseMoveSpeed = player.baseMoveSpeed + player.acceleration * Time.deltaTime * player.sprintMultiplier;

    		if(player.isTouchingRightWall()) {
    			player.baseMoveSpeed = 0;
    		}

    	}
    	else {
    		
    		if(player.baseMoveSpeed > player.deceleration * Time.deltaTime) {
    			player.baseMoveSpeed = player.baseMoveSpeed - player.deceleration * Time.deltaTime * player.sprintMultiplier;
    		}
    		else if(player.baseMoveSpeed < -player.deceleration * Time.deltaTime) {
    			player.baseMoveSpeed = player.baseMoveSpeed + player.deceleration * Time.deltaTime * player.sprintMultiplier;
    		}
    		else {
    			player.baseMoveSpeed = 0;
    		}
    	}
    	if(player.baseMoveSpeed > player.maxSpeed * player.sprintMultiplier) {
    		player.baseMoveSpeed = player.maxSpeed * player.sprintMultiplier;
    	}
    	if(player.baseMoveSpeed < LEFT * player.maxSpeed * player.sprintMultiplier) {
    		player.baseMoveSpeed = LEFT * player.maxSpeed * player.sprintMultiplier;
    	}
    	player.transform.position = new Vector3(player.transform.position.x + player.baseMoveSpeed * Time.deltaTime, player.transform.position.y, -1); 
    }
    public void jump() {
        if(Input.GetKeyDown(KeyCode.E) )
        {
            //animator.SetInteger("State", 2);
            if(player.isGrounded())
            {   
                player.rb2D.velocity = new Vector2 (player.rb2D.velocity.x, player.jumpForce);
                player.stoppedJumping = false;
            }
            else if(player.canDoubleJump) {
                
                player.rb2D.velocity = new Vector2 (player.rb2D.velocity.x, 0);
                player.rb2D.velocity = new Vector2 (player.rb2D.velocity.x, player.jumpForce);
                player.stoppedDoubleJumping = false;
                player.canDoubleJump = false;

            }
            else {

            }
        }
        //if you keep holding down the jump button...
        if((Input.GetKey(KeyCode.E)) && !player.stoppedJumping)
        {
            //and your counter hasn't reached zero...
            if(player.jumpTimeCounter > 0)
            {
                //keep jumping!
                player.rb2D.velocity = new Vector2 (player.rb2D.velocity.x, player.jumpForce);
            }
        }
        if((Input.GetKey(KeyCode.E)) && !player.stoppedDoubleJumping)
        {
            //and your counter hasn't reached zero...
            if(player.doubleJumpTimeCounter > 0)
            {
                player.rb2D.velocity = new Vector2 (player.rb2D.velocity.x, player.doubleJumpForce);
            }
        }
        if(!player.isGrounded()) {
            player.jumpTimeCounter -= Time.deltaTime;
        }
        if(player.stoppedJumping && !player.canDoubleJump) {
            player.doubleJumpTimeCounter -= Time.deltaTime;
        }
        //if you stop holding down the jump button...
        if(player.jumpTimeCounter <= 0)
        {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            player.jumpTimeCounter = 0;
            player.stoppedJumping = true;
        }
        if(player.doubleJumpTimeCounter <= 0)
        {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            player.doubleJumpTimeCounter = 0;
            player.stoppedDoubleJumping = true;
        }
        if(player.isGrounded()) {
            player.jumpTimeCounter = player.jumpTime;
            player.doubleJumpTimeCounter = player.doubleJumpTime;
            player.canDoubleJump = true;
        }
        if (player.isTouchingCeiling()) {
            player.rb2D.velocity = new Vector2 (player.rb2D.velocity.x, 0);
            player.doubleJumpTimeCounter = 0;
            player.jumpTimeCounter = 0;
        }
    }

}
