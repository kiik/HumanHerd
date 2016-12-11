using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof (SpriteRenderer))]
public class Tiling : MonoBehaviour {

	public float X;
	public int tileOffset = 0;

	void Awake () {
		GetComponent<Renderer>().sharedMaterial.SetFloat("RepeatX", X);
		GetComponent<Renderer>().sharedMaterial.SetFloat("TOffsetX", tileOffset);
	}

	void Update() {
		GetComponent<Renderer>().sharedMaterial.SetFloat("RepeatX", X);
		GetComponent<Renderer>().sharedMaterial.SetFloat("TOffsetX", tileOffset);
	}

}
