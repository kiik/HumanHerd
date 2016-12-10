using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BuildingMaterial : MonoBehaviour {

    SpriteRenderer sr;
    Color32 obstructed = new Color32(230, 90, 90, 200);
    Color32 clear = new Color32(90,230,115,200);

    bool isAvailable = true;

	void Awake () {
        sr = GetComponent<SpriteRenderer>();
        sr.color = clear;
	}

    public void Obstruct()
    {
        sr.color = obstructed;
    }
    public void Clear()
    {
        sr.color = clear;
    }
    public bool IsAvailable()
    {
        return isAvailable;
    }
    public void DisableSpriteRenderer()
    {
        sr.enabled = false;
    }
}
