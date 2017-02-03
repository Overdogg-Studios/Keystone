using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableEnemy : MonoBehaviour
{

    private int state = 0;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
            float translateSpeed = Random.Range(-2, 2);
            this.transform.Translate(translateSpeed, 0,0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = 1;
        }
    }
}