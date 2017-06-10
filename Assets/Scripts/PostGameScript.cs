using System.Collections;
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

    public Text current;
    public Text record;
    public GameObject recortLabel;
    public GameObject currentLabel;

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

        current.gameObject.SetActive(false);
        record.gameObject.SetActive(false);
        currentLabel.SetActive(false);
        recortLabel.SetActive(false);
    }

    public void Winner(bool[] winners, int type, bool moreIsBetter, float floatAmount, int intAmount, int gameId)
    {
        Winner(winners);
        bool isComWinner=false;
        current.gameObject.SetActive(true);
        this.record.gameObject.SetActive(true);
        currentLabel.SetActive(true);
        recortLabel.SetActive(true);
        if (type==0)
        {
            int record = PlayerPrefs.GetInt("Record_game_" + gameId, -1);
            string winnerName = "";
            for (int i = 0; i < winners.Length; i++)
                if (winners[i])
                {
                    isComWinner = !GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().players[i];
                    winnerName = PlayerPrefs.GetString("Player" + (i + 1));
                    break;
                }
            if (record == -1 || (moreIsBetter && intAmount > record) || (!moreIsBetter && intAmount < record))
            {
                if (!isComWinner)
                {
                    record = intAmount;
                    PlayerPrefs.SetInt("Record_game_" + gameId, intAmount);
                    PlayerPrefs.SetString("SRecord_game_" + gameId, winnerName);
                }
            }
            current.text = "" + intAmount;
            if (record == -1)
                this.record.text = "Sin records";
            else
                this.record.text = "" + record + "(" + PlayerPrefs.GetString("SRecord_game_" + gameId) + ")";
        }
        else
        {
            float record = PlayerPrefs.GetFloat("Record_game_" + gameId, -1);
            string winnerName = "";
            for (int i = 0; i < winners.Length; i++)
                if (winners[i])
                {
                    isComWinner = !GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().players[i];
                    winnerName = PlayerPrefs.GetString("Player" + (i + 1));
                    break;
                }
            if (record == -1 || (moreIsBetter && floatAmount > record) || (!moreIsBetter && floatAmount < record))
            {
                if (!isComWinner)
                {
                    record = floatAmount;
                    PlayerPrefs.SetFloat("Record_game_" + gameId, floatAmount);
                    PlayerPrefs.SetString("SRecord_game_" + gameId, winnerName);
                }
            }
            current.text = "" + FormatTime(floatAmount);
            if (record == -1)
                this.record.text = "Sin records";
            else
                this.record.text = "" + FormatTime(record) + "(" + PlayerPrefs.GetString("SRecord_game_" + gameId) + ")";
        }
    }

    string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = System.String.Format("{0:00}.{1:000}", seconds, fraction);
        return timeText;
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
