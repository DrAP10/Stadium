﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreGameScript : MonoBehaviour {

    public GameObject[] scriptsContainers;
    public GameObject countDownMenu;
    public Sprite number1;
    public Sprite number2;
    public Sprite number3;


    public float countDown = 3;
	bool started;

    void Awake()
    {
        foreach (GameObject g in scriptsContainers)
        {
            MonoBehaviour[] scripts = g.GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
        }
    }

	void Start () {
        countDown = 3;
		started = false;
        countDownMenu.SetActive(true);
        countDownMenu.GetComponent<Image>().sprite = number3;
    }
	
	// Update is called once per frame
	void Update () {
		if (started)
			return;
        float old = countDown;
        countDown -= Time.deltaTime;
        if(countDown<=0)
        {
			started = true;
            countDownMenu.SetActive(false);
            foreach (GameObject g in scriptsContainers)
            {
                MonoBehaviour[] scripts = g.GetComponentsInChildren<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = true;
                }
            }
			GameObject.FindGameObjectWithTag ("GameState").GetComponent<SoundTrackScript> ().Play ();
			//GameObject.FindGameObjectWithTag("InGameMenu").gameObject.GetComponent<AudioSource> ().Play ();
        }
        else if(old > 2 && countDown <= 2)
            countDownMenu.GetComponent<Image>().sprite = number2;
        else if (old > 1 && countDown <= 1)
            countDownMenu.GetComponent<Image>().sprite = number1;
    }
}
