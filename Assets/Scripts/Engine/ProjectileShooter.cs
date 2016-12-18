using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour {


	public float startDelay;
	public float timeInterval;
	public float travelTime;
	public float currentTimeInterval;
	public float xDirection;
	public float yDirection;
	public float xVelocity;
	public float yVelocity; 
	public float xOffset; //How far away from the projectile shooter that the projectiles spawn (left/right).
	public float yOffset; //How far away from the projectile shooter that the projectiles spawn (up/down).

	public string projectileName;
	public bool autoSpawnProjectiles;

	private Vector3 projectileLocation;
	// Use this for initialization
	void Start () {
		projectileLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(autoSpawnProjectiles) {

			currentTimeInterval -= Time.deltaTime;
		

			if(startDelay >= 0) {
				startDelay -= Time.deltaTime;
			}
			if(currentTimeInterval <= 0 && startDelay <= 0) {
				createProjectile();
			}
		}
		else {
			projectileLocation = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z);
		}
	}
	public void createProjectile() {

		GameObject go = (GameObject)Instantiate(Resources.Load(projectileName), projectileLocation, Quaternion.Euler(0, 180, 0));
		Projectile proj = go.GetComponent<Projectile>();
		proj.make(travelTime, xDirection, yDirection, xVelocity, yVelocity);
		if(xDirection >= 0) {
			proj.GetComponent<SpriteRenderer>().flipX = true;
		}
		else {
			proj.GetComponent<SpriteRenderer>().flipX = false;
		}
		currentTimeInterval = timeInterval;
	}
}
