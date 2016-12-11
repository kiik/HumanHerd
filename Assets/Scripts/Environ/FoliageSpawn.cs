using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageSpawn : MonoBehaviour {

	public int amount = 10;

	public GameObject[] entities;

	void Start () {
		Bounds b = ((Collider2D)GetComponent<Collider2D>()).bounds;

		spawnFoliage();
	}

	void Update () {

	}

	void spawnFoliage() {
		if(entities.Length <= 0) return;

		for(int i = 0; i < amount; i++) {
			spawnFoliageSingle();
		}
	}

	void spawnFoliageSingle() {
		Collider2D col = GetComponent<Collider2D>();
		GameObject o = Instantiate(entities[(int)Random.Range(0, entities.Length)], transform.position, Quaternion.identity);

		Bounds b = col.bounds, b2 = o.GetComponent<SpriteRenderer>().bounds;

		Vector3 p = new Vector3(Random.Range(b.min.x + b2.extents.x, b.max.x - b2.extents.x), Random.Range(b.min.y + + b2.extents.y, b.max.y - + b2.extents.y), 0);

		o.transform.position = p;
		o.transform.parent = transform;

	}

}
