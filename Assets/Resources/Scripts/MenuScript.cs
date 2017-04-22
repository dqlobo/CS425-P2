using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuScript : MonoBehaviour {

	public Sprite mainSprite;
	public Sprite pauseSprite;
	Image image;
	bool isPaused;
	UnityAction restartAction;
	UnityAction pauseAction;
	// Use this for initialization
	void OnEnable () {
		isPaused = false;
		image = GetComponent<Image> ();

		restartAction = new UnityAction (DoRestart);
		EventManager.StartListening ("RESTART", restartAction);
		pauseAction = new UnityAction (PauseGame);
		EventManager.StartListening ("PAUSE", pauseAction);
	}

	void OnDisable () {
		EventManager.StopListening ("RESTART", restartAction);
		EventManager.StopListening ("PAUSE", pauseAction);
	}

	void Start () {
		image.color = Color.white;
		image.sprite = mainSprite;

	}

	void DoRestart() {
		image.color = Color.clear;
		if (isPaused)
			isPaused = false;
	}

	void PauseGame () {
		isPaused = !isPaused;
		if (isPaused) {	
			image.sprite = pauseSprite;

			image.color = new Color (1, 1, 1, 0.5F);
		} else {
			image.color = Color.clear;
		}
	}

//	void 
}
