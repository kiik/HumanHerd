using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    // Drag
    public GameObject wallPrefab;
    Vector2 dragStart;
    Vector2 dragEnd;
    Vector2 completeDragLine;
    bool isDragging = false;
    int maxFit = 0;
    int buildingLayer;
    int buildingArtLayer;
    int controlLayer;


    Vector3 mousePosInWorldCoords;

	// Use this for initialization
	void Start () {
        buildingLayer = LayerMask.NameToLayer("Building");
        buildingArtLayer = LayerMask.NameToLayer("BuildingArt");
        controlLayer = buildingLayer;
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
        controlLayer = buildingLayer;
        mousePosInWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragStart = new Vector3(mousePosInWorldCoords.x,mousePosInWorldCoords.y,0);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 1 << controlLayer);
        
        if (hit.collider == null)
        {
            controlLayer = buildingArtLayer;
            Debug.Log("TRUE");
            isDragging = true;
        }
        else {
            Debug.Log(hit.collider.name);
            if (hit.collider.GetComponent<Wall>() != null)
            {
                controlLayer = buildingArtLayer;
                dragStart = hit.collider.transform.parent.position;
                isDragging = true;
            }
        }
    }

    void DrawLineObjects()
    {
        mousePosInWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragEnd = new Vector3(mousePosInWorldCoords.x, mousePosInWorldCoords.y, 0);
        //Debug.DrawLine(dragStart, dragEnd);

        completeDragLine = dragEnd - dragStart;
        float totalLength = (completeDragLine).magnitude;
        int objectsToDraw = (int)(totalLength * 100) / 16 + 1;
        int poolCount = PoolManager.instance.GetWoodenWallPoolCount();

        // Add objects to pool if needed
        if (poolCount < objectsToDraw)
        {
            for (int i=0; i< objectsToDraw-poolCount; i++)
            {
                PoolManager.instance.IncreaseWoodenWallPool();
            }
        }
        
        // Activate required objects
        for (int i = 0; i < objectsToDraw; i++)
        {
            if (i == 0) { PoolManager.instance.SetWoodenWallPos(i, dragStart); }
            PoolManager.instance.SetWoodenWallPos(i, ((completeDragLine / totalLength) * 0.16f * i)+dragStart);
        }
        // Disable objects if necessary
        int objectsToDisable = poolCount - objectsToDraw;
        poolCount = PoolManager.instance.GetWoodenWallPoolCount();
        if (objectsToDisable > 0)
        {
            for (int i = 0; i < objectsToDisable; i++)
            {
                PoolManager.instance.CheckWoodenWallDisable((poolCount-objectsToDisable)+i);
            }
        }

        Debug.DrawLine(((completeDragLine / totalLength) * 0.16f * 1) + dragStart, dragEnd);
        Debug.Log(LayerMask.LayerToName(controlLayer) + ", " + controlLayer);
        if (Physics2D.Linecast(((completeDragLine / totalLength) * 0.16f * 1) + dragStart, dragEnd, 1 << controlLayer))
        {
            PoolManager.instance.ProhibitBuild();
        }
    }

    void Build()
    {
        PoolManager.instance.BuildWoodenWall();
    }
}
