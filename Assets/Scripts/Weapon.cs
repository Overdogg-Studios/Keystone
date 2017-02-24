using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float damage = 0;
    public float detonatesAtTime = -1; // if -1 does not detonate
    public float isDeletedAtTime = -1; // if -1 does not get deleted
    public float timeAlive = -1; //Time projectile is alive
    public WeaponPathType pathType;
    public Direction direction;
    public string projectileName;
    public Vector3 firingOffset;
    public Vector2 projectileSpeed;
    public Vector3 projectileDestination;
    public GameObject masterProjectile;

    private bool fired;
	// Use this for initialization
	void Start () {
        fired = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        switch (pathType) {
            case WeaponPathType.ARCED_DUE_TO_GRAVITY:
                if (direction == Direction.LEFT) {
                    if(fired) {
                        //Instantiate(masterProjectile,transform.position + firingOffset,Quaternion.Euler(0,180,0));
                        (((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<GravityProjectile>()).fire(timeAlive, projectileDestination);
                    }
                }
                else if (direction == Direction.RIGHT) {
                    if (fired) {
                        (((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<GravityProjectile>()).fire(timeAlive, projectileDestination);
                    }
                }
                else if (direction == Direction.UP) {
                    if (fired) {
                        (((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<GravityProjectile>()).fire(timeAlive, projectileDestination);
                    }
                }
                else if (direction == Direction.DOWN) {
                    if (fired) {
                        (((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<GravityProjectile>()).fire(timeAlive, projectileDestination);
                    }
                }
                else if (direction == Direction.UP_LEFT) {
                    if (fired) {
                        (((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<GravityProjectile>()).fire(timeAlive, projectileDestination);
                    }
                }
                else if (direction == Direction.UP_RIGHT) {
                    if (fired) {
                        (((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<GravityProjectile>()).fire(timeAlive, projectileDestination);
                    }
                }
                else if (direction == Direction.DOWN_LEFT) {
                    if (fired) {
                        (((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<GravityProjectile>()).fire(timeAlive, projectileDestination);
                    }
                }
                else if (direction == Direction.DOWN_RIGHT) {
                    if (fired) {
                        (((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<GravityProjectile>()).fire(timeAlive, projectileDestination);
                    }
                }
                break;
            case WeaponPathType.ARCED_OTHER:
                ArcedProjectile proj = ((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<ArcedProjectile>();
                proj.fire(timeAlive, projectileDestination);
                break;
            default:
                if (direction == Direction.LEFT) {
                    if (fired) {
                        Projectile proj0 = ((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<Projectile>();
                        proj0.make(timeAlive, -1, 0, projectileSpeed.x, projectileSpeed.y);
                        proj0.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else if (direction == Direction.RIGHT) {
                    if (fired) {
                        Projectile proj1 = ((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<Projectile>();
                        proj1.make(timeAlive, 1, 0, projectileSpeed.x, projectileSpeed.y);
                        proj1.GetComponent<SpriteRenderer>().flipX = true;
                    }
                }
                else if (direction == Direction.UP) {
                    if (fired) {
                        Projectile proj2 = ((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<Projectile>();
                        proj2.make(timeAlive, 0, 1, projectileSpeed.x, projectileSpeed.y);
                        proj2.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else if (direction == Direction.DOWN) {
                    if (fired) {
                        Projectile proj3 = ((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<Projectile>();
                        proj3.make(timeAlive, 0, -1, projectileSpeed.x, projectileSpeed.y);
                        proj3.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else if (direction == Direction.UP_LEFT) {
                    if (fired) {
                        Projectile proj4 = ((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<Projectile>();
                        proj4.make(timeAlive, -1, 1, projectileSpeed.x, projectileSpeed.y);
                        proj4.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else if (direction == Direction.UP_RIGHT) {
                    if (fired) {
                        Projectile proj5 = ((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<Projectile>();
                        proj5.make(timeAlive, 1, 1, projectileSpeed.x, projectileSpeed.y);
                        proj5.GetComponent<SpriteRenderer>().flipX = true;
                    }
                }
                else if (direction == Direction.DOWN_LEFT) {
                    if (fired) {
                        Projectile proj6 = ((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<Projectile>();
                        proj6.make(timeAlive, -1, -1, projectileSpeed.x, projectileSpeed.y);
                        proj6.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else if (direction == Direction.DOWN_RIGHT) {
                    if (fired) {
                        Projectile proj7 = ((GameObject)Instantiate(Resources.Load(projectileName), transform.position + firingOffset, Quaternion.Euler(0, 180, 0))).GetComponent<Projectile>();
                        proj7.make(timeAlive, 1, -1, projectileSpeed.x, projectileSpeed.y);
                        proj7.GetComponent<SpriteRenderer>().flipX = true;
                    }
                }
                break;
        }

        if (fired) {
            fired = false;
        }
    }

    /**
     * Fires a projectile out of the weapon
     */
    public void fire() {
        fired = true;
    }
}
