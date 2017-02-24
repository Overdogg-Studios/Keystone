using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public float delayTime = 5;
    public string nextSceneName;
    private float timer;

	// Use this for initialization
	void Start () {
        timer = delayTime;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if(timer > 0) {
            return;
        }
        else {
            SceneManager.LoadScene(nextSceneName);
        }
	}
}
