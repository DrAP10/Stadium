using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameScript : MonoBehaviour {

    public GameObject exitMenu;
    public bool isPause = false;
	SoundTrackScript soundTrackScript;

    // Use this for initialization
    void Start () {
		soundTrackScript = GameObject.FindGameObjectWithTag ("GameState").GetComponent<SoundTrackScript> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isPause = !isPause;
            exitMenu.SetActive(isPause);
			if (isPause) {
				//GameObject.FindGameObjectWithTag("InGameMenu").gameObject.GetComponent<AudioSource> ().Pause;
				Time.timeScale = 0;
				soundTrackScript.Pause ();
				return;
			} else {
				//GameObject.FindGameObjectWithTag("InGameMenu").gameObject.GetComponent<AudioSource> ().UnPause();
				Time.timeScale = 1;
				soundTrackScript.UnPause ();
			}
        }
    }

    public void Resume()
    {
        isPause = false;
        exitMenu.SetActive(isPause);
        Time.timeScale = 1;
		soundTrackScript.UnPause ();
    }

    public void GoToLoadScene()
    {
        isPause = false;
        Time.timeScale = 1;
        exitMenu.SetActive(isPause);
		GameObject.FindGameObjectWithTag ("GameState").GetComponent<GameState> ().currentMenu = 2;
        SceneManager.LoadScene(1);
		soundTrackScript.SetClip (0);
		soundTrackScript.Play ();
    }
}
