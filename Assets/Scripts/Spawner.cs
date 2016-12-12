using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : SpawnerBase {

		public int amount = 10;

		public GameObject[] entities;

		void Start () {
			spawnArea = GetComponent<Collider2D>().bounds;

			//initiateSpawn();
		}

		void Update () {

		}

		void initiateSpawn() {
			for(int i = 0; i < amount; i++) {
				spawnEntity(getRandomPrefab());
			}
		}

		GameObject getRandomPrefab() {
			if(entities.Length <= 0) return null;

			return entities[(int)Random.Range(0, entities.Length)];
		}

}
