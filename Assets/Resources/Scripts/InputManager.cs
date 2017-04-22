using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.R)) { // restart game
			EventManager.TriggerEvent("RESTART");
		} else if (Input.GetKeyUp (KeyCode.P)) { // pause game			
			EventManager.TriggerEvent ("PAUSE");		
		} else if (Input.GetKeyUp(KeyCode.Q)) { // quit game
			Application.Quit ();
		}
			
	}

	void OnEnable () {
		EventManager.TriggerEvent ("PAUSE");
	}
}
