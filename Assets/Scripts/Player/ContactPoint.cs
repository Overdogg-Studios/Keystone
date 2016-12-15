using UnityEngine;
using System.Collections;

public class ContactPoint : MonoBehaviour {

	public const float radius = 0.01f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDrawGizmos () {
		Gizmos.color = new Color(0.5F, 1F, 0.5F, 1F);
        Gizmos.DrawWireSphere(transform.position, radius);
	}
}
