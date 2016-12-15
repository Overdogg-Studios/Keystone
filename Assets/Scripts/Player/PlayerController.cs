using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


	public float speed; //Current player speed.
	public float maxSpeed; //The fastest a player can move.
	public float acceleration; //How fast the player will accelerate to max speed.
	public float deceleration; //How fast the player decelerates down to 0 speed.
	public float sprintMultiplier; //How much the player's movement speed is multiplied if the player is sprinting.
	private float currentSprintMultiplier; //Internal variable used to keep track of whether or not the player is sprinting.

	public float rollTime;
	public float rollSpeedMultiplier;
	private float currentRollTime;
	private bool isRolling;


	public bool isDead; //Whether or not the character is currently dead (Unable to move, presented with a death screen).
	public int maxVelocity; //The player's maximum speed (falling speed primarily).

	public int currentPlayerHealth; //The current player's health.
	public int maxPlayerHealth; //The total player's health.

	//Points used to determine whether the player is currently in contact with a ceiling, is grounded, or is being squished.
	public Transform leftGroundPoint;
	public Transform centerGroundPoint;
	public Transform rightGroundPoint;

	public Transform leftCeilingPoint;
	public Transform centerCeilingPoint;
	public Transform rightCeilingPoint;

	public Transform topRightSidePoint;
	public Transform centerRightSidePoint;
	public Transform bottomRightSidePoint;

	public Transform topLeftSidePoint;
	public Transform centerLeftSidePoint;
	public Transform bottomLeftSidePoint;

	//How large the contact points are.
	private float radiusOfContactPoints = ContactPoint.radius;
	
	//Layermask of all things that the player can jump on and collide with. 
	public LayerMask groundMask;

	GameObject deathScreen;

	public SavePoint lastSavePoint;


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

	public CameraController mainCamera;

	Rigidbody2D rb2D;  
	// Use this for initialization
	void Start () {

		currentSprintMultiplier = 1;
        jumpTimeCounter = jumpTime;
		rb2D = GetComponent<Rigidbody2D>();
		currentPlayerHealth = maxPlayerHealth;
		deathScreen = (GameObject)Instantiate(Resources.Load("DeathScreen"), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
	}

	void FixedUpdate() {
		roll();
		if(!isDead && !isRolling) {
			sprint();
			move();
		}
	}
	void Update()
    {

    	if(!isDead && !isRolling) {
    		jump();
			
    	}
    	capMaxVelocity();
		die();
		respawn();
		
    }
    public void roll() {
    	if(Input.GetKey("space") && currentRollTime <= 0) {
    		currentRollTime = rollTime;
    		isRolling = true;
    	}
    	if(isRolling) {

    		transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, -1); 

    		currentRollTime -= Time.deltaTime;
    		if(currentRollTime <= 0) {
    			isRolling = false;
    		}
    	}
    }
    /**
     * Move the character left and right.
     */
    public void move() {
    	if(Input.GetKey("left") && (speed < (maxSpeed * currentSprintMultiplier))) {
    		if(speed > 0) {
    			speed = speed/10;
    		}
    		speed = speed - acceleration * Time.deltaTime * currentSprintMultiplier;

    		if(isTouchingLeftWall()) {
    			speed = 0;
    		}
    	}
    	else if ((Input.GetKey("right")) && (speed > (-1 * maxSpeed * currentSprintMultiplier))) {
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
    	if(speed < -1 * maxSpeed * currentSprintMultiplier) {
    		speed = -1 * maxSpeed * currentSprintMultiplier;
    	}
    	transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, -1); 
    }
    /**
     * Recieve damage. Although current design is that everything is one hit kill, the infastructure for a more gradual system is still in place.
     */
	public void takeDamage(int dmg) {
		currentPlayerHealth -= dmg;
	}
	/**
	 * Handles the character's death upon currentPlayerHealth reaching 0. 
	 */
	void die() {
		if(currentPlayerHealth <= 0) {


			isDead = true;
			rb2D.isKinematic = true;
			rb2D.velocity = new Vector2 (0, 0);
			this.GetComponent<Renderer>().enabled = false;


			//Spawn a DeathScreen object in the exact center of the screen. 
			mainCamera = GameObject.Find("Camera").GetComponent<CameraController>();
			Vector3 centerScreen = mainCamera.transform.position;
			centerScreen.z = -3;
			deathScreen.transform.position = centerScreen;
			deathScreen.GetComponent<Renderer>().enabled = true;
			

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
				
				deathScreen.GetComponent<Renderer>().enabled = false;
				currentPlayerHealth = maxPlayerHealth; 
				Debug.Log("Save Point" + saveLocation);
				Debug.Log("Player Position: " + transform.position);
			}
			//Otherwise just reload the level.
			else {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);﻿
			}
			
		}
		
	
	}
	/**
	 * Return whether or not the player's ground points are in contact with anything on the layermask "Ground Mask".
	 */
	public bool isGrounded() {

		return Physics2D.OverlapCircle(leftGroundPoint.position, radiusOfContactPoints, groundMask) || 
		Physics2D.OverlapCircle(rightGroundPoint.position, radiusOfContactPoints, groundMask) || 
		Physics2D.OverlapCircle(centerGroundPoint.position, radiusOfContactPoints, groundMask);
	}
	/**
	 * Return whether or not the player's ceilingPoints are in contact with anything on the layermask "Ground Mask".
	 */
	public bool isTouchingCeiling() {
		return Physics2D.OverlapCircle(leftCeilingPoint.position, radiusOfContactPoints, groundMask) || 
		Physics2D.OverlapCircle(rightCeilingPoint.position, radiusOfContactPoints, groundMask) || 
		Physics2D.OverlapCircle(centerCeilingPoint.position, radiusOfContactPoints, groundMask);
	}
	/**
	 * Return whether or not the player's right Wall Points are in contact with anything on the layermask "Ground Mask".
	 */
	public bool isTouchingRightWall() {
		return Physics2D.OverlapCircle(topRightSidePoint.position, radiusOfContactPoints, groundMask) || 
		Physics2D.OverlapCircle(centerRightSidePoint.position, radiusOfContactPoints, groundMask) || 
		Physics2D.OverlapCircle(bottomRightSidePoint.position, radiusOfContactPoints, groundMask);
	}
	/**
	 * Return whether or not the player's left Wall Points are in contact with anything on the layermask "Ground Mask".
	 */
	public bool isTouchingLeftWall() {
		return Physics2D.OverlapCircle(topLeftSidePoint.position, radiusOfContactPoints, groundMask) || 
		Physics2D.OverlapCircle(centerLeftSidePoint.position, radiusOfContactPoints, groundMask) || 
		Physics2D.OverlapCircle(bottomLeftSidePoint.position, radiusOfContactPoints, groundMask);
	}
	/**
	 * Controls whether or not the player is sprinting. If not sprinting, no additional speed is granted.
	 */
	public void sprint() {
		if (Input.GetKey("left shift")) {
			currentSprintMultiplier = sprintMultiplier;
		}
		else {
			currentSprintMultiplier = 1;
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
