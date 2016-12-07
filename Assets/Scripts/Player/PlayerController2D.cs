using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController2D : MonoBehaviour {


	public float moveSpeed; //Horizontal Move Speed.
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

	//How large the contact points are.
	public float radiusOfContactPoints;
	
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

		//sets the jumpCounter to whatever we set our jumptime to in the editor
        jumpTimeCounter = jumpTime;
		rb2D = GetComponent<Rigidbody2D>();
		currentPlayerHealth = maxPlayerHealth;
		deathScreen = (GameObject)Instantiate(Resources.Load("DeathScreen"), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
	}

	// Update is called once per frame
	void Update()
    {

    	if(!isDead) {
    		move();
    		jump();
			capMaxVelocity();
    	}

		die();
		respawn();
		
    }
    /**
     * Move the character left and right.
     */
    public void move() {
    	Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb2D.velocity.y);
		rb2D.velocity = moveDir;
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

				Vector3 saveLocation = new Vector3(lastSavePoint.xPosition, lastSavePoint.yPosition, -1);
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

		return Physics2D.OverlapCircle(leftGroundPoint.position, radiusOfContactPoints, groundMask) || Physics2D.OverlapCircle(rightGroundPoint.position, radiusOfContactPoints, groundMask) || Physics2D.OverlapCircle(centerGroundPoint.position, radiusOfContactPoints, groundMask);
	}
	/**
	 * Return whether or not the player's ceilingPoints are in contact with anything on the layermask "Ground Mask".
	 */
	public bool isTouchingCeiling() {
		return Physics2D.OverlapCircle(leftCeilingPoint.position, radiusOfContactPoints, groundMask) || Physics2D.OverlapCircle(rightCeilingPoint.position, radiusOfContactPoints, groundMask) || Physics2D.OverlapCircle(centerCeilingPoint.position, radiusOfContactPoints, groundMask);
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
