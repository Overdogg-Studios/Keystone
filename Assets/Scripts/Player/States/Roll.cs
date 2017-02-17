using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : State {

	public const float LEFT = -1.0f;
	public const float RIGHT = 1.0f;

	private PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	// Use this for initializatio
	// Update is called once per frame
	public override void Update () {
		roll();
	}
	public void roll() {

        if(Input.GetKeyDown("space") && (Input.GetKey("left") || Input.GetKey("right")) && player.currentRollTime <= 0 && player.isGrounded() && player.currentRollDelay <= 0) {
            player.currentRollTime = player.rollTime;
            player.currentRollDelay = player.rollDelay;
            player.isRolling = true;
            player.animator.SetInteger("State", 3);

            if(Input.GetKey("left")) {
                player.direction = LEFT;
            }
            if(Input.GetKey("right")) {
                player.direction = RIGHT;
            }
        }
        else if(player.isRolling && player.direction != 0) {

            player.speed = player.currentRollTime * player.direction * player.rollSpeedMultiplier;

            if(player.isTouchingLeftWall() && player.direction == LEFT) {
                player.speed = 0;
            }
            if(player.isTouchingRightWall() && player.direction == RIGHT) {
                player.speed = 0;
            }

            player.transform.position = new Vector3(player.transform.position.x + player.speed, player.transform.position.y, -1); 

            player.currentRollTime -= Time.deltaTime;
            if(player.currentRollTime <= 0) {
                player.isRolling = false;
            }
        }
        else {
            player.currentRollDelay -= Time.deltaTime;
        }
    }
}