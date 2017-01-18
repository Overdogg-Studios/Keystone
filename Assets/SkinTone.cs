using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinTone : MonoBehaviour {

    // Use this for initialization
    public Color skinColor;
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Renderer>().material.SetColor("_Color", skinColor);
        GetComponent<Renderer>().material.SetTexture("_MainTex", GetComponent<SpriteRenderer>().sprite.texture);
	}
}
