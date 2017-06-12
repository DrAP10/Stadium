using UnityEngine;
using System.Collections;

public class Game1Impact : MonoBehaviour {
    public int points;
	// Use this for initialization
	void Start () {
        points = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name.Equals("Magikarp"))
        {
            collision.gameObject.GetComponent<Game1Jump>().speed = 0f;
            collision.gameObject.GetComponent<Game1Jump>().goingUp = false;
			transform.parent.gameObject.GetComponent<Animator> ().SetTrigger ("ImpactTrigger");
            //transform.parent.transform.parent.gameObject.GetComponentInChildren<Animator>().SetTrigger("CounterImpact");
            Debug.Log(transform.parent.gameObject.name);
            points++;
            string pointsString = points.ToString();
            if (pointsString.Length == 1)
                pointsString = "0" + pointsString;
            transform.parent.transform.Find("Scoreboard").GetComponent<TextMesh>().text = pointsString;
        }
    }
}
