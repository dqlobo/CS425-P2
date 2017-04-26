using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Douglas : MonoPauseBehavior {

	public Transform target;
	Animator animator;
	Rigidbody rb;
	BoxCollider bc;
	UnityAction restartAction;

	void OnEnable () {
		restartAction = new UnityAction (RestartCharacter);
		EventManager.StartListening ("RESTART", restartAction);
		RegisterPause ();

	}

	void OnDisable () {
		EventManager.StopListening ("RESTART", restartAction);
	}

	void RestartCharacter () {
		gameObject.SetActive (false);
	}

	void Start () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		bc = GetComponent<BoxCollider> ();
	}

	void Update () {
		if (isPaused) {
			animator.speed = 0;	
			rb.useGravity = false;
			rb.velocity = Vector3.zero;
		} else {
			animator.speed = 1;		
			rb.useGravity = true;
			if (transform.position.y < -0.025) {
				SetFallingState ();
			} else if (Vector3.Distance (target.position, transform.position) < 10) {
				WalkToPosition (target.position);
			} else {
				SetIdleState ();
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player")
			EventManager.TriggerEvent ("ARRESTED");
	}

	void SetIdleState () {
		if (animator.GetBool("isWalking"))
			animator.SetBool ("isWalking", false);
	}
	void SetFallingState() {
//		transform.TransformPoint (bc.center);
		animator.SetTrigger("fallTrigger");
		bc.enabled = false;
//		rb.AddForce (transform.forward * 5.0f);
		transform.position += transform.forward * Time.deltaTime;
		transform.RotateAround (transform.TransformPoint(bc.center),
			(transform.right),
			270 * Time.deltaTime);
	}
	void WalkToPosition(Vector3 newPosition) {
		// was using LookAt but I needed a way to only do partial lookat rotation
		// based rotation line on https://forum.unity3d.com/threads/transform-lookat-or-quaternion-lookrotation-on-1-axis-only.36377/
		Quaternion partialRot = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, partialRot, Time.deltaTime * 0.5F);
		if (!Physics.Raycast (transform.position, transform.forward, 1F, LayerMask.GetMask ("Scenery"))) {
			if (!animator.GetBool("isWalking"))
				animator.SetBool ("isWalking", true);		
			transform.position += transform.forward * Time.deltaTime * 1.4F;
		} 
	}

}
