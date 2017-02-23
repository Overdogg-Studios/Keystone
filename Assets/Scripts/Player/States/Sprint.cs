using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : State {

    private const string name = "Frozen";

	private PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	// Use this for initializatio
	// Update is called once per frame
	public override void Update () {
        player.animator.SetInteger("State", 1);
        player.currentSprintMultiplier = player.sprintMultiplier;
        player.move();
        player.jump();
	}
    public override string ToString() {
        return name;
    }

}
