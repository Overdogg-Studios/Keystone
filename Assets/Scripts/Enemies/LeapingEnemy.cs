using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapingEnemy : MonoBehaviour {

	public Entity entity;
	public HealthPool healthPool;
	public PlayerController player;

	public float direction;
	public float leapTime = 0.5f;
	public float leapPause = 3.0f;
	public float currentLeapTime = 0;
	public float currentLeapPause = 0;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		entity = GetComponent<Entity>();
		healthPool = GetComponent<HealthPool>();
	}
	
	// Update is called once per frame
	void Update () {
		Leap();
	}

	void Leap () {
		if(player.transform.position.x < transform.position.x) {
			direction = Constant.LEFT;
		}
		else {
			direction = Constant.RIGHT;
		}
		if(currentLeapPause < 0) {

			if(currentLeapTime > 0) {
				direction = 0;	
				currentLeapTime -= Time.deltaTime;
				currentLeapPause = leapPause;
			}
		}
		else {
			direction = 0;
			currentLeapPause -= Time.deltaTime;
			currentLeapTime = leapTime;
		}
		
		entity.move(direction);

	}
}
