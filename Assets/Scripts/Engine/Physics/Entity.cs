using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    [HideInInspector]
    public Vector2 position;
    [HideInInspector]
    public Vector2 previous;

    public LayerMask collideWith;

    private Rect hitbox;
    private BoxCollider2D collider;
    public Vector2 velocity;
    public Vector2 acceleration;
    public float maxHorizontalVelocity;
    public float maxVerticalVelocity;

    //Helper Functions
    public Vector2 direction
    {
        get {
            return velocity.normalized;
        }
        set {
            velocity = value.normalized * velocity.magnitude;
        }
    }
    public float speed
    {
        get {
            return velocity.magnitude;
        }
        set {
            velocity = direction * speed;
        }
    }

    [ExecuteInEditMode]
    void Start ()
    {
        collider = GetComponent<BoxCollider2D>();
        hitbox.size = new Vector2(hitbox.size.x * collider.size.x, hitbox.size.y * collider.size.y);
        
        position = transform.position;
        previous = position;

    }

    //Validate hitbox size in editor
    private void OnValidate()
    {
        if (hitbox.size == Vector2.zero) {
           hitbox.size = Vector2.one;
        }
    }

    private void Update()
    {
        PhysUpdate();

    }

    void PhysUpdate ()
    {
        position = transform.position;
        previous = position;

        Vector2 newPosition = position + velocity * Time.deltaTime;
        velocity += acceleration * Time.deltaTime;
        capMaxVelocity();
        //Attempt move; if position blocked, slide along X or Y axis
        if ( !moveTo( newPosition ) )
        {
            if ( moveTo(new Vector2(position.x, newPosition.y)))
            {
                moveContact(new Vector2(newPosition.x, position.y));
                velocity.x = 0;
            }
            else if ( moveTo(new Vector2(newPosition.x, position.y)))
            {
                moveContact(new Vector2(position.x, newPosition.y));
                velocity.y = 0;
            }
            else
                moveContact(new Vector2(position.x, newPosition.y));

        }
  
        //Move Transform
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
    bool moveTo(Vector2 newPosition)
    {
        if (placeFree(newPosition))
        {
            position = newPosition;
            return true;
        }
        return false;
    }

    void moveContact(Vector2 newPosition)
    {
        float precision = 0.1F;
        Vector2 movement = newPosition - position;
        Vector2 bestPosition = position;
        for (float i = 0; i <= 1; i += precision)
        {
            if ( placeFree(position + movement * i) )
                bestPosition = position+movement*i;
            else
            {
                position = bestPosition;
                return;
            }
        }
    }

    bool placeFree( Vector2 newPosition )
    {
        Collider2D hit = Physics2D.OverlapBox(newPosition + hitbox.position + collider.offset, hitbox.size, 0, collideWith);
        return !hit;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(new Vector3(transform.position.x + hitbox.x + collider.offset.x, transform.position.y + hitbox.y + collider.offset.y, transform.position.z), hitbox.size );
    }
    public bool isGrounded() {
        return !placeFree(position + (Vector2.down * 0.05F));
    }
    public bool isTouchingCeiling() {
        return !placeFree(position + (Vector2.up * 0.05F));
    }
    public bool isTouchingRightWall() {
        return !placeFree(position + (Vector2.right * 0.05F));
    }
    public bool isTouchingLeftWall() {
        return !placeFree(position + (Vector2.left * 0.05F));
    }
    private void capMaxVelocity() {
        if(velocity.x > maxHorizontalVelocity) {
            velocity.x = maxHorizontalVelocity;
        }
        if(velocity.x < -maxHorizontalVelocity) {
            velocity.x = -maxHorizontalVelocity;
        }
        if(velocity.y > maxVerticalVelocity) {
            velocity.y = maxVerticalVelocity;
        }
        if(velocity.y < -maxVerticalVelocity) {
            velocity.y = -maxVerticalVelocity;
        }
    }
}