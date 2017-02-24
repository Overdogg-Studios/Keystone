using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyColorsToShader : MonoBehaviour
{

    // Use this for initialization
    public Color skinColor;
    public Color bootColor;
    public Color hatColor;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        GetComponent<Renderer>().material.SetColor("_Color", skinColor);
        GetComponent<Renderer>().material.SetColor("_Boots", bootColor);
        GetComponent<Renderer>().material.SetColor("_Hat", hatColor);
        GetComponent<Renderer>().material.SetTexture("_MainTex", GetComponent<SpriteRenderer>().sprite.texture);
    }
}