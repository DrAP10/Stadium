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
		GoToModeSelection ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameSelected(int id)
	{
		SceneManager.LoadScene (id);
	}

	public void GoToPlayerSelection()
	{
		modeSelection.SetActive (false);
		gameSelection.SetActive (false);
		playerSelection.SetActive (true);
		championshipConfiguration.SetActive (false);
	}

	public void GoToModeSelection()
	{
		modeSelection.SetActive (true);
		gameSelection.SetActive (false);
		playerSelection.SetActive (false);
		championshipConfiguration.SetActive (false);
	}

	public void GoToGameSelection()
	{
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
