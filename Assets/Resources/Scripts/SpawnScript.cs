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
	UnityAction restartAction;


	void OnDisable () {
		EventManager.StopListening ("RESTART", restartAction);
		UnregisterPause ();
	}

	void Start () {
		restartAction = new UnityAction (RestartCounter);
		EventManager.StartListening ("RESTART", restartAction);
		RegisterPause ();
		RestartCounter ();
	}
	
	void Update () {
		if (!isPaused) {
			timer += Time.deltaTime;
			if (timer > spawnInterval) {
				SpawnNewDouglas ();
				timer = 0;
				counter++;
				if (counter >= 10) {
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
		do {
			Vector3 xPosition = Vector3.right * Random.Range (-10, 0),
			zPosition = Vector3.forward * Random.Range (-15, 2);
			
			NavMeshHit hit;
			if (NavMesh.SamplePosition (xPosition + zPosition, out hit, 15, NavMesh.AllAreas)) {
				newDouglas.transform.position = hit.position;
				if (NavMesh.Raycast (newDouglas.transform.position, newDouglas.transform.forward * 3, out hit, NavMesh.AllAreas)) {
					transform.eulerAngles = Vector3.up * 180;
				}
			}
		} while (Vector3.Distance(newDouglas.transform.position,target.position) < 2);

	}

	void RestartCounter () {
		counter = 0;
		timer = 2;
	}
}
