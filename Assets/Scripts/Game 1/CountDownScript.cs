using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownScript : MonoBehaviour {
	int labelTime;
	float time;
	bool isEnd;
	GUIStyle style;
	// Use this for initialization
	void Start () {
		time = 30;
		labelTime = 30;
		isEnd = false;
		style = new GUIStyle ();
		style.normal.textColor = Color.yellow;
		style.fontSize = 100;
		style.alignment = TextAnchor.UpperCenter;
	}
	
	// Update is called once per frame
	void Update () {
		if (isEnd)
			return;
		if (time <= 0) {
			int maxPoints=-1;
			foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player")) {
				if (g.GetComponentInChildren<Game1Impact> ().points > maxPoints)
					maxPoints = g.GetComponentInChildren<Game1Impact> ().points;
			}
			bool[] winners = new bool[4];
			for (int i = 0; i < 4; i++) 
			{
				winners [i] = false;
			}
			foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player")) {
				if (g.GetComponentInChildren<Game1Impact> ().points == maxPoints)
					winners[g.GetComponentInChildren<Game1Jump> ().id] = true;
			}
			GameObject.FindGameObjectWithTag("InGameMenu").GetComponent<PostGameScript> ().Winner (winners);
		}
		time -= Time.deltaTime;
		if (labelTime > time)
			labelTime--;
	}

	void OnGUI(){
		GUI.Label (new Rect (0, 0, Screen.width, Screen.height), labelTime.ToString(), style);
	}
}
