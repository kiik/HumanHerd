using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : SpawnerBase {

	public int NPCAmount = 10;
	public GameObject NPC;

	public struct Wave {
		int amount;
	};

	public Wave[] waves;

	int wave_ix;

	// Use this for initialization
	void Start () {
		spawnArea = GameObject.FindGameObjectWithTag("InvaderSpawner").GetComponent<Collider2D>().bounds;
		wave_ix = 0;

		initialSpawn();
	}

	// Update is called once per frame
	void Update () {

	}

	void initialSpawn() {
		for(int i = 0; i < NPCAmount; i++)
			spawnEntity(NPC);
	}

	void spawnWave() {
		// TODO: Release wave.amount number of citizens
	}

}
