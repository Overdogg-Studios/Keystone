using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour {

	public float startDelay;
	public float timeInterval;
	public int travelTime;
	public float currentTimeInterval;
	public int xDirection;
	public int yDirection;
	public float xVelocity;
	public float yVelocity; 
	
	public string projectileName;

	private Vector3 projectileLocation;
	
	// Use this for initialization
	void Start () {
		projectileLocation= new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentTimeInterval -= Time.deltaTime;
		createProjectile();

		if(startDelay >= 0) {
			startDelay -= Time.deltaTime;
		}
	}
	public void createProjectile() {

		if(currentTimeInterval <= 0 && startDelay <= 0)  {

			
			GameObject go = (GameObject)Instantiate(Resources.Load(projectileName), projectileLocation, Quaternion.Euler(0, 180, 0));
			Projectile projectile_script = go.GetComponent<Projectile>();
			projectile_script.make(travelTime, xDirection, yDirection, xVelocity, yVelocity);
			currentTimeInterval = timeInterval;
		}
	}
}
