using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State {

    public const string name = "Run";

	private PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	public override void Update () {

        player.animator.SetInteger("State", 4);
        player.currentSprintMultiplier = 1;
		player.move();
        player.jump();
        
	}
    public override string ToString() {
        return name;
    } 
}