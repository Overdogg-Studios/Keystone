using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{

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
                isActivated = false;
                GetComponent<Hazard>().isActive = false;
                timer = 0.0f;
            }
        }
        else
        {
            if (timer > timeInterval && isActivated)
            {
                isPoweredOn = true;
                GetComponent<Hazard>().isActive = true;
                timer = 0.0f;
            }
            else
            {
                isPoweredOn = false;
                GetComponent<Hazard>().isActive = false;
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
