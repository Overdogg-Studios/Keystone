using UnityEngine;
using System.Collections;


/**
 * Contains player abilities and attributes. Uses external states to activate and control available functions and animation states.
 * @type MonoBehaviour
 */
public class PlayerController : MonoBehaviour {

	public float direction;
    public int terminalVelocity;
    public const float LEFT = -1.0f;
    public const float RIGHT = 1.0f;
    public ArrayList list = new ArrayList();
	
	public LayerMask groundMask;

	ProjectileShooter weapon;

	public HealthPool healthPool;
	public Entity entity;
	public SavePoint lastSavePoint;
    public Vector3 spawnPoint;
    public State currentState;
	
    public float jumpForce;
    public float jumpTime;
    public float jumpTimeCounter;
    public float doubleJumpForce;
    public float doubleJumpTime;
    public float doubleJumpTimeCounter;
    [HideInInspector] public bool canDoubleJump;
	[HideInInspector] public bool stoppedJumping = false;
	[HideInInspector] public bool stoppedDoubleJumping = true;

	[HideInInspector] public Animator animator;

	void Start () {
        entity = GetComponent<Entity>();
        spawnPoint = transform.position;
        currentState = new Default();
		animator = GetComponent<Animator>();
		direction = RIGHT;
		weapon = GetComponent<ProjectileShooter>();
        jumpTimeCounter = jumpTime;
		//entity.rb = GetComponent<Rigidbody2D>();
		healthPool = GetComponent<HealthPool>();
		healthPool.currentHealth = healthPool.maxHealth;
		
	}
	void Update()
    {
        currentState.Update();
		respawn();
		
    }
    /**
     * Reverses the player sprite on it's x if the direction in which the player is moving changes.
     */
    public void flipSprite() {
    	if(direction == RIGHT) {
    		GetComponent<SpriteRenderer>().flipX = false;
    	}
    	if(direction == LEFT) {
    		GetComponent<SpriteRenderer>().flipX = true;
    	}
    }
    /**
     * Shoots a projectile from the player's weapon.
     */
    public void shoot() {

    	if(Input.GetKey("a") && weapon.currentTimeInterval <= 0) {

    		weapon.xDirection = entity.direction;

    		if((direction == LEFT && weapon.xOffset > 0) || (direction == Constant.RIGHT && weapon.xOffset < 0)) {

    			weapon.xOffset *= Constant.LEFT;
    		}
    		weapon.createProjectile();
    		weapon.currentTimeInterval = weapon.timeInterval;
    	}
    	else {
    		weapon.currentTimeInterval -= Time.deltaTime;  
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
            GetComponent<Renderer>().enabled = true; 
            healthPool.currentHealth = healthPool.maxHealth;
		}
	}
    /**
     * Assigns the player's save point to a new one. 
     */
	public void setSavePoint(SavePoint current) {
		lastSavePoint = current;
	}
}
