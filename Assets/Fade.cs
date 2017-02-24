using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    public bool fadeIn; // If false fade out
    public bool fadeInOut;
    public bool fadeOutIn;
    public float fadeTime;
    private float timer;
    private Image image;
	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
        timer = fadeTime;
        if(fadeIn) {
            Color currentColor = image.color;
            currentColor.a = 0;
            image.color = currentColor;
        }
    }

    // Update is called once per frame
    void Update() {
        float delta = Time.deltaTime;

        timer -= delta;
        if(timer > 0) {
            Color currentColor = image.color;
            float newAlpha = currentColor.a;
            if (fadeIn) {
                newAlpha += delta / 2;
            }
            else {
                newAlpha -= delta / 2;
            }
            currentColor.a = newAlpha;
            image.color = currentColor;
        }
        else {
            if(fadeInOut && fadeIn) {
                fadeIn = false;
                timer = fadeTime;
            }
            else if(fadeOutIn && fadeIn == false) {
                fadeIn = true;
                timer = fadeTime;
            }
        }
    }
}
