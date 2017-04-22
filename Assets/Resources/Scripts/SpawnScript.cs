using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class SpawnScript : MonoPauseBehavior {
	public float spawnInterval;
	public Transform target;
	float timer;
	float counter;

	void Start () {
		timer = 2; // offset (first spawn at 3 seconds
		counter = 0;
	}
	
	void Update () {
		if (!isPaused) {
			timer += Time.deltaTime;
			if (timer > spawnInterval) {
				SpawnNewDouglas ();
				timer = 0;
				counter++;
				if (counter == 5) {
					EventManager.TriggerEvent ("VICTORY");
				}
			}
		}
	}

	void SpawnNewDouglas () {
		// Got load snippet from P1	
		GameObject newDouglas = (GameObject) Instantiate (Resources.Load ("Prefabs/Douglas"));
		Douglas dScript = newDouglas.GetComponent<Douglas> ();
		dScript.target = target;
		newDouglas.transform.SetParent (transform);
		Vector3 xPosition = Vector3.right * Random.Range (-23, 15),
			zPosition = Vector3.forward * Random.Range (-20, 20);

		NavMeshHit hit;
		if (NavMesh.SamplePosition (xPosition + zPosition, out hit, 100, NavMesh.AllAreas)) {
			newDouglas.transform.position = hit.position;
		}

	}
}
