using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {

	/**
	 * Called by the player or AI controller. Pilots game logic and all player or AI controller functions.
	 */
	public abstract void Update();
	
	/**
	 * Controls when and to which states the current state can transition too.
	 */
	public abstract void changeState();
	/**
	 * Returns name of the state in UpperCamelCase.
	 */
	public abstract override string ToString();
}