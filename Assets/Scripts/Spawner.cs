using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

		public int amount = 10;

		public GameObject[] entities;

		Collider2D col;

		void Start () {
			col = GetComponent<Collider2D>();

			initiateSpawn();
		}

		void Update () {

		}

		void initiateSpawn() {
			for(int i = 0; i < amount; i++) {
				spawnEntity(getRandomPrefab());
			}
		}

		Vector3 generateSpawnPoint(Bounds b2) {
			//FIXME: What if Objects larger than spawn area?
			Bounds b = col.bounds;

			Vector3 p = new Vector3(Random.Range(b.min.x + b2.extents.x, b.max.x - b2.extents.x), Random.Range(b.min.y + + b2.extents.y, b.max.y - + b2.extents.y), 0);
			return p;
		}

		GameObject getRandomPrefab() {
			if(entities.Length <= 0) return null;

			return entities[(int)Random.Range(0, entities.Length)];
		}

		void spawnEntity(GameObject pref) {
			GameObject o = Instantiate(pref, transform.position, Quaternion.identity);
			o.transform.position = generateSpawnPoint(o.GetComponent<Collider2D>().bounds);
		}

}
