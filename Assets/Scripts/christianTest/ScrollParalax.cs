using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollParalax : MonoBehaviour
{
    //Place component on each scrolling gameobject

    private new Camera camera;
    public float scrollWidth = 16;

    private Vector3 startPosition;
    private Vector3 startCamPosition;

    private GameObject leftBuffer;
    private GameObject rightBuffer;

    private Vector3 offset = Vector3.zero;
    public Vector2 motionScale = Vector2.one;
    
    // Use this for initialization
    void Start()
    {
        camera = Camera.main;
        startCamPosition = camera.transform.position;
        startPosition = transform.position;

        Vector3 pos = transform.position;
        leftBuffer = Instantiate(gameObject, pos + (Vector3.left*scrollWidth) , Quaternion.identity );
        leftBuffer.transform.SetParent(transform);
        leftBuffer.GetComponent<ScrollParalax>().enabled = false;

        rightBuffer = Instantiate(gameObject, pos + (Vector3.right * scrollWidth), Quaternion.identity);
        rightBuffer.transform.SetParent(transform);
        rightBuffer.GetComponent<ScrollParalax>().enabled = false;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        offset = camera.transform.position - startCamPosition;
        offset.x *= motionScale.x;
        offset.y *= motionScale.y;
        
        //Wrap
        if ( Mathf.Abs(offset.x) > scrollWidth && motionScale.x != 1)
        {
            offset.x += scrollWidth * Mathf.Sign(offset.x);
        }

        transform.position = startPosition + offset;
    }
}