using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BuildingMaterial : MonoBehaviour {

    public int cost = 1;

    SpriteRenderer sr;
    Color32 obstructed = new Color32(230, 90, 90, 200);
    Color32 clear = new Color32(90,230,115,200);

    bool isObstructed = false;
    bool isActive = false;

	void Awake () {
        sr = GetComponent<SpriteRenderer>();
        sr.color = clear;
	}

    public bool IsObstructed()
    {
        return isObstructed;
    }
    public void SetObstructed()
    {
        sr.color = obstructed;
        isObstructed = true;
    }
    public void Clear()
    {
        sr.color = clear;
        isObstructed = false;
    }
    public bool IsActive()
    {
        return isActive;
    }
    public void SetBMActive()
    {
        if (isActive) { return; }
        isActive = true;
        GameManager.instance.soundManager.WallLocate();
    }
    public void SetBMInactive()
    {
        isActive = false;
    }
    public void DisableSpriteRenderer()
    {
        sr.enabled = false;
    }
    public void EnableSpriteRenderer()
    {
        sr.enabled = true;
    }
}
