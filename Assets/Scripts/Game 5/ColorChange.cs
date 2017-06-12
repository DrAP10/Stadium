using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour {
    public Material blue;
    public Material green;
    bool currentBlue;
    float time;

	// Use this for initialization
	void Start () {
        time = Random.Range(1, 3);
        System.Random rand = new System.Random();
        if (rand.Next(1) == 1)
        {
            gameObject.GetComponent<Renderer>().material = blue;
            currentBlue = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = green;
            currentBlue = false;
        }
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            player.GetComponent<G5PlayerController>().currentBlue = currentBlue;
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
	    if(time<=0)
        {
            time = Random.Range(1, 3);
            if(currentBlue)
                gameObject.GetComponent<Renderer>().material = green;
            else
                gameObject.GetComponent<Renderer>().material = blue;
            currentBlue = !currentBlue;
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                player.GetComponent<G5PlayerController>().currentBlue = currentBlue;
        }
	}
}
