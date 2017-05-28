using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

	public bool championship;
	public string[] players;
	public int currentGame;
	public int[] points;
	public int pointsToWin;

	void Awake() 
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		championship = false;
		players = new string[4];
		currentGame = -1;
		points = new int[4];
		for (int i = 0; i < 4; i++) {
			points [i] = 0;
			players [i] = "";
		}
		pointsToWin = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Screenmanager Resolution Width", 800);
        PlayerPrefs.SetInt("Screenmanager Resolution Height", 600);
        PlayerPrefs.SetInt("Screenmanager Is Fullscreen mode", 0);
    }
}
