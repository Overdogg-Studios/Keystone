/**
 * Controls the external functionality of the game; Menus, quitting, respawning, restarting, etc. 
 */

using UnityEngine;  
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	GameObject deathScreen;
	private PlayerController  player;
	public CameraController mainCamera;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		deathScreen = (GameObject)Instantiate(Resources.Load("DeathScreen"), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Input.GetKey("escape")) {
			Application.Quit();
		}
		checkForDeadPlayer();
	}

	void checkForDeadPlayer() {

		if(player.isDead) {

			//Spawn a DeathScreen object in the exact center of the screen. 
			mainCamera = GameObject.Find("Camera").GetComponent<CameraController>();
			Vector3 centerScreen = mainCamera.transform.position;
			centerScreen.z = -3;
			deathScreen.transform.position = centerScreen;
			deathScreen.GetComponent<Renderer>().enabled = true;
		}
		else {
			deathScreen.GetComponent<Renderer>().enabled = false;
		}
	}

}
