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
	public bool isPaused;
	// Use this for initialization
	void Start () {
		isPaused = false;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		deathScreen = (GameObject)Instantiate(Resources.Load("DeathScreen"), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
		
		quit();
		pause();
		checkForDeadPlayer();
	}

	void checkForDeadPlayer() {

		if(player.healthPool.isDead()) {

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
	void quit() {
		if (Input.GetKeyDown("escape")) {
			Application.Quit();
		}
	}
	void pause() {

		if (Input.GetKeyDown("p")) {

			if(isPaused == false) {
				Time.timeScale = 0;
				isPaused = true;
			}
			else {
				Time.timeScale = 1;
				isPaused = false;
			}

		}
	}
}
