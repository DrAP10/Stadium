using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G4Trigger : MonoBehaviour {

    public Material gold;
    public Material white;
    public bool onSide;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<G4PlayerController>().BallChange(0);
        }
        collider.gameObject.GetComponent<Renderer>().material = white;
        onSide = true;
    }

    void OnTriggerExit(Collider collider)
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<G4PlayerController>().BallChange(1);
        }
        collider.gameObject.GetComponent<Renderer>().material = gold;
        onSide = false;
    }
}
