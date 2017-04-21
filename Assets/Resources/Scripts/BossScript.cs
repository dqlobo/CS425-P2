using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossScript : MonoBehaviour {

	public Transform target;
	NavMeshAgent agent;
	Animator animator;

	// Use this for initialization
	void Start () {
		GetAllComponents ();
		WalkToPosition (target.position);

	}

	void GetAllComponents () {
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void WalkToPosition(Vector3 newPosition) {
		animator.SetTrigger ("isWalking");
		agent.SetDestination (newPosition);
	}


}
