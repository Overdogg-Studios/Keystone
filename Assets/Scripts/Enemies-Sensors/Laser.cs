using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies_Sensors
{
    public class Laser : MonoBehaviour
    {

        public int damage;
        public bool isEnemy; //Determines if the projectile will hurt the player, or hurt enemies.
        private PlayerController allen;
        public bool isActivated;
        private bool isPoweredOn;
        public float timeInterval;
        private float timer;
        private float onTimer;
        public float onTimeLength;
        public Sprite LaserOff;
        public Sprite LaserOn;

        void Start()
        {
            isPoweredOn = false;
            timer = 0.0f;
            onTimer = 0;
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            allen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (other.gameObject.tag == "Player" && isEnemy && isActivated && isPoweredOn)
            {
                allen.GetComponent<HealthPool>().takeDamage(damage);
            }

        }
        void FixedUpdate()
        {
            timer += Time.deltaTime;

            if(onTimer != 0 || isPoweredOn)
            {
                onTimer += Time.deltaTime;
                if(onTimer > onTimeLength)
                {
                    onTimer = 0.0f;
                    isPoweredOn = false;
                    timer = 0.0f;
                }
            }
            else
            {
                if (timer > timeInterval && isActivated)
                {
                    isPoweredOn = true;
                    timer = 0.0f;
                }
                else
                {
                    isPoweredOn = false;
                }
                if (!isPoweredOn)
                {
                    GetComponent<SpriteRenderer>().sprite = LaserOff;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = LaserOn;
                }
            }
        }

    }
}
