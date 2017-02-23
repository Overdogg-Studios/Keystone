using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : State {

	private const string name = "Frozen";
	private PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	// Use this for initializatio
	// Update is called once per frame
	public override void Update () {
		
	}
	public override string ToString() {
        return name;
    }
}