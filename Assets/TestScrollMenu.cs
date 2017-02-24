using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrollMenu : MonoBehaviour {


    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponentInParent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /**
     * Switches to options menu
     */
    public void OptionsMenu() {
        animator.SetInteger("State", 1);
    }

    /**
     * Switches to main menu
     */
    public void MainMenu() {
        animator.SetInteger("State", 2);
    }
}
