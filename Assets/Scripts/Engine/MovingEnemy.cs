using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour {

    public enum Difficulty {EASY,MEDUIM,HARD};
    public Difficulty enemyDifficulty;
    private GameObject allen;
    private float motionTimer;
    private int direction; // If 0 moving left, if 1 moving right
    private Vector3 startPosition;
    private bool movedOffTrack;
    // Use this for initialization
    void Start ()
    {
        direction = 0;
        startPosition = transform.position;
        movedOffTrack = false;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        allen = GameObject.FindGameObjectWithTag("Player");
        float xDistance = Mathf.Abs(transform.position.x - allen.transform.position.x);
        float yDistance = Mathf.Abs(transform.position.y - allen.transform.position.y);
        //Debug.Log(xDistance);
        if (enemyDifficulty == Difficulty.EASY)
        {
            motionTimer += Time.deltaTime;
            if(motionTimer >= 1.0)
            {
                if(direction == 0)
                {
                    direction = 1;
                }
                else
                {
                    direction = 0;
                }
                motionTimer = 0;
            }
            if(direction == 0)
            {
                transform.position += new Vector3(-0.07f, 0, 0);
            }
            else
            {
                transform.position += new Vector3(0.07f, 0, 0);
            }
        }
        else if(enemyDifficulty == Difficulty.MEDUIM)
        {
            if(movedOffTrack == false)
            {
                //Debug.Log("Standard Movement");
                motionTimer += Time.deltaTime;
                if (motionTimer >= 1.0)
                {
                    if (direction == 0)
                    {
                        direction = 1;
                    }
                    else
                    {
                        direction = 0;
                    }
                    motionTimer = 0;
                }
                if (direction == 0)
                {
                    transform.position += new Vector3(-0.07f, 0, 0);
                }
                else
                {
                    transform.position += new Vector3(0.07f, 0, 0);
                }
            }
            if (xDistance < 2.0f && yDistance < 2.0f)
            {
                if(transform.position.x > allen.transform.position.x)
                {
                    transform.position += new Vector3(-0.07f, 0, 0);
                }
                else
                {
                    transform.position += new Vector3(0.07f, 0, 0);
                }
                movedOffTrack = true;
                motionTimer = 0;
                //Debug.Log("OFf Track");
            }
            else
            {
                if(movedOffTrack)
                {
                    if(transform.position.x > startPosition.x)
                    {
                        transform.position += new Vector3(-0.17f, 0, 0);
                    }
                    else
                    {
                        transform.position += new Vector3(0.17f, 0, 0);
                    }
                    if(Mathf.Abs(transform.position.x - startPosition.x) < 0.1f)
                    {
                        motionTimer = 0;
                        movedOffTrack = false;
                        direction = 0;
                        //Debug.Log("Back on track");
                    }
                }
            }
        }
        else if(enemyDifficulty == Difficulty.HARD)
        {
            if (movedOffTrack == false)
            {
                //Debug.Log("Standard Movement");
                motionTimer += Time.deltaTime;
                if (motionTimer >= 1.0)
                {
                    if (direction == 0)
                    {
                        direction = 1;
                    }
                    else
                    {
                        direction = 0;
                    }
                    motionTimer = 0;
                }
                if (direction == 0)
                {
                    transform.position += new Vector3(-0.07f, 0, 0);
                }
                else
                {
                    transform.position += new Vector3(0.07f, 0, 0);
                }
            }
            if (xDistance < 3.0f && yDistance < 2.0f)
            {
                if (transform.position.x > allen.transform.position.x)
                {
                    transform.position += new Vector3(-0.17f, 0, 0);
                }
                else
                {
                    transform.position += new Vector3(0.17f, 0, 0);
                }
                movedOffTrack = true;
                motionTimer = 0;
                //Debug.Log("OFf Track");
            }
            else
            {
                if (movedOffTrack)
                {
                    if (transform.position.x > startPosition.x)
                    {
                        transform.position += new Vector3(-0.17f, 0, 0);
                    }
                    else
                    {
                        transform.position += new Vector3(0.17f, 0, 0);
                    }
                    if (Mathf.Abs(transform.position.x - startPosition.x) < 0.1f)
                    {
                        motionTimer = 0;
                        movedOffTrack = false;
                        direction = 0;
                        //Debug.Log("Back on track");
                    }
                }
            }
        }
        else
        {
            throw new UnityException("Enemy has no difficulty");
        }
	}
}
