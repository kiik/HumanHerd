using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public float X;
	public int tileOffset = 0;

	void Awake () {
		GetComponent<Renderer>().material.SetFloat("RepeatX", X);
		GetComponent<Renderer>().material.SetFloat("TOffsetX", tileOffset);
	}

	void Update() {
		GetComponent<Renderer>().material.SetFloat("RepeatX", X);
		GetComponent<Renderer>().material.SetFloat("TOffsetX", tileOffset);
	}

}
