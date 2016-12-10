using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    // Drag
    public GameObject wallPrefab;
    Vector2 dragStart;
    Vector2 dragEnd;
    bool isDragging = false;
    int maxFit = 0;


    Vector3 mousePosInWorldCoords;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isDragging)
        {
            DrawLineObjects();
        }
        MouseInput();
        KeyboardInput();
	}

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            MouseRay();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Build();
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
            Instantiate(wallPrefab, dragStart, wallPrefab.transform.localRotation);
        }
        else { 
            // TODO check what we hit. 
        }
    }

    void DrawLineObjects()
    {
        mousePosInWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragEnd = new Vector3(mousePosInWorldCoords.x, mousePosInWorldCoords.y, 0);
        Debug.DrawLine(dragStart, dragEnd);
        float totalLength = (dragStart-dragEnd).magnitude;

        int objectsToDraw = (int)(totalLength * 100) / 16;
        int poolCount = PoolManager.instance.GetWoodenWallPoolCount();

        // Add objects to pool if missing
        if (poolCount < objectsToDraw)
        {
            for (int i=0; i< objectsToDraw-poolCount; i++)
            {
                PoolManager.instance.IncreaseWoodenWallPool();
            }
        }


    }

    void Build()
    {

    }
}
