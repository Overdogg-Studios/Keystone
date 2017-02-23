using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float horizontalSpeed; 
	public float maxSpeed; 
	public float horizontalAcceleration; 
	public float horizontalDeceleration;
	public float sprintMultiplier;
	public float currentSprintMultiplier;
	public float direction;
    public int terminalVelocity;

	public const float LEFT = -1.0f;
	public const float RIGHT = 1.0f;

	[HideInInspector]
	public bool isDead;

	ContactBox leftSideContactBox;
	ContactBox rightSideContactBox;
	ContactBox ceilingContactBox;
	ContactBox groundContactBox;
	
	//Layermask of all things that the player can jump on and collide with. 
	public LayerMask groundMask;

	ProjectileShooter weapon;
	HealthPool hp;
	
	public SavePoint lastSavePoint;

	State currentState;
	/*these floats are the force you use to jump, the max time you want your jump to be allowed to happen,
     * and a counter to track how long you have been jumping*/
    public float jumpForce;
    public float jumpTime;
    public float jumpTimeCounter;
    
    public float doubleJumpForce;
    public float doubleJumpTime;
    public float doubleJumpTimeCounter;
    public bool canDoubleJump;
	public bool stoppedJumping = false;
	public bool stoppedDoubleJumping = true;

	public Rigidbody2D rb2D; 

	public Animator animator;
	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator>();
		isDead = false;
		currentSprintMultiplier = 1;
        currentState = new Idle();
		leftSideContactBox = transform.Find("Contact Boxes/Left Side Contact Box").GetComponent<ContactBox>();
		rightSideContactBox = transform.Find("Contact Boxes/Right Side Contact Box").GetComponent<ContactBox>();
		ceilingContactBox = transform.Find("Contact Boxes/Ceiling Contact Box").GetComponent<ContactBox>();
		groundContactBox = transform.Find("Contact Boxes/Ground Contact Box").GetComponent<ContactBox>();

		direction = RIGHT;
		weapon = GetComponent<ProjectileShooter>();
        jumpTimeCounter = jumpTime;
		rb2D = GetComponent<Rigidbody2D>();
		hp = GetComponent<HealthPool>();
		hp.currentHealth = hp.maxHealth;
		
	}
	void Update()
    {
    	currentState.Update();
    	flipSprite();
		die();
		respawn();
 		DetermineState();
		capMaxVelocity();
		shoot();
		
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
    void DetermineState() {
    	if(isDead) {
    		currentState = new Frozen();
    	}
    	else if(!isGrounded()) {
    		currentState = new InAir();
    	}
		else if (Input.GetKey("left") || Input.GetKey("right")) {
			if(Input.GetKey("left shift")) {
				currentState = new Sprint();
			}
			else {
				currentState = new Run();
			}
		}
		else {
			currentState = new Idle();
		}
		Debug.Log(currentState);
    }
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
	 * @return true if touching, false otherwise.
	 */
	public bool isGrounded() {
		return Physics2D.OverlapBox(groundContactBox.position, new Vector2(groundContactBox.x, groundContactBox.y), 0f, groundMask);
	}
	public bool isTouchingCeiling() {
		return Physics2D.OverlapBox(ceilingContactBox.position, new Vector2(ceilingContactBox.x, ceilingContactBox.y), 0f, groundMask);
	}
	public bool isTouchingRightWall() {
		return Physics2D.OverlapBox(rightSideContactBox.position, new Vector2(rightSideContactBox.x, rightSideContactBox.y), 0f, groundMask);
	}
	public bool isTouchingLeftWall() {
		return Physics2D.OverlapBox(leftSideContactBox.position, new Vector2(leftSideContactBox.x, leftSideContactBox.y), 0f, groundMask);
	}
	/**
	 * Cap the player's speed (typically falling speed) at a given terminalVelocity.
	 */
	public void capMaxVelocity() {
		if(rb2D.velocity.magnitude > terminalVelocity) {
			rb2D.velocity = rb2D.velocity.normalized*terminalVelocity;
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
	public void move() {
    	if(Input.GetKey("left") && (horizontalSpeed < (maxSpeed))) {
    		
    		direction = LEFT;
    		if(horizontalSpeed > 0) {
    			
    			horizontalSpeed = horizontalSpeed/10;
    		}
    		horizontalSpeed = horizontalSpeed - horizontalAcceleration * Time.deltaTime;

    		if(isTouchingLeftWall()) {
    			horizontalSpeed = 0;
    		}
    	}
    	else if ((Input.GetKey("right")) && (horizontalSpeed > (LEFT * maxSpeed))) {
    		direction = RIGHT;
    		if(horizontalSpeed < 0) {
    			horizontalSpeed = horizontalSpeed/10;
    		}
    		horizontalSpeed = horizontalSpeed + horizontalAcceleration * Time.deltaTime;

    		if(isTouchingRightWall()) {
    			horizontalSpeed = 0;
    		}

    	}
    	else {
    		
    		if(horizontalSpeed > horizontalDeceleration * Time.deltaTime) {
    			horizontalSpeed = horizontalSpeed - horizontalDeceleration * Time.deltaTime;
    		}
    		else if(horizontalSpeed < -horizontalDeceleration * Time.deltaTime) {
    			horizontalSpeed = horizontalSpeed + horizontalDeceleration * Time.deltaTime;
    		}
    		else {
    			horizontalSpeed = 0;
    		}
    	}
    	if(horizontalSpeed > maxSpeed) {
    		horizontalSpeed = maxSpeed;
    	}
    	if(horizontalSpeed < LEFT * maxSpeed ) {
    		horizontalSpeed = LEFT * maxSpeed;
    	}
    	transform.position = new Vector3(transform.position.x + horizontalSpeed * Time.deltaTime * currentSprintMultiplier, transform.position.y, -1); 
    }
}
