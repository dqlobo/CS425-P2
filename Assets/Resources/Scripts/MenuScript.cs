using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuScript : MonoBehaviour {

	public Sprite mainSprite;
	public Sprite pauseSprite;
	public Sprite loseSprite;
	public Sprite winSprite;
	Image image;
	bool isPaused;
	UnityAction restartAction;
	UnityAction pauseAction;
	UnityAction arrestedAction;
	UnityAction victoryAction;
	bool isGameOver;

	void OnDisable () {
		EventManager.StopListening ("RESTART", restartAction);
		EventManager.StopListening ("PAUSE", pauseAction);
		EventManager.StopListening ("ARRESTED", arrestedAction);
		EventManager.StopListening ("VICTORY", victoryAction);
	}

	void Start () {
		isPaused = false;
		isGameOver = true;
		image = GetComponent<Image> ();

		restartAction = new UnityAction (DoRestart);
		EventManager.StartListening ("RESTART", restartAction);
		pauseAction = new UnityAction (PauseGame);
		EventManager.StartListening ("PAUSE", pauseAction);
		arrestedAction = new UnityAction (Arrested);
		EventManager.StartListening ("ARRESTED", arrestedAction);
		victoryAction = new UnityAction (Won);
		EventManager.StartListening ("VICTORY", victoryAction);

		image.color = Color.white;
		image.sprite = mainSprite;
	}

	void DoRestart() {
		isGameOver = false;
		image.color = Color.clear;
		if (isPaused)
			isPaused = false;
	}

	void PauseGame () {
		if (!isGameOver) {
			isPaused = !isPaused;
			if (isPaused) {	
				image.sprite = pauseSprite;
				image.color = new Color (1, 1, 1, 0.5F);
			} else {
				image.color = Color.clear;
			}
		}
	}

	void Arrested () {
		if (!isGameOver) {
			isGameOver = true;
			image.color = Color.white;
			image.sprite = loseSprite;
		}
	}

	void Won () {		
		if (!isGameOver) {
			isGameOver = true;
			image.color = Color.white;
			image.sprite = winSprite;
		}
	}
}
