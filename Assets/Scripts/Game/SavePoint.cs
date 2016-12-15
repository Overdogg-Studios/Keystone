using UnityEngine;
using System.Collections;

public class SavePoint : MonoBehaviour {

	public float xPosition;
	public float yPosition;

	private PlayerController  player;
	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		xPosition = transform.position.x;
		yPosition = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		
		if(other.gameObject.tag == "Player") {

			player.setSavePoint(GetComponent<SavePoint>());
		}
	}
	public Vector3 getPosition() {
		return new Vector3(xPosition, yPosition, -1);
	}
}
