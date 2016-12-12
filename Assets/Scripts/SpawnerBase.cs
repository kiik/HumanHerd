using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBase : MonoBehaviour {

		protected Bounds spawnArea;

		protected Vector3 generateSpawnPoint(Bounds b2) {
			//FIXME: What if Objects larger than spawn area?
			Bounds b = spawnArea;

			Vector3 p = new Vector3(Random.Range(b.min.x + b2.extents.x, b.max.x - b2.extents.x), Random.Range(b.min.y + + b2.extents.y, b.max.y - + b2.extents.y), 0);
			return p;
		}

		protected void spawnEntity(GameObject pref) {
			GameObject o = Instantiate(pref, transform.position, Quaternion.identity);
			o.transform.position = generateSpawnPoint(o.GetComponent<Collider2D>().bounds);
		}

}
