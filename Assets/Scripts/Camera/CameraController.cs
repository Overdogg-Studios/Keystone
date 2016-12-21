using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {     

	private float rightBound;
	private float leftBound;
	private float topBound;
	private float bottomBound;
	private float xBound;
	private float yBound;
	public float vertExtent;
	public float horzExtent;
	public Vector3 pos;
	private Transform target;
	private BoxCollider2D levelBounds;

	// Use this for initialization
	void Start () 
	{
		
	 float vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
	 float horzExtent = vertExtent * Screen.width / Screen.height;

	 levelBounds = GameObject.Find("Background").GetComponentInChildren<BoxCollider2D>();

	 target = GameObject.Find("Player").transform;

	 xBound = levelBounds.bounds.size.x;
	 yBound = levelBounds.bounds.size.y;

	 leftBound = (float)(horzExtent - xBound / 2.0f);
	 rightBound = (float)(xBound / 2.0f - horzExtent);
	 bottomBound = (float)(vertExtent - yBound / 2.0f);
	 topBound = (float)(yBound  / 2.0f - vertExtent);
	}

	void Update () 
	{

		//Debug.Log(leftBound);
		var pos = new Vector3(target.position.x, target.position.y, transform.position.z);

		pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
		pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
		transform.position = pos;


        Vector3 toadd = new Vector3(pos.x / 3, pos.y / 3, 1);
        GameObject.Find("Ghost").transform.position = toadd;
    }
}