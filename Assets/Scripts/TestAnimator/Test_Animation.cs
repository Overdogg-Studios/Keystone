using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Animation {

    private Sprite[] sprites;
    public Texture2D texture;
    public float timeToDisplayFrame;
    public bool faceLeft;
    public bool animate;

    private SpriteRenderer renderer;
    private int frameCounter;
    private float timer = 0.0f;

    public Test_Animation(Texture2D texture, float timeToDisplayFrame, bool faceLeft, bool animate, SpriteRenderer renderer)
    {
        this.texture = texture;
        this.timeToDisplayFrame = timeToDisplayFrame;
        this.faceLeft = faceLeft;
        this.animate = animate;


        sprites = Resources.LoadAll<Sprite>(texture.name);
        //Debug.Log(sprites.Length);
        frameCounter = 0;
        //renderer = GetComponent<SpriteRenderer>();
        this.renderer = renderer;
    }

	// Use this for initialization
	void Start ()
    {
        //sprites = Resources.LoadAll<Sprite>(texture.name);
        //frameCounter = 0;
        //renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	public void FixedUpdate ()
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
        //Debug.Log(frameCounter);
        renderer.sprite = sprites[frameCounter];
        if(faceLeft)
        {
            renderer.flipX = true;
        }
        else
        {
            renderer.flipX = false;
        }
	}

    public void setAnimate(bool animate)
    {
        this.animate = animate;
    }

    public void setFacingLeft(bool left)
    {
        this.faceLeft = left;
    }
}
