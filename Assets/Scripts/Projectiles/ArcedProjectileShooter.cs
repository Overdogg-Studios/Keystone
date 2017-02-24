using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ArcedProjectileShooter : MonoBehaviour {
    public bool isEnemy;
    public string arrowName;
    public float airTime;
    public float timeBetweenShots;
    private PlayerController allen;
    private Sensor sensor;
    private float timer;

    void Start() {
        timer = 0.0f;
        allen = GameObject.Find("Player").GetComponent<PlayerController>();
        sensor = GetComponent<Sensor>();

    }
    void Update() {
        if(sensor.playerDetected) {
            if (timer >= timeBetweenShots) {
                Debug.Log("Found Enemy!");
                GameObject go = (GameObject)Instantiate(Resources.Load(arrowName), transform.position, Quaternion.Euler(0, 180, 0));
                ArcedProjectile proj = go.GetComponent<ArcedProjectile>();
                proj.fire(airTime, allen.transform.position);
                timer = 0.0f;
            }
            timer += Time.deltaTime;
        }
    } 
}