using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Animation : MonoBehaviour {

    private Sprite[] sprites;
    public Texture2D texture;
    public float timeToDisplayFrame;

    private SpriteRenderer renderer;
    private int frameCounter;
    private float timer = 0.0f;



	// Use this for initialization
	void Start ()
    {
        sprites = Resources.LoadAll<Sprite>(texture.name);
        frameCounter = 0;
        renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        timer += Time.deltaTime;
        if(timer >= timeToDisplayFrame)
        {
            frameCounter++;
            if (frameCounter >= sprites.Length)
            {
                frameCounter = 0;
            }
            timer = 0.0f;
        }
        Debug.Log(frameCounter);
        renderer.sprite = sprites[frameCounter];
	}
}
