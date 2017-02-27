using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Animator : MonoBehaviour {

    public Texture2D sprint;
    public Texture2D walk;

    private Test_Animation sprintAnim;
    private Test_Animation walkAnim;


    private float testTimer = 0.0f; // Used so i do not have to press keys for testing

	// Use this for initialization
	void Start ()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        sprintAnim = new Test_Animation(sprint, 1, false,false,renderer);
        walkAnim = new Test_Animation(walk, 1, false,true,renderer);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(testTimer > 10)
        {
            walkAnim.setAnimate(false);
            sprintAnim.setAnimate(true);
            sprintAnim.FixedUpdate();
        }
        else if(testTimer > 5)
        {
            walkAnim.setFacingLeft(true);
            walkAnim.FixedUpdate();
        }
        else
        {
            walkAnim.FixedUpdate();
        }
        testTimer += Time.deltaTime;
	}
}
