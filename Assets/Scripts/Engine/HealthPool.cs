using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPool : MonoBehaviour {

	private const float FLICKER_TIME = 0.1f;
	public float currentFlickerTime = 0;
	public Color flickerColor;
	public Color normalColor;
	public int maxHealth = 100;
	public int currentHealth = 0;
	private SpriteRenderer sprite;
	private Collider2D collider;
	// Use this for initialization
	void Start () {

		flickerColor = new Color(1f, 0.5f, 0.5f, 1f);
		normalColor = new Color(1f, 1f, 1f, 1f);
		currentHealth = maxHealth;
		sprite = GetComponent<SpriteRenderer>();
		collider = GetComponent<Collider2D>();

	}
	
	// Update is called once per frame
	void Update () {

		if(isDead()) {
			die();
		}
		flicker();
	}

	public void takeDamage(int dmg) {
		currentHealth -= dmg;
		currentFlickerTime = FLICKER_TIME;
		if(currentHealth < 0) {
			currentHealth = 0;
		}
		
	}

	public bool isDead() {
		if(currentHealth == 0) {
			return true;
		}
		return false;
	}

	void die() {
		sprite.enabled = false;
		collider.enabled = false;
	}
	public void flicker() {

		if(currentFlickerTime > 0) {
			currentFlickerTime -= Time.deltaTime;
			sprite.color = flickerColor;
		}
		else {
			sprite.color = normalColor;
		}
	}
}
