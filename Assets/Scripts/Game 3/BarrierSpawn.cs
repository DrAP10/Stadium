﻿using UnityEngine;
using System.Collections;

public class BarrierSpawn : MonoBehaviour {
    public GameObject barrier;
    public GameObject goal;

	// Use this for initialization
	void Start () {
        float lastPosition = 0f;
	    while(lastPosition<20f)
        {
            float nextPosition = Random.Range(1, 4) + lastPosition;
            if (nextPosition > 23f)
                break;
            Debug.Log(nextPosition);
            foreach(GameObject plane in GameObject.FindGameObjectsWithTag("Plane"))
            {
                GameObject barrierObject = Instantiate(barrier,
                    new Vector3(plane.transform.position.x, plane.transform.position.y + 1f, nextPosition*10),
                    plane.transform.rotation) as GameObject;
                barrierObject.transform.parent = plane.transform;
                barrierObject.transform.localScale = new Vector3(10, 2, 0.005f);
            }
            lastPosition = nextPosition;
        }
        foreach (GameObject plane in GameObject.FindGameObjectsWithTag("Plane"))
        {
            GameObject goalObject = Instantiate(goal,
                new Vector3(plane.transform.position.x, plane.transform.position.y, 250),
                plane.transform.rotation) as GameObject;
            goalObject.transform.parent = plane.transform;
            goalObject.transform.localScale = new Vector3(1.1f, 0.65f, 0.02f);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
