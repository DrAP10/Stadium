using UnityEngine;
using System.Collections;

public class G9PlayerController : MonoBehaviour {
    bool right = true;
    public int points = 0;
    bool win;
    public bool gameOver;

    public int id;
    public bool comPlayer;
    float comTime=0;

	float winAnimationTime;

	// Use this for initialization
	void Start () {
        gameOver = false;
		winAnimationTime = 3f;
        comPlayer = !GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().players[id];
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
            return;
        if (win && gameObject.GetComponent<Animation>().IsPlaying("Win"))
            return;
        else if (win && !gameObject.GetComponent<Animation>().IsPlaying("Win") && !gameObject.GetComponent<Animation>().IsPlaying("Rotation"))
        {
            gameObject.GetComponent<Animation>().Play("Rotation");
            return;
        }
		else if(gameObject.GetComponent<Animation>().IsPlaying("Rotation"))
		{
			winAnimationTime -= Time.deltaTime;
			if (winAnimationTime <= 0) 
			{
				bool[] winners = new bool[4];
				for (int i = 0; i < 4; i++) 
				{
					winners [i] = i == id;
				}
				GameObject.FindGameObjectWithTag("InGameMenu").GetComponent<PostGameScript> ().Winner (winners);
			}
		}
		if (gameOver)
			return;
        if (!comPlayer)
        {
            if ((Input.GetButtonDown(transform.parent.name + " Main") && right)
                || (Input.GetButtonDown(transform.parent.name + " Secondary") && !right))
            {
                right = !right;
                if (points > 8)
                    transform.Translate(transform.up * (-0.04f));
                GetComponent<G9ModifyTerrain>().LowerTerrain();
                points++;
                if (points > 100)
                {
                    gameObject.GetComponent<Animation>().Play("Win");
                    win = true;
                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                        player.GetComponent<G9PlayerController>().gameOver = true;
                }
            }
        }
        else
        {
            comTime -= Time.deltaTime;
            if (comTime <= 0)
            {
                comTime = Random.Range(0.05f, 0.2f);
                if (points > 8)
                    transform.Translate(transform.up * (-0.04f));
                GetComponent<G9ModifyTerrain>().LowerTerrain();
                points++;
                if (points > 100)
                {
                    gameObject.GetComponent<Animation>().Play("Win");
                    win = true;
                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                        player.GetComponent<G9PlayerController>().gameOver = true;
                }
            }
        }
    }
}
