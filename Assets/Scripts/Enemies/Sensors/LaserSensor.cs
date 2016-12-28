using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Enemies_Sensors
{
    class LaserSensor : MonoBehaviour
    {
        public int damage;
        public bool isEnemy;
        private PlayerController allen;

        void Start()
        {
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            allen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if(collision.gameObject.tag == "Player" && isEnemy)
            {
                Laser[] l = GetComponentsInChildren<Laser>();
                for(int i = 0; i < l.Length; i++)
                {
                    l[i].damage = damage;
                    l[i].isActivated = true;
                }
            }
        }
    }
}
