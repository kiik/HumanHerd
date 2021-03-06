using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class Tiling : MonoBehaviour {

	public float X, Y;
	public int tileOffset = 0;

	void Awake () {
		GetComponent<Renderer>().material.SetFloat("RepeatX", X);
		GetComponent<Renderer>().material.SetFloat("RepeatY", Y);
		GetComponent<Renderer>().material.SetFloat("TOffsetX", tileOffset);
	}

	void Update() {
		GetComponent<Renderer>().material.SetFloat("RepeatX", X);
		GetComponent<Renderer>().material.SetFloat("RepeatY", Y);
		GetComponent<Renderer>().material.SetFloat("TOffsetX", tileOffset);
	}

}
