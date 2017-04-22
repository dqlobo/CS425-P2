using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BossScript : MonoPauseBehavior {

	Vector3 target;
	NavMeshAgent agent;
	Animator animator;
	bool hasDest;
	UnityAction restartAction;
	public Vector3 originPosition;
	public Vector3 originRotation;

	void OnDisable () {
		UnregisterPause ();
		EventManager.StopListening ("RESTART", restartAction);
		EventManager.StopListening ("ARRESTED", restartAction);
		EventManager.StopListening ("VICTORY", restartAction);

	}

	void RestartCharacter () {
		transform.position = originPosition;
		transform.eulerAngles = originRotation;
		hasDest = false;
		agent.SetDestination (transform.position);
		animator.SetBool ("isWalking", false);
		animator.SetTime (0);
		isPaused = false;
	}
	void Start () {
		originPosition = transform.position;
		originRotation = transform.eulerAngles;
		restartAction = new UnityAction (RestartCharacter);
		EventManager.StartListening ("RESTART", restartAction);
		EventManager.StartListening ("ARRESTED", restartAction);
		EventManager.StartListening ("VICTORY", restartAction);


		RegisterPause ();
		GetAllComponents ();
		hasDest = false;
	}

	void GetAllComponents () {
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		// check if destination reached? 
		if (isPaused) {
			animator.speed = 0;
			agent.SetDestination (transform.position);
		} else {
			animator.speed = 1;
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hit;
				// based on https://docs.unity3d.com/Manual/nav-MoveToClickPoint.html
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
//				agent.areaMask.
					NavMeshHit meshHit;
					if (NavMesh.SamplePosition (hit.point, out meshHit, 5, NavMesh.AllAreas)) {
						target = meshHit.position;
						hasDest = true;
					}

				}
			}
			if (hasDest) {
				WalkToPosition (target);
			}
			if (IsCloseEnough ()) {
				DidReachDestination ();
			}
		}
	}

	void WalkToPosition(Vector3 newPosition) {
		animator.SetBool ("isWalking", true);
		agent.SetDestination (newPosition);
	}

	void DidReachDestination() {
		animator.SetBool ("isWalking", false);
	}

	bool IsCloseEnough() {
		return Mathf.Abs(transform.position.x - target.x) < .1
			&& Mathf.Abs(transform.position.z - target.z) < .1;
	}		
}
