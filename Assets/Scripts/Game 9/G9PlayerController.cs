using UnityEngine;
using System.Collections;

public class G9PlayerController : MonoBehaviour {
    bool right = true;
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
            if(points>8)
                transform.Translate(transform.up * (-0.04f));
            GetComponent<G9ModifyTerrain>().LowerTerrain();
            points++;
            if(points>100)
            {
                gameObject.GetComponent<Animation>().Play("Win");
                win = true;
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    player.GetComponent<G9PlayerController>().gameOver = true;
            }
        }
    }
}
