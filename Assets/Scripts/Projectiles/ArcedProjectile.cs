using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ArcedProjectile : MonoBehaviour {

    private PlayerController allen;
    private Vector3 destination;
    private float airTime;
    private bool fired;
    private float currentTime;
    private float timeExisted;
    
    void Start() {
        currentTime = 0.0f;
    }

    void FixedUpdate() {
        //Vector3 throwSpeed = calculateBestThrowSpeed(transform.position, destination, airTime);
        // GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.VelocityChange);
        transform.position += new Vector3(0.07f, 0.01f - (timeExisted/100), 0);
        timeExisted += Time.deltaTime;
        if(timeExisted >= 20) {
            Destroy(this.gameObject);
        }
    }

    /**
     * Fires a projectile to a specific destination in a certain amount of time
     * @param airTime The time it takes for the projectile to reach the destination
     * @param destination The destination of the projectile
     */
    public void fire(float airTime, Vector3 destination) {
        this.airTime = airTime;
        this.destination = destination;
    }
}
