using UnityEngine;

public class PlayerMouseControls : MonoBehaviour {

    // Drag
    public GameObject wallPrefab;
    Vector2 dragStart;
    Vector2 dragEnd;
    Vector2 completeDragLine;
    bool isDragging = false;
    int buildingLayer;
    int buildingArtLayer;
    int controlLayer;

    int totalCurrency;

    UIManager uiMan;

    Vector3 mousePosInWorldCoords;

    // Text colors
    Color32 enoughMoney = new Color32(45, 125, 60,255);
    Color32 notEnoughMoney = new Color32(204, 44, 44, 255);

    // Use this for initialization
    void Start () {
        uiMan = GameManager.instance.uiManager;
        buildingLayer = LayerMask.NameToLayer("Building");
        buildingArtLayer = LayerMask.NameToLayer("BuildingArt");
        controlLayer = buildingLayer;
    }
	
	// Update is called once per frame
	void Update () {
        if (uiMan.menuState == UIManager.MenuState.GameMenu || uiMan.menuState == UIManager.MenuState.Tutorial) { return; }
        if (isDragging)
        {
            DrawLineObjects();
        }
        MouseInput();
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
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            if (hit.collider != null)
            {
                Wall wall = hit.collider.GetComponent<Wall>();
                if (wall != null)
                {
                    wall.Hit(50);
                }
            }
        }
    }

    void MouseRay()
    {
        totalCurrency = GameManager.instance.ecoManager.GetCurrency();
        Debug.Log("total: " + totalCurrency);
        controlLayer = buildingLayer;
        mousePosInWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragStart = new Vector3(mousePosInWorldCoords.x,mousePosInWorldCoords.y,0);
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 1 << controlLayer);

        Debug.Log(controlLayer);

        if (hits.Length <= 0)
        {
            controlLayer = buildingArtLayer;
            isDragging = true;
        }
        else {
            Collider2D col = null;
            foreach (RaycastHit2D hit in hits)
            {
                if (col == null) { col = hit.collider; }
                if (col.transform.parent.position.y > hit.collider.transform.parent.position.y)
                {
                    col = hit.collider;
                }
            }
            if (col != null)
            {
                controlLayer = buildingArtLayer;
                dragStart = col.transform.parent.position;
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
        int totalCost = 0;
        for (int i = 0; i < objectsToDraw; i++)
        {
            if (i == 0) { PoolManager.instance.SetWoodenWallPos(i, dragStart); }
            totalCost += PoolManager.instance.SetWoodenWallPos(i, ((completeDragLine / totalLength) * 0.16f * i)+dragStart);
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
        if (Physics2D.Linecast(((completeDragLine / totalLength) * 0.16f * 1) + dragStart, dragEnd, 1 << controlLayer))
        {
            PoolManager.instance.ProhibitBuild();
            return;
        }

        if (totalCost > GameManager.instance.ecoManager.GetCurrency())
        {
            PoolManager.instance.ProhibitBuild();
            GameManager.instance.uiManager.SetMouseText("No money", notEnoughMoney);
            return;
        }
        
        GameManager.instance.uiManager.SetMouseText("Cost: " + totalCost, enoughMoney);
    }

    void Build()
    {
        int totalCost = PoolManager.instance.BuildWoodenWall();
        GameManager.instance.ecoManager.DecreaseCurrency(totalCost);
        GameManager.instance.uiManager.SetMoneyText(totalCurrency - totalCost);
        GameManager.instance.uiManager.HideMouseText();
        totalCurrency = 0;
    }
}
