using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
	
	public GameObject modeSelection;
	public GameObject gameSelection;
	public GameObject playerSelection;
    public GameObject championshipConfiguration;
    public GameObject gameConfiguration;

    public Slider masterVolume;
    public Slider AIDifficulty;
    GameState gameState;

    // Use this for initialization
    void Start () {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        int currentMenu = gameState.currentMenu;
        masterVolume.value = gameState.masterVolume;
        AIDifficulty.value = gameState.AIDifficulty;
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
        else if (Input.GetButtonDown("Cancel") &&
            GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().currentMenu == 3)
            GoToModeSelection();
    }

	public void GameSelected(int id)
	{
		SceneManager.LoadScene (id);
		GameObject.FindGameObjectWithTag ("GameState").GetComponent<SoundTrackScript> ().SetClip (id-1);
	}

	public void GoToPlayerSelection()
	{
        GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().currentMenu = 1;
        modeSelection.SetActive (false);
		gameSelection.SetActive (false);
		playerSelection.SetActive (true);
		championshipConfiguration.SetActive (false);
        gameConfiguration.SetActive(false);
    }

	public void GoToModeSelection()
	{
        GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().currentMenu = 0;
        modeSelection.SetActive (true);
		gameSelection.SetActive (false);
		playerSelection.SetActive (false);
		championshipConfiguration.SetActive (false);
        gameConfiguration.SetActive(false);
    }

	public void GoToGameSelection()
	{
        GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().currentMenu = 2;
        modeSelection.SetActive (false);
		gameSelection.SetActive (true);
		playerSelection.SetActive (false);
		championshipConfiguration.SetActive (false);
        gameConfiguration.SetActive(false);
    }

	public void GoToChampionshipConfiguration()
	{
		modeSelection.SetActive (false);
		gameSelection.SetActive (false);
		playerSelection.SetActive (false);
		championshipConfiguration.SetActive (true);
        gameConfiguration.SetActive(false);
    }

    public void GoToGameConfiguration()
    {
        GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().currentMenu = 3;
        modeSelection.SetActive(false);
        gameSelection.SetActive(false);
        playerSelection.SetActive(false);
        championshipConfiguration.SetActive(false);
        gameConfiguration.SetActive(true);
    }

    public void SetMasterVolume()
    {
        AudioListener.volume = masterVolume.value;
        PlayerPrefs.SetFloat("MasterVolume", masterVolume.value);
    }

    public void SetAIDifficulty()
    {
        PlayerPrefs.SetInt("AIDifficulty", (int)AIDifficulty.value);
    }
    
}
