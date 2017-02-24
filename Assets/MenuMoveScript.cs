using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMoveScript : MonoBehaviour {

    private Vector3 end;
    private bool moving;
    private GameObject parent;

	// Use this for initialization
	void Start () {
        moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(moving) {
            while (parent.transform.position != end) {
                parent.transform.position = Vector3.MoveTowards(parent.transform.position, end, 10 * Time.deltaTime);
            }
            if (parent.transform.position == end) {
                moving = false;
            }
        }


    }

    /**
     * Clicked start button in menu
     * @param parent The parent object
     */
    public void ClickedStart(GameObject parent) {
        Debug.Log("Clicked Start");
        end = GameObject.Find("LoadSave").transform.position;
        Debug.Log(end);
        this.parent = parent;
        moving = true;
    }

    /**
     * Click options button in menu
     */
    public void ClickedOptions() {
        this.transform.Translate(-Screen.width, 0, 0);
    }
}
