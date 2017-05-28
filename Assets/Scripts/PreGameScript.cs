using System.Collections;
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
	// Use this for initialization
	void Start () {
        countDown = 3;
        countDownMenu.SetActive(true);
        countDownMenu.GetComponent<Image>().sprite = number3;
        foreach (GameObject g in scriptsContainers)
        {
            MonoBehaviour[] scripts = g.GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        float old = countDown;
        countDown -= Time.deltaTime;
        if(countDown<=0)
        {
            countDownMenu.SetActive(false);
            foreach (GameObject g in scriptsContainers)
            {
                MonoBehaviour[] scripts = g.GetComponentsInChildren<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = true;
                }
            }
        }
        else if(old > 2 && countDown <= 2)
            countDownMenu.GetComponent<Image>().sprite = number2;
        else if (old > 1 && countDown <= 1)
            countDownMenu.GetComponent<Image>().sprite = number1;
    }
}
