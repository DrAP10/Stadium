using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
	
	public GameObject modeSelection;
	public GameObject gameSelection;
	public GameObject playerSelection;
	public GameObject championshipConfiguration;

	// Use this for initialization
	void Start () {
		int currentMenu = GameObject.FindGameObjectWithTag ("GameState").GetComponent<GameState> ().currentMenu;
		switch (currentMenu)
		{
		case 0:
			GoToModeSelection ();
			break;
		case 1:
			GoToPlayerSelection ();
			break;
		case 2:
			GoToGameSelection ();
			break;
		default:
			GoToModeSelection ();
			break;
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Cancel") &&
            GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().currentMenu == 2)
            GoToPlayerSelection();	
	}

	public void GameSelected(int id)
	{
		SceneManager.LoadScene (id);
	}

	public void GoToPlayerSelection()
	{
        GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().currentMenu = 1;
        modeSelection.SetActive (false);
		gameSelection.SetActive (false);
		playerSelection.SetActive (true);
		championshipConfiguration.SetActive (false);
	}

	public void GoToModeSelection()
	{
        GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().currentMenu = 0;
        modeSelection.SetActive (true);
		gameSelection.SetActive (false);
		playerSelection.SetActive (false);
		championshipConfiguration.SetActive (false);
	}

	public void GoToGameSelection()
	{
        GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().currentMenu = 2;
        modeSelection.SetActive (false);
		gameSelection.SetActive (true);
		playerSelection.SetActive (false);
		championshipConfiguration.SetActive (false);
	}

	public void GoToChampionshipConfiguration()
	{
		modeSelection.SetActive (false);
		gameSelection.SetActive (false);
		playerSelection.SetActive (false);
		championshipConfiguration.SetActive (true);
	}
    
}
