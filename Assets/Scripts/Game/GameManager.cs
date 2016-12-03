/**
 * Controls the external functionality of the game; Menus, quitting, respawning, restarting, etc. 
 */

using UnityEngine;  
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey("escape")) {
			Application.Quit();
		}
	}
}
