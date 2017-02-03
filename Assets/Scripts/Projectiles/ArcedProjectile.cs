using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ArcedProjectile : MonoBehaviour
{

    private PlayerController allen;
    //private Vector3 destination;
    //private float airTime;
    private bool fired;
    //private float currentTime;
    private float timeExisted;
    
    void Start()
    {
        //currentTime = 0.0f;
    }

    void FixedUpdate()
    {
        //Vector3 throwSpeed = calculateBestThrowSpeed(transform.position, destination, airTime);
        // GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.VelocityChange);
        transform.position += new Vector3(0.07f, 0.01f - (timeExisted/100), 0);
        timeExisted += Time.deltaTime;
        if(timeExisted >= 20)
        {
            Destroy(this.gameObject);
        }
    }

    public void fire(float airTime, Vector3 destination)
    {
        //this.airTime = airTime;
        //this.destination = destination;
    }
    //Note Grabbed off of http://answers.unity3d.com/questions/248788/calculating-ball-trajectory-in-full-3d-world.html
    private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
    {
        // calculate vectors
        Vector3 toTarget = target - origin;
        Vector3 toTargetXZ = toTarget;
        toTargetXZ.y = 0;

        // calculate xz and y
        float y = toTarget.y;
        float xz = toTargetXZ.magnitude;

        // calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
        // where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
        // so xz = v0xz * t => v0xz = xz / t
        // and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
        float t = timeToTarget;
        float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
        float v0xz = xz / t;

        // create result vector for calculated starting speeds
        Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
        result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
        result.y = v0y;                                // set y to v0y (starting speed of y plane)

        return result;
    }
}
