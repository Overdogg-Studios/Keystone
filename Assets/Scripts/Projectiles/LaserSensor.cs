using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class LaserSensor : MonoBehaviour
{
    private Sensor sensor;
    private Laser[] l;

    void Start()
    {
        l = GetComponentsInChildren<Laser>();
    }
    void Update() {
        sensor = GetComponent<Sensor>();

        if(sensor.playerDetected)
        {
            
            for(int i = 0; i < l.Length; i++)
            { 
                l[i].isActivated = true;
            }
        }

    }
}
