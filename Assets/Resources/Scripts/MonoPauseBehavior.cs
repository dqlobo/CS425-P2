using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoPauseBehavior : MonoBehaviour {

	UnityAction freezeAction;
	UnityAction unPauseRestartAction;
	protected bool isPaused;

	void Start () {
		RegisterPause ();
	}

	protected void RegisterPause() {
		freezeAction = new UnityAction (Freeze);
		unPauseRestartAction = new UnityAction (RestartAndUnpause);
		EventManager.StartListening ("PAUSE", freezeAction);
		EventManager.StartListening ("RESTART", unPauseRestartAction); 
	}

	protected void UnregisterPause() {
		EventManager.StopListening ("PAUSE", freezeAction);
		EventManager.StartListening ("RESTART", unPauseRestartAction); 
	}

	void OnDisable () {
		UnregisterPause ();
	}

	protected void Freeze () {
		isPaused = !isPaused;
	}

	void RestartAndUnpause () {
		if (isPaused)
			isPaused = false;
	}
}
