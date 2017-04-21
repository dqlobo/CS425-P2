using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossScript : MonoBehaviour {

	Vector3 target;
	NavMeshAgent agent;
	Animator animator;

	// Use this for initialization
	void Start () {
		GetAllComponents ();
	}

	void GetAllComponents () {
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		// check if destination reached? 
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
				WalkToPosition (hit.point);
			}
		}
		if (target != null && IsCloseEnough ()) {
			DidReachDestination ();
		}
	}

	void WalkToPosition(Vector3 newPosition) {
		target = newPosition;
		animator.SetBool ("isWalking", true);
		agent.SetDestination (newPosition);
	}

	void DidReachDestination() {
		animator.SetBool ("isWalking", false);
	}

	bool IsCloseEnough() {
		return transform.position.x == target.x
		&& transform.position.z == target.z;
	}
}
