using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    // Drag
    public GameObject wallPrefab;
    Vector2 dragStart;
    Vector2 dragEnd;

    Vector3 mousePosInWorldCoords;

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
        mousePosInWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragStart = new Vector3(mousePosInWorldCoords.x,mousePosInWorldCoords.y,0);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        if (hit.collider == null)
        {
            Instantiate(wallPrefab, dragStart, Quaternion.identity);
        }
        else { Debug.Log(hit.collider.name); }
    }
}
