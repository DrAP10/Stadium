using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

	public bool championship;
	public bool[] players;
	public int currentGame;
	public int[] points;
	public int pointsToWin;
	public int currentMenu;//0=ModeSelection, 1=PlayerSelection, 2=GameSelection

	void Awake() 
	{
		DontDestroyOnLoad(transform.gameObject); championship = false;
        players = new bool[4];
        currentGame = -1;
        points = new int[4];
        for (int i = 0; i < 4; i++)
        {
            points[i] = 0;
            players[i] = false;
        }
        pointsToWin = 0;
		currentMenu = 0;//ModeSelection
    }

	// Use this for initialization
	void Start ()
    {
        SceneManager.LoadScene(1);
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
