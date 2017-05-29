using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierVisibility : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.name=="Barrier(Clone)")
			collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
	}

	void OnTriggerExit(Collider collider)
	{
		Destroy (collider.gameObject);
	}
}
