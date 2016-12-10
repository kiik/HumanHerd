using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Pathfinding;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance = null;

    [SerializeField]
    GameObject WoodenWallParent;
    [SerializeField]
    GameObject WoodenWall;
    [SerializeField]
    GameObject WoodenWallBuild;
    public int woodenWallPoolCount = 0;
    public List<GameObject> woodenWallPool = new List<GameObject>();

    void Awake()
    {
        if (!instance) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }

    void Start()
    {
        InitPools();
    }

    void InitPools()
    {
        for (int i = 0; i < woodenWallPoolCount; i++)
        {
            GameObject go = Instantiate(WoodenWall);
            woodenWallPool.Add(go);
            go.transform.SetParent(WoodenWallParent.transform);
            WoodenWall.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    public void IncreaseWoodenWallPool()
    {
        GameObject go = Instantiate(WoodenWall);
        woodenWallPool.Add(go);
        go.transform.SetParent(WoodenWallParent.transform);
        WoodenWall.GetComponent<Relay>().spriteRenderer.enabled = false;
    }

    public int GetWoodenWallPoolCount()
    {
        return woodenWallPool.Count;
    }

    public void ResetWoodenWallPool()
    {
        foreach (GameObject go in woodenWallPool)
        {
            Relay relay = go.GetComponent<Relay>();
            relay.buildingMaterial.SetBMInactive();
            relay.buildingMaterial.Clear();
            relay.buildingMaterial.DisableSpriteRenderer();
        }
    }

    public void SetWoodenWallPos(int index, Vector2 pos)
    {
        if (float.IsNaN(pos.x)) { return; }

        GameObject woodenWall = woodenWallPool[index];
        woodenWall.transform.position = pos;
        Relay relay = woodenWall.GetComponent<Relay>();
        relay.buildingMaterial.EnableSpriteRenderer();
        relay.buildingMaterial.Clear();
        relay.buildingMaterial.SetBMActive();
    }

    public void CheckWoodenWallDisable(int index)
    {
        GameObject go = woodenWallPool[index];
        Relay relay = go.GetComponent<Relay>();
        if (relay.buildingMaterial.IsActive())
        {
            go.transform.localPosition = Vector2.zero;
            relay.buildingMaterial.DisableSpriteRenderer();
            relay.buildingMaterial.SetBMInactive();

        }
    }

    public GraphUpdateObject BuildWoodenWall()
    {
        GameObject g1 = null;
        GameObject g2 = null;

        foreach (GameObject go in woodenWallPool.Where(g => g.GetComponent<Relay>().buildingMaterial.IsActive() && !g.GetComponent<Relay>().buildingMaterial.IsObstructed()))
        {
            GameObject g = Instantiate(WoodenWallBuild, go.transform.position, go.transform.rotation);
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

        return guo;
    }

    public void ProhibitBuild()
    {
        foreach (GameObject go in woodenWallPool)
        {
            Relay relay = go.GetComponent<Relay>();
            if (relay.buildingMaterial.IsActive())
            {
                relay.buildingMaterial.SetObstructed();
            }
        }
    }
}
