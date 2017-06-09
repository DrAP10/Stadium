﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostGameScript : MonoBehaviour {

	public GameObject postGamePanel;
	public GameObject p1Icon;
	public GameObject p2Icon;
	public GameObject p3Icon;
	public GameObject p4Icon;

	public Sprite p1Tex;
	public Sprite p2Tex;
	public Sprite p3Tex;
	public Sprite p4Tex;

	public Sprite p1ConTex;
	public Sprite p2ConTex;
	public Sprite p3ConTex;
	public Sprite p4ConTex;

	bool waitingToExit;

	SoundTrackScript soundTrackScript;

	// Use this for initialization
	void Start () {
		waitingToExit = false;
		soundTrackScript = GameObject.FindGameObjectWithTag ("GameState").GetComponent<SoundTrackScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Continue") && waitingToExit)
		{
			GoToLoadScene ();
		}
	}

	public void Winner(bool[] winners)
	{
		GameState gameState = GameObject.FindGameObjectWithTag ("GameState").GetComponent<GameState> ();
		p1Icon.SetActive (winners [0]);
		p2Icon.SetActive (winners [1]);
		p3Icon.SetActive (winners [2]);
		p4Icon.SetActive (winners [3]);
		p1Icon.GetComponent<Image> ().sprite = (!gameState.players [0]) ? p1ConTex : p1Tex;
		p2Icon.GetComponent<Image> ().sprite = (!gameState.players [1]) ? p2ConTex : p2Tex;
		p3Icon.GetComponent<Image> ().sprite = (!gameState.players [2]) ? p3ConTex : p3Tex;
		p4Icon.GetComponent<Image> ().sprite = (!gameState.players [3]) ? p4ConTex : p4Tex;

		postGamePanel.SetActive(true);
		Time.timeScale = 0;
		waitingToExit = true;

		soundTrackScript.SetClip (7);
		soundTrackScript.Play ();
	}

	public void GoToLoadScene()
	{
		waitingToExit = false;
		Time.timeScale = 1;
		postGamePanel.SetActive(false);
		GameObject.FindGameObjectWithTag ("GameState").GetComponent<GameState> ().currentMenu = 2;
		SceneManager.LoadScene(1);
		soundTrackScript.SetClip (0);
		soundTrackScript.Play ();
	}
}
