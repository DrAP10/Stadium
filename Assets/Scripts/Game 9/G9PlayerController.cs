using UnityEngine;
using System.Collections;

public class G9PlayerController : MonoBehaviour {
    bool right = false;
    public int points = 0;
    bool win;
    public bool gameOver;
	// Use this for initialization
	void Start () {
        gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (win && gameObject.GetComponent<Animation>().IsPlaying("Win"))
            return;
        else if (win && !gameObject.GetComponent<Animation>().IsPlaying("Win") && !gameObject.GetComponent<Animation>().IsPlaying("Rotation"))
        {
            gameObject.GetComponent<Animation>().Play("Rotation");
            return;
        }
        if (gameOver)
            return;
        if ((Input.GetButtonDown(transform.parent.name+" Main") && right)
            ||(Input.GetButtonDown(transform.parent.name + " Secondary") && !right))
        {
            right = !right;
            transform.Translate(transform.up * (-0.04f));
            if (points < 20)
            {
                float scale = 0.02f;
                float translate = 0.1f;
                transform.parent.FindChild("Floor").transform.FindChild("Hole").transform.localScale += new Vector3(0.2f, 0, 0.2f);
                transform.parent.FindChild("Floor").transform.FindChild("Right").transform.localScale += new Vector3(-scale, 0, 0f);
                transform.parent.FindChild("Floor").transform.FindChild("Right").transform.Translate(new Vector3(translate, 0, 0f));
                transform.parent.FindChild("Floor").transform.FindChild("Left").transform.localScale += new Vector3(-scale, 0, 0f);
                transform.parent.FindChild("Floor").transform.FindChild("Left").transform.Translate(new Vector3(-translate, 0, 0f));
                transform.parent.FindChild("Floor").transform.FindChild("Forward").transform.localScale += new Vector3(0f, 0, -scale);
                transform.parent.FindChild("Floor").transform.FindChild("Forward").transform.Translate(new Vector3(0f, 0, translate));
                transform.parent.FindChild("Floor").transform.FindChild("Back").transform.localScale += new Vector3(0f, 0, -scale);
                transform.parent.FindChild("Floor").transform.FindChild("Back").transform.Translate(new Vector3(0f, 0, -translate));
            }
            if (points > 2)
                transform.parent.FindChild("Floor").transform.FindChild("Center").gameObject.SetActive(false);
            //transform.FindChild("Floor").transform.Translate(transform.up * (0.02f));
            points++;
            if(points>100)
            {
                gameObject.GetComponent<Animation>().Play("Win");
                win = true;
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    player.GetComponent<G9PlayerController>().gameOver = true;
            }
        }
        /*if (Input.GetButtonDown("Fire2") && !right)
        {
            right = true;
            transform.Translate(transform.up * (-0.02f));
            transform.FindChild("Floor").transform.FindChild("Hole").transform.localScale += new Vector3(0.1f, 0, 0.1f);
            if (points > 5)
                transform.FindChild("Floor").transform.FindChild("Center").gameObject.SetActive(false);
            transform.FindChild("Floor").transform.Translate(transform.up * (0.02f));
            //transform.FindChild("Sphere").transform.localScale += new Vector3(0.1f, 0, 0.1f);
            //transform.FindChild("Sphere").transform.Translate(transform.up * (0.02f));
            points++;
        }*/
    }
}
