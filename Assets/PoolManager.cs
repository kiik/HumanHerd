using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Pathfinding;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance = null;

    [SerializeField]
    GameObject StoneWall;
    [SerializeField]
    GameObject StoneWallBuild;
    [SerializeField]
    GameObject wallParent;
    [SerializeField]
    GameObject WoodenWall;
    [SerializeField]
    GameObject WoodenWallBuild;
    public int woodenWallPoolCount = 0;
    public List<GameObject> wallPool = new List<GameObject>();

    public GameObject wall;
    public GameObject wallBuild;

    void Awake()
    {
        if (!instance) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }

    void Start()
    {
        wall = WoodenWall;
        wallBuild = WoodenWallBuild;
        InitPools();
    }

    void InitPools()
    {
        for (int i = 0; i < woodenWallPoolCount; i++)
        {
            GameObject go = Instantiate(wall);
            wallPool.Add(go);
            go.transform.SetParent(wallParent.transform);
            wall.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    public void IncreaseWoodenWallPool()
    {
        GameObject go = Instantiate(wall);
        wallPool.Add(go);
        go.transform.SetParent(wallParent.transform);
        if (wall.GetComponent<Relay>() == null) { return; }
        wall.GetComponent<Relay>().spriteRenderer.enabled = false;
    }

    public int GetWoodenWallPoolCount()
    {
        return wallPool.Count;
    }

    public void ResetWoodenWallPool()
    {
        foreach (GameObject go in wallPool)
        {
            Relay relay = go.GetComponent<Relay>();
            relay.buildingMaterial.SetBMInactive();
            relay.buildingMaterial.Clear();
            relay.buildingMaterial.DisableSpriteRenderer();
        }
    }

    public int SetWoodenWallPos(int index, Vector2 pos)
    {
        if (float.IsNaN(pos.x)) { return 0; }
        
        GameObject buildWall = wallPool[index];
        buildWall.transform.position = pos;
        Relay relay = buildWall.GetComponent<Relay>();
        if (relay == null) { return 0; }
        relay.buildingMaterial.EnableSpriteRenderer();
        relay.buildingMaterial.Clear();
        relay.buildingMaterial.SetBMActive();

        return relay.buildingMaterial.cost;
    }

    public void CheckWoodenWallDisable(int index)
    {
        GameObject go = wallPool[index];
        Relay relay = go.GetComponent<Relay>();
        if (relay == null) { return; }
        if (relay.buildingMaterial.IsActive())
        {
            go.transform.localPosition = Vector2.zero;
            relay.buildingMaterial.DisableSpriteRenderer();
            relay.buildingMaterial.SetBMInactive();

        }
    }

    public int BuildWoodenWall()
    {
        GameObject g1 = null;
        GameObject g2 = null;

        int totalCost = 0;

        foreach (GameObject go in wallPool)
        {
            if (go.GetComponent<Relay>() == null) { return 0; }
            if (!(go.GetComponent<Relay>().buildingMaterial.IsActive() && !go.GetComponent<Relay>().buildingMaterial.IsObstructed())) { continue; }
            GameObject g = Instantiate(wallBuild, go.transform.position, go.transform.rotation);
            totalCost += go.GetComponent<Relay>().buildingMaterial.cost;
            if (g1 == null)
            {
                g1 = g;
            }
            else { g2 = g; }
        }

        GraphUpdateObject guo = null;

        if ((g1 != null) && (g2 != null) ) {
            Bounds b1 = g1.GetComponentInChildren<SpriteRenderer>().GetComponent<BoxCollider2D>().bounds,
            b2 = g2.GetComponentInChildren<SpriteRenderer>().GetComponent<BoxCollider2D>().bounds;

            Vector3 min = new Vector3(Mathf.Min(b1.min.x, b2.min.x), Mathf.Min(b1.min.y, b2.min.y), 0);
            Vector3 max = new Vector3(Mathf.Max(b1.max.x, b2.max.x), Mathf.Max(b1.max.y, b2.max.y), 0);

            Bounds b = new Bounds(min + (max - min) * 0.5f, max - min);

            guo = new GraphUpdateObject(b);
            AstarPath.active.UpdateGraphs(guo);
        }

        ResetWoodenWallPool();

        return totalCost;
    }

    public void ProhibitBuild()
    {
        foreach (GameObject go in wallPool)
        {
            Relay relay = go.GetComponent<Relay>();
            if (relay == null) { return; }
            if (relay.buildingMaterial.IsActive())
            {
                relay.buildingMaterial.SetObstructed();
            }
        }
    }

    public void UpgradeToStone()
    {
        wall = StoneWallBuild;
        wallBuild = StoneWall;

        int count = wallPool.Count;
        foreach (GameObject g in wallPool)
        {
            Destroy(g);
        }
        wallPool.Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(wall);
            wallPool.Add(go);
            go.transform.SetParent(wallParent.transform);
            wall.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
}
