using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShieldScript : MonoBehaviour {


    public float translate;
	// Use this for initialization
	void Start () {
        translate = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        GetComponent<Renderer>().material.SetVector("_Translation", new Vector3(translate,translate,0));
        translate += 0.005f;
        if(translate > 1) {
            translate = 0;
        }
    }
}
