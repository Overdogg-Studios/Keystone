using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * State that the player enters when dead, in a cutscene, or unable to move for any reason.
 * @type State
 */
public class Frozen : State {

	private const string name = "Frozen";
	private PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	// Use this for initializatio
	// Update is called once per frame
	public override void Update () {
		changeState();
	}
	public override string ToString() {
        return name;
    }
    public override void changeState() {

    	if(!player.healthPool.isDead()) {
    		player.currentState = new Default();
    	}
    }
}