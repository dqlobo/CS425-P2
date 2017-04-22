using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoPauseBehavior : MonoBehaviour {
	UnityAction freezeAction;
	protected bool isPaused;

	void OnEnable () {
		RegisterPause ();
	}

	protected void RegisterPause() {
		freezeAction = new UnityAction (Freeze);
		EventManager.StartListening ("PAUSE", freezeAction);
	}

	protected void UnregisterPause() {
		EventManager.StopListening ("PAUSE", freezeAction);
	}

	void OnDisable () {
		UnregisterPause ();
	}

	protected void Freeze () {
		isPaused = !isPaused;
	}
}
