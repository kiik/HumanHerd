using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    // Drag
    public GameObject WallPrefab;
    Vector2 dragStart;
    Vector2 dragEnd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MouseInput();
        KeyboardInput();
	}

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseRay();
        }
        else if (Input.GetMouseButtonUp(0))
        {

        }
    }

    void KeyboardInput()
    {

    }

    void MouseRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Debug.Log(Input.mousePosition);
    }
}
