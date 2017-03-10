using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    [HideInInspector]
    public Vector2 position;
    [HideInInspector]
    public Vector2 previous;

    public LayerMask collideWith;
    public Rect hitBox;

    public Vector2 velocity;
    public Vector2 acceleration;
    public float gravity;

    public bool onGround = false;

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
        position = transform.position;
        previous = position;

	}

    //Validate hitbox size in editor
    private void OnValidate()
    {
        if (hitBox.size == Vector2.zero)
            hitBox.size = Vector2.one;
    }

    private void Update()
    {
        PhysUpdate();

        onGround = !placeFree(position + (Vector2.down * 0.1F) );

        //Error Handling
        if( !placeFree(position) )
        {
            Debug.Log("stuck in wall");
        }
    }

	void PhysUpdate ()
    {
        position = transform.position;
        previous = position;

        Vector2 newPosition = position + velocity * Time.deltaTime;
        velocity += acceleration * Time.deltaTime;

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
        Debug.Log("MoveContact Fail: (" + movement.x + ") , (" + movement.y + ")");
    }

    bool placeFree( Vector2 newPosition )
    {
        Collider2D hit = Physics2D.OverlapBox(newPosition + hitBox.position, hitBox.size, 0, collideWith);
        return !hit;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(new Vector3(transform.position.x + hitBox.x, transform.position.y + hitBox.y, transform.position.z), hitBox.size );
    }
    public bool isGrounded() {
		return onGround;
	}
	public bool isTouchingCeiling() {
		return false;
	}
	public bool isTouchingRightWall() {
		return false;
	}
	public bool isTouchingLeftWall() {
		return false;
	}
}