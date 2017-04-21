using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Douglas : MonoBehaviour {

	public Transform target;
	NavMeshAgent agent;
	Animator animator;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		GetAllComponents ();
	}

	void GetAllComponents () {
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -0.05) {
			SetFallingState ();
		} else  if (Vector3.Distance (target.position, transform.position) < 8) {
			WalkToPosition (target.position);
		} else {
			SetIdleState ();
		}
	}

	void OnCollisionEnter(Collision collider) {
//		print ("Collision");
	}

	void SetIdleState () {
		animator.SetBool ("isWalking", false);
	}
	void SetFallingState() {
		animator.SetTrigger ("fallTrigger");
		transform.Rotate (Vector3.right * 90 * Time.deltaTime);
	}
	void WalkToPosition(Vector3 newPosition) {
//		target = newPosition;
		animator.SetBool ("isWalking", true);
		// was using LookAt but I needed a way to only do partial lookat rotation
		// based code on https://forum.unity3d.com/threads/transform-lookat-or-quaternion-lookrotation-on-1-axis-only.36377/
		Quaternion partialRot = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, partialRot, Time.deltaTime / 2);
		transform.position += transform.forward  * Time.deltaTime * 1.2F;

	}

}
