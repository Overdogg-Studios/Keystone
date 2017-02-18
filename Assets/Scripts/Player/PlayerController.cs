using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed; //Current player speed.
	public float maxSpeed; //The fastest a player can move.
	public float acceleration; //How fast the player will accelerate to max speed.
	public float deceleration; //How fast the player decelerates down to 0 speed.
	public float sprintMultiplier; //How much the player's movement speed is multiplied if the player is sprinting.
	public float currentSprintMultiplier; //Internal variable used to keep track of whether or not the player is sprinting.
	public float direction; //Which way the player is traveling. 1 for right, -1 for left.
    public int maxVelocity; //The player's maximum speed (falling speed primarily).

	public const float LEFT = -1.0f;
	public const float RIGHT = 1.0f;

	public float rollTime; //How long the player's roll lasts.
	public float rollDelay; //How long a player has to wait before they can roll again.
	public float rollSpeedMultiplier; //Multiplier that determines how much the roll's speed is multiplied by.
	public float currentRollTime;
	public float currentRollDelay;
	public bool isRolling;

    private Sprint sprintState;


	[HideInInspector]
	public bool isDead; //Whether or not the character is currently dead (Unable to move, presented with a death screen).

	//Boxes used to determine whether the player is currently in contact with a ceiling, is grounded, or is being squished.
	private Transform leftSideContactBox;
	private Transform rightSideContactBox;
	private Transform ceilingContactBox;
	private Transform groundContactBox;
	
	//Layermask of all things that the player can jump on and collide with. 
	public LayerMask groundMask;

	private ProjectileShooter weapon;
	private HealthPool hp;
	
	public SavePoint lastSavePoint;

	private State currentState;
	/*these floats are the force you use to jump, the max time you want your jump to be allowed to happen,
     * and a counter to track how long you have been jumping*/
    public float jumpForce;
    public float jumpTime;
    public float jumpTimeCounter;
    
    public float doubleJumpForce;
    public float doubleJumpTime;
    public float doubleJumpTimeCounter;
    bool canDoubleJump;
	bool stoppedJumping = false;
	bool stoppedDoubleJumping = true;

	public Rigidbody2D rb2D;  
	public Animator animator;
	// Use this for initialization
	void Start () {

		isDead = false;

        currentState = new Sprint();
		leftSideContactBox = transform.Find("Contact Boxes/Left Side Contact Box");
		rightSideContactBox = transform.Find("Contact Boxes/Right Side Contact Box");
		ceilingContactBox = transform.Find("Contact Boxes/Ceiling Contact Box");
		groundContactBox = transform.Find("Contact Boxes/Ground Contact Box");

		direction = RIGHT;
		weapon = GetComponent<ProjectileShooter>();
		currentSprintMultiplier = 1;
        jumpTimeCounter = jumpTime;
		rb2D = GetComponent<Rigidbody2D>();
		hp = GetComponent<HealthPool>();
		hp.currentHealth = hp.maxHealth;
		animator = GetComponent<Animator>();
	}

	void FixedUpdate() {
		
		//if(!isDead && !isRolling) {
		currentState.Update();
		//}
	}
	void Update()
    {

    	if(!isDead) {
    		flipSprite();
		}
    	if(!isDead && !isRolling) {
    		jump();
			
    	}
		die();
		respawn();
 
       capMaxVelocity();
		
    }
    public void flipSprite() {
    	if(direction == RIGHT) {
    		this.GetComponent<SpriteRenderer>().flipX = false;
    	}
    	if(direction == LEFT) {
    		this.GetComponent<SpriteRenderer>().flipX = true;
    	}
    }
    /**
     * Shoots a projectile from the projectile shooter.
     */
    /*
    public void shoot() {

    	if(Input.GetKey("a") && weapon.currentTimeInterval <= 0) {

    		weapon.xDirection = direction;

    		if((direction == LEFT && weapon.xOffset > 0) || (direction == RIGHT && weapon.xOffset < 0)) {

    			weapon.xOffset *= LEFT;
    		}
    		weapon.createProjectile();
    		weapon.currentTimeInterval = weapon.timeInterval;
    	}
    	else {
    		weapon.currentTimeInterval -= Time.deltaTime;  
    	}
    	
    }
    */
    /*
    public void roll() {

    	if(Input.GetKeyDown("space") && (Input.GetKey("left") || Input.GetKey("right")) && currentRollTime <= 0 && isGrounded() && currentRollDelay <= 0) {
    		currentRollTime = rollTime;
    		currentRollDelay = rollDelay;
    		isRolling = true;
    		animator.SetInteger("State", 3);

	    	if(Input.GetKey("left")) {
	    		direction = LEFT;
	    	}
	    	if(Input.GetKey("right")) {
	    		direction = RIGHT;
	    	}
    	}
    	else if(isRolling && direction != 0) {

    		speed = currentRollTime * direction * rollSpeedMultiplier;

    		if(isTouchingLeftWall() && direction == LEFT) {
    			speed = 0;
    		}
    		if(isTouchingRightWall() && direction == RIGHT) {
    			speed = 0;
    		}

    		transform.position = new Vector3(transform.position.x + speed, transform.position.y, -1); 

    		currentRollTime -= Time.deltaTime;
    		if(currentRollTime <= 0) {
    			isRolling = false;
    		}
    	}
    	else {
    		currentRollDelay -= Time.deltaTime;
    	}
    }
    */
    /**
     * Move the character left and right.
     */
    /*
    public void move() {

    	if(Input.GetKey("left") || Input.GetKey("right")) {
    		if(animator.GetInteger("State") != 1) {
    			animator.SetInteger("State", 4);
    		}
    	}
    	else {
    		animator.SetInteger("State", 0);
    	}
    	if(Input.GetKey("left") && (speed < (maxSpeed * currentSprintMultiplier))) {
    		
    		
    		direction = LEFT;
    		if(speed > 0) {
    			
    			speed = speed/10;
    		}
    		speed = speed - acceleration * Time.deltaTime * currentSprintMultiplier;

    		if(isTouchingLeftWall()) {
    			speed = 0;
    		}
    	}
    	else if ((Input.GetKey("right")) && (speed > (LEFT * maxSpeed * currentSprintMultiplier))) {
    		direction = RIGHT;
    		if(speed < 0) {
    			speed = speed/10;
    		}
    		speed = speed + acceleration * Time.deltaTime * currentSprintMultiplier;

    		if(isTouchingRightWall()) {
    			speed = 0;
    		}

    	}
    	else {
    		
    		if(speed > deceleration * Time.deltaTime) {
    			speed = speed - deceleration * Time.deltaTime * currentSprintMultiplier;
    		}
    		else if(speed < -deceleration * Time.deltaTime) {
    			speed = speed + deceleration * Time.deltaTime * currentSprintMultiplier;
    		}
    		else {
    			speed = 0;
    		}
    	}
    	if(speed > maxSpeed * currentSprintMultiplier) {
    		speed = maxSpeed * currentSprintMultiplier;
    	}
    	if(speed < LEFT * maxSpeed * currentSprintMultiplier) {
    		speed = LEFT * maxSpeed * currentSprintMultiplier;
    	}
    	transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, -1); 
    }
    /**
     * Recieve damage. Although current design is that everything is one hit kill, the infastructure for a more gradual system is still in place.
     */
	/**
	 * Handles the character's death upon currentPlayerHealth reaching 0. 
	 */
	void die() {
		if(hp.currentHealth <= 0) {
			isDead = true;
			rb2D.isKinematic = true;
			rb2D.velocity = new Vector2 (0, 0);
			this.GetComponent<Renderer>().enabled = false;
		}
	}
	void respawn() {

		if(Input.GetKeyDown(KeyCode.R)) {

			//If the player has not reached a save point. 
			if(lastSavePoint != null) {

				isDead = false;
				rb2D.isKinematic = false;
				Vector3 saveLocation = lastSavePoint.getPosition();
				transform.position = saveLocation;
				this.GetComponent<Renderer>().enabled = true; 
				hp.currentHealth = hp.maxHealth;
			}
			//Otherwise just reload the level.
			else {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);﻿
			}
			
		}
		
	}
	/**
	 * Check to see if the player is touching the ground, ceiling or a wall on either side.
	 * @return True if touching, false otherwise.
	 */
	public bool isGrounded() {
		return Physics2D.OverlapBox(groundContactBox.position, new Vector2(groundContactBox.GetComponent<ContactBox>().x, groundContactBox.GetComponent<ContactBox>().y), 0f, groundMask);
	}
	public bool isTouchingCeiling() {
		return Physics2D.OverlapBox(ceilingContactBox.position, new Vector2(ceilingContactBox.GetComponent<ContactBox>().x, ceilingContactBox.GetComponent<ContactBox>().y), 0f, groundMask);
	}
	public bool isTouchingRightWall() {
		return Physics2D.OverlapBox(rightSideContactBox.position, new Vector2(rightSideContactBox.GetComponent<ContactBox>().x, rightSideContactBox.GetComponent<ContactBox>().y), 0f, groundMask);
	}
	public bool isTouchingLeftWall() {
		return Physics2D.OverlapBox(leftSideContactBox.position, new Vector2(leftSideContactBox.GetComponent<ContactBox>().x, leftSideContactBox.GetComponent<ContactBox>().y), 0f, groundMask);
	}
	/**
	 * Controls whether or not the player is sprinting. If not sprinting, no additional speed is granted.
	 */
	public void sprint() {
		if (Input.GetKey("left shift")) {
			currentSprintMultiplier = sprintMultiplier;
			animator.SetInteger("State", 1);
		}
		else {
			currentSprintMultiplier = 1;
			if(animator.GetInteger("State") != 4) {
				animator.SetInteger("State", 0);
			}
			
		}
	}
	/**
	 * Cap the player's speed (typically falling speed) at a given maxVelocity.
	 */
	public void capMaxVelocity() {
		if(rb2D.velocity.magnitude > maxVelocity) {
			rb2D.velocity = rb2D.velocity.normalized*maxVelocity;
		}
	}
	/**
	 * Controls the character's ability to jump, double jump and deals with the character's response to various jump related collisons. (Ground and Ceiling).
	 */
	public void jump() {
		if(Input.GetKeyDown(KeyCode.E) )
        {
        	//animator.SetInteger("State", 2);
            if(isGrounded())
            {	
                rb2D.velocity = new Vector2 (rb2D.velocity.x, jumpForce);
                stoppedJumping = false;
            }
            else if(canDoubleJump) {
            	
            	rb2D.velocity = new Vector2 (rb2D.velocity.x, 0);
            	rb2D.velocity = new Vector2 (rb2D.velocity.x, jumpForce);
               	stoppedDoubleJumping = false;
                 canDoubleJump = false;

            }
            else {

            }
        }
        //if you keep holding down the jump button...
        if((Input.GetKey(KeyCode.E)) && !stoppedJumping)
        {
            //and your counter hasn't reached zero...
            if(jumpTimeCounter > 0)
            {
                //keep jumping!
                rb2D.velocity = new Vector2 (rb2D.velocity.x, jumpForce);
            }
        }
        if((Input.GetKey(KeyCode.E)) && !stoppedDoubleJumping)
        {
            //and your counter hasn't reached zero...
            if(doubleJumpTimeCounter > 0)
            {
                rb2D.velocity = new Vector2 (rb2D.velocity.x, doubleJumpForce);
            }
        }
 		if(!isGrounded()) {
 			jumpTimeCounter -= Time.deltaTime;
 		}
 		if(stoppedJumping && !canDoubleJump) {
 			doubleJumpTimeCounter -= Time.deltaTime;
 		}
        //if you stop holding down the jump button...
        if(jumpTimeCounter <= 0)
        {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }
        if(doubleJumpTimeCounter <= 0)
        {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            doubleJumpTimeCounter = 0;
            stoppedDoubleJumping = true;
        }
        if(isGrounded()) {
			jumpTimeCounter = jumpTime;
			doubleJumpTimeCounter = doubleJumpTime;
			canDoubleJump = true;
		}
		if (isTouchingCeiling()) {
			rb2D.velocity = new Vector2 (rb2D.velocity.x, 0);
			doubleJumpTimeCounter = 0;
			jumpTimeCounter = 0;
		}
	}
	public void setSavePoint(SavePoint current) {
		lastSavePoint = current;
	}
}
