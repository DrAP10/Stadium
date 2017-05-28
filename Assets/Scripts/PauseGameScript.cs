using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameScript : MonoBehaviour {

    public GameObject exitMenu;
    public bool isPause = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isPause = !isPause;
            exitMenu.SetActive(isPause);
            if (isPause)
            {
                Time.timeScale = 0;
                return;
            }
            else
                Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        isPause = false;
        exitMenu.SetActive(isPause);
        Time.timeScale = 1;
    }

    public void GoToLoadScene()
    {
        isPause = false;
        Time.timeScale = 1;
        exitMenu.SetActive(isPause);
        SceneManager.LoadScene(0);
    }
}
