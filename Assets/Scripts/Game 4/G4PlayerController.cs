using UnityEngine;
using System.Collections;

public class G4PlayerController : MonoBehaviour {
    int health = 10;
    bool hypno = false;
    public bool dead;
    bool onSide = false;
    public GameObject zObject;
    public GameObject hypnoObject;

	public AudioClip hypnoClip;
	public AudioClip sleepClip;

	public int id;
	public bool comPlayer;

	//AI
	bool lastOnSide;//true if onSide true last frame

	// Use this for initialization
	void Start () 
	{
		dead = false;
		comPlayer = !GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().players[id];
		GetComponentInChildren<Animator> ().SetTrigger ("Hypno");
	}
	
	// Update is called once per frame
	void Update () {
        if (dead || Time.timeScale == 0)
            return;
		if (!comPlayer) {
			if (Input.GetButtonDown (transform.parent.name + " Main")) {
				if (onSide) {
					hypno = true;
					GameObject o = Instantiate (hypnoObject, transform.Find ("Hypno Spawn").position, transform.Find ("Hypno Spawn").rotation) as GameObject;
					GetComponent<AudioSource> ().PlayOneShot (hypnoClip);
					Destroy (o, 0.2f);
				} else
					TakeDemage ();
			}
		}
		else //AI
		{
			if (!onSide && (Random.Range (0, 999) == 1) ) //0.1%
			{
				TakeDemage ();
			}
		}
	}

    public void BallChange(int state)//0=in, 1=out
    {
        if (dead)
            return;
        if (state == 0)
        {
            hypno = false;
            onSide = true;
			if (comPlayer) 
			{
				if (Random.Range (0, 4) != 1) {//80% 
					hypno = true;
					GameObject o = Instantiate (hypnoObject, transform.Find ("Hypno Spawn").position, transform.Find ("Hypno Spawn").rotation) as GameObject;
					Destroy (o, 0.2f);
					GetComponent<AudioSource> ().PlayOneShot (hypnoClip);
				} 
			}
        }
        else
        {
            onSide = false;
            if (!hypno)
                TakeDemage();
        }
    }
    void TakeDemage()
    {
        health--;
        if (health <= 0)
        {
            gameObject.GetComponent<Animation>().Play("Dead");
			GetComponentInChildren<Animator> ().SetTrigger ("Dead");
            dead = true;

			int idWinner=-1;
			int playersAlive=0;
			foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
			{
				if (!player.GetComponent<G4PlayerController> ().dead) 
				{
					playersAlive++;
					idWinner = player.GetComponent<G4PlayerController> ().id;
				}
			}
			if (playersAlive==1)
			{
				bool[] winners = new bool[4];
				for (int i = 0; i < 4; i++) 
				{
					winners [i] = i == idWinner;
				}
				GameObject.FindGameObjectWithTag("InGameMenu").GetComponent<PostGameScript> ().Winner (winners);
			}
        }
        GameObject o=Instantiate(zObject,transform.Find("Z Spawn").position, transform.Find("Z Spawn").rotation) as GameObject;
        Destroy(o, 0.2f);
		GetComponent<AudioSource> ().PlayOneShot (sleepClip);
		GetComponentInChildren<Animator> ().SetTrigger ("Sleep");
    }
}
