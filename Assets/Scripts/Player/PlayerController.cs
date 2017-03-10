using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Contains player abilities and attributes. Uses external states to activate and control available functions and animation states.
 * @type MonoBehaviour
 */
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
	
	public LayerMask groundMask;

	ProjectileShooter weapon;

	public HealthPool healthPool;
	
	public SavePoint lastSavePoint;
    public Vector3 spawnPoint;
    public State currentState;
	
    public float jumpForce;
    public float jumpTime;
    public float jumpTimeCounter;
    public Entity entity;
    public float doubleJumpForce;
    public float doubleJumpTime;
    public float doubleJumpTimeCounter;
    [HideInInspector] public bool canDoubleJump;
	[HideInInspector] public bool stoppedJumping = false;
	[HideInInspector] public bool stoppedDoubleJumping = true;

	//[HideInInspector] public Rigidbody2D rb2D; 
	[HideInInspector] public Animator animator;

	void Start () {

        entity = GetComponent<Entity>();
        spawnPoint = transform.position;
        currentState = new Default();
		animator = GetComponent<Animator>();
		currentSprintMultiplier = 1;
		direction = RIGHT;
		weapon = GetComponent<ProjectileShooter>();
        jumpTimeCounter = jumpTime;
		//rb2D = GetComponent<Rigidbody2D>();
		healthPool = GetComponent<HealthPool>();
		healthPool.currentHealth = healthPool.maxHealth;
		
	}
	void Update()
    {
        Debug.Log(currentState);
        currentState.Update();
		die();
		respawn();
		
    }
    /**
     * Reverses the player sprite on it's x if the direction in which the player is moving changes.
     */
    public void flipSprite() {
    	if(direction == RIGHT) {
    		this.GetComponent<SpriteRenderer>().flipX = false;
    	}
    	if(direction == LEFT) {
    		this.GetComponent<SpriteRenderer>().flipX = true;
    	}
    }
    /**
     * Shoots a projectile from the player's weapon.
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
	/**
	 * Handles character death.
	 */
	void die() {
		if(healthPool.currentHealth <= 0) {
			//rb2D.isKinematic = true;
			//rb2D.velocity = new Vector2 (0, 0);
			this.GetComponent<Renderer>().enabled = false;
		}
	}
    /**
     * Handles character respawning.
     */
	void respawn() {

		if(Input.GetKeyDown(KeyCode.R)) {

			if(lastSavePoint != null) {
				Vector3 saveLocation = lastSavePoint.getPosition();
				transform.position = saveLocation;
			}
			else {
                transform.position = spawnPoint;
			}
            
            //rb2D.isKinematic = false;
            this.GetComponent<Renderer>().enabled = true; 
            healthPool.currentHealth = healthPool.maxHealth;
		}
	}
	/**
	 * Controls the character's ability to jump, double jump and deals with the character's response to various jump related collisons. (Ground and Ceiling).
     * Uses the rigidbody attached to the player to accomplish this.
	 */
	public void jump() {

        /*
		if(Input.GetKeyDown(KeyCode.E) )
        {
            if(entity.isGrounded())
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
 		if(!entity.isGrounded()) {
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
        if(entity.isGrounded()) {
			jumpTimeCounter = jumpTime;
			doubleJumpTimeCounter = doubleJumpTime;
			canDoubleJump = true;
		}
		if (entity.isTouchingCeiling()) {
			rb2D.velocity = new Vector2 (rb2D.velocity.x, 0);
			doubleJumpTimeCounter = 0;
			jumpTimeCounter = 0;
		}
        */
	}
    /**
     * Assigns the player's save point to a new one. 
     */
	public void setSavePoint(SavePoint current) {
		lastSavePoint = current;
	}
    /**
     * Controls the player's horizontal movement via transform.positon. Does not use the player's rigidbody.
     */
	public void move() {

        
    	if(Input.GetKey("left") && (horizontalSpeed < (maxSpeed))) {

    		entity.velocity = new Vector2(entity.velocity.x -= 0.1f, entity.velocity.y);

    		direction = LEFT;
    		if(horizontalSpeed > 0) {
    			
    			horizontalSpeed = horizontalSpeed/10;
    		}
    		horizontalSpeed = horizontalSpeed - horizontalAcceleration * Time.deltaTime;

    		if(entity.isTouchingLeftWall()) {
    			horizontalSpeed = 0;
    		}
    	}
    	else if ((Input.GetKey("right")) && (horizontalSpeed > (LEFT * maxSpeed))) {

            entity.velocity = new Vector2(entity.velocity.x += 0.1f, entity.velocity.y);

    		direction = RIGHT;
    		if(horizontalSpeed < 0) {
    			horizontalSpeed = horizontalSpeed/10;
    		}
    		horizontalSpeed = horizontalSpeed + horizontalAcceleration * Time.deltaTime;

    		if(entity.isTouchingRightWall()) {
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
    	//transform.position = new Vector3(transform.position.x + horizontalSpeed * Time.deltaTime * currentSprintMultiplier, transform.position.y, -1); 
    }
}
