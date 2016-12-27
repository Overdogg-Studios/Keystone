using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Enemies_Sensors
{
    class ArrowLauncher : MonoBehaviour
    {
        public bool isEnemy;
        public string arrowName;
        public float airTime;
        public int damage;
        public float timeBetweenShots;
        private PlayerController allen;
        private float timer;

        void Start()
        {
            timer = 0.0f;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (timer >= timeBetweenShots)
            {
                allen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                if (collision.gameObject.tag == "Player" && isEnemy)
                {
                    Debug.Log("Found Enemy!");
                    GameObject go = (GameObject)Instantiate(Resources.Load(arrowName), transform.position, Quaternion.Euler(0, 180, 0));
                    Arrow arrow = go.GetComponent<Arrow>();
                    arrow.fire(airTime, allen.transform.position, damage);
                    timer = 0.0f;
                }
            }
            timer += Time.deltaTime;
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (timer >= timeBetweenShots)
            {
                allen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                if (collision.gameObject.tag == "Player" && isEnemy)
                {
                    Debug.Log("Found Enemy!");
                    GameObject go = (GameObject)Instantiate(Resources.Load(arrowName), transform.position, Quaternion.Euler(0, 180, 0));
                    Arrow arrow = go.GetComponent<Arrow>();
                    arrow.fire(airTime, allen.transform.position, damage);
                    timer = 0.0f;
                }
            }
            timer += Time.deltaTime;
        }
    }
}
