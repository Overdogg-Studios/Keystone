using UnityEngine;
using System.Collections;

public class ContactPoint : MonoBehaviour {

	public const float radius = 0.01f;
	public float x;
	public float y;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnDrawGizmos () {
		Gizmos.color = new Color(0.2F, 1F, 0.2F, 1F);
        Gizmos.DrawWireCube(transform.position, new Vector3(x, y, 1));
	}
}
