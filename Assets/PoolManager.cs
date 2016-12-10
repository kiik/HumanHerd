using System.Collections.Generic;
using UnityEngine;

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

    public void BuildWoodenWall()
    {
        foreach (GameObject go in woodenWallPool)
        {
            Relay relay = go.GetComponent<Relay>();
            if (relay.buildingMaterial.IsActive())
            {
                Instantiate(WoodenWallBuild, go.transform.position, go.transform.rotation);
            }
        }
        ResetWoodenWallPool();
    }
}
