using UnityEngine;
using System.Collections;

public class ContactBox : MonoBehaviour {

	public float x;
	public float y;
	[HideInInspector]
	public Vector2 position;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		position = transform.position;
	}
	void OnDrawGizmos () {
		Gizmos.color = new Color(0.2F, 1F, 0.2F, 1F);
        Gizmos.DrawWireCube(transform.position, new Vector3(x, y, 1));
	}
}
