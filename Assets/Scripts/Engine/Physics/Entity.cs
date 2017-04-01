using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    //Collision Variables
    [HideInInspector] private LayerMask collideWith;
    public Rigidbody2D rb;
    private BoxCollider2D collider;
    private Vector2 hitboxSize;

    //Movement Variables
    public float horizontalSpeed; 
    public float horizontalAcceleration = 8; 
    public float horizontalDeceleration = 15;
    public float maxHorizontalVelocity = 5;
    public float maxVerticalVelocity = 6.5f;
    public float direction;
    public float pivotMultiplier = 2;
    
    //Jump Variables
    public float jumpForce = 5;
    public float jumpTime = 0.15f;
    public float jumpTimeCounter;
    public int currentNumberOfJumps;
    public int numberOfJumps = 1;

    //Miscellaneous
    public bool flipX;

    [ExecuteInEditMode]
    void Start ()
    {
        collideWith = LayerMask.GetMask("Ground");
        direction = Constant.RIGHT;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        hitboxSize = collider.size - new Vector2(collider.size.x / 10, collider.size.y / 10);
    }

    private void Update()
    {

        capVelocity();
        //Debug.Log("Ground: " + isGrounded() + " Ceiling: " + isTouchingCeiling() + " Left Wall: " + isTouchingLeftWall() + " Right Wall: " + isTouchingRightWall());
    }
    bool placeFree( Vector2 newPosition )
    {
        Collider2D hit = Physics2D.OverlapBox(newPosition + collider.offset, hitboxSize, 0, collideWith);
        return !hit;
    }

    public bool isGrounded() {
        return !placeFree( (Vector2) transform.position + (Vector2.down * 0.05f));
    }
    public bool isTouchingCeiling() {
        return !placeFree( (Vector2) transform.position + (Vector2.up * 0.05f));
    }
    public bool isTouchingRightWall() {
        return !placeFree( (Vector2) transform.position + (Vector2.right * 0.05f));
    }
    public bool isTouchingLeftWall() {
        return !placeFree( (Vector2) transform.position + (Vector2.left * 0.05f));
    }
    private void capVelocity() {

        if(rb.velocity.x > maxHorizontalVelocity) {
            rb.velocity = new Vector2(maxHorizontalVelocity, rb.velocity.y);
        }
        if(rb.velocity.x < -maxHorizontalVelocity) {
            rb.velocity = new Vector2(-maxHorizontalVelocity, rb.velocity.y);
        }
        if(rb.velocity.y > maxVerticalVelocity) {
            rb.velocity = new Vector2(rb.velocity.x, maxVerticalVelocity);
        }
        if(rb.velocity.y < -maxVerticalVelocity) {
            rb.velocity = new Vector2(rb.velocity.x, -maxVerticalVelocity);
        }
    }
    /**
     * Controls the player's horizontal movement via transform.positon. 
     */
    public void move(float direction) {

        //Accelerate the object.
        if(direction == Constant.LEFT) {
            
            this.direction = direction;
            flipX = true;
            horizontalSpeed = horizontalSpeed - horizontalAcceleration * Time.deltaTime;
            
            if(rb.velocity.x > 0) {
                horizontalSpeed /= pivotMultiplier;
            }
        }
        else if (direction == Constant.RIGHT) {
            
            this.direction = direction;
            flipX = false;
            horizontalSpeed = horizontalSpeed + horizontalAcceleration * Time.deltaTime;

            if(rb.velocity.x < 0) {
                horizontalSpeed /= pivotMultiplier;
            }

        }
        //Accelerate the object.
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
        if(horizontalSpeed > maxHorizontalVelocity) {
            horizontalSpeed = maxHorizontalVelocity;
        }
        if(horizontalSpeed < Constant.LEFT * maxHorizontalVelocity ) {
            horizontalSpeed = Constant.LEFT * maxHorizontalVelocity;
        }
        
        rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
    }
    /**
     * Controls the character's ability to jump and double jump.
     */
    public void jump(bool newJump = true, bool holdingJump = true) {
        if (isGrounded()) {
            currentNumberOfJumps = numberOfJumps;
        }

        if (currentNumberOfJumps > 0 && newJump) {
            currentNumberOfJumps--;
            jumpTimeCounter = jumpTime;
        }
        if(jumpTimeCounter >= 0 && holdingJump) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }
        
    }
    /**
     * Reverses the character's sprite on it's x if the direction in which the player is moving changes.
     */
    public void flipSprite() {
        if(!flipX) {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    /**
     * Used for managing one off collisions related to entity movement.
     * @param Collider2D collision: The object being collided with.
     */
    void OnCollisionEnter2D(Collision2D collision) {

        Debug.Log("WOAH");
        if(isTouchingLeftWall() || isTouchingRightWall()    ) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if(isTouchingCeiling()) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}