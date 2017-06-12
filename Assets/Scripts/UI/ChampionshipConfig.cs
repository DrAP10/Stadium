using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChampionshipConfig : MonoBehaviour {

	public GameObject pointsPanel;
	public GameObject difficultyPanel;
	public Text pointsLabel;
	int points;
	bool axisInUse=false;

	// Use this for initialization
	void Start () {
		points = 5;
		UpdatePointsLabel ();
	}
	
	// Update is called once per frame
	void Update () {
		float v = Input.GetAxis ("P3 Vertical");
		if (v > 0 && !axisInUse) 
		{
			if (points < 15) 
			{
				axisInUse = true;
				points++;
				UpdatePointsLabel ();
			}
		} 
		else if (v < 0 && !axisInUse) 
		{
			if (points > 5) 
			{
				axisInUse = true;
				points--;
				UpdatePointsLabel ();
			}
		} else if (v == 0)
			axisInUse = false;
		if (Input.GetButtonDown ("Submit"))
			Camera.main.GetComponent<MenuScript> ().GoToPlayerSelection ();
		if (Input.GetButtonDown ("Cancel"))
			Camera.main.GetComponent<MenuScript> ().GoToModeSelection ();
	}

	void UpdatePointsLabel()
	{
		pointsLabel.text = points.ToString();
	}
}
