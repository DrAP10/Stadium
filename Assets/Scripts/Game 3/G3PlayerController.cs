using UnityEngine;
using System.Collections;

public class G3PlayerController : MonoBehaviour {
    float speed;
    float time;
    bool gameOver;

	public int id;
	public bool comPlayer;

	// Use this for initialization
	void Start () {
        time = 1;
        speed = 0;
		gameOver = false;
		comPlayer = !GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().players[id];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameOver || Time.timeScale == 0)
            return;
        time += Time.deltaTime;
        bool hit = gameObject.GetComponent<Animation>().IsPlaying("Impact");
        bool jump = gameObject.GetComponent<Animation>().IsPlaying("Jump");
        bool run = gameObject.GetComponent<Animation>().IsPlaying("Run");
        if (hit)
            return;
        if (speed != 0 && !jump && !run && !hit)
            gameObject.GetComponent<Animation>().Play("Run");
        else if (speed == 0 && run)
            gameObject.GetComponent<Animation>().Stop("Run");
        else
        {
            gameObject.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (time > 2f)
            speed -= 5;
        if (speed < 0)
            speed = 0;
        
		if (!comPlayer) {
			if (Input.GetButtonDown (transform.parent.name + " Main") && !hit) {
				speed += 5;
				if (speed > 10)
					speed = 10;
				time = 0f;
			}
			if (Input.GetButtonDown (transform.parent.name + " Secondary") && !hit) {
				gameObject.GetComponent<Animation> ().Play ("Jump", PlayMode.StopAll);
				GetComponent<AudioSource> ().Play ();
			}
		} 
		else 
		{
			if (!hit) {
				speed = Random.Range(9,10);
				time = 0;
			}
		}

        Transform planeTransform = GameObject.Find("Planes").transform.Find(transform.parent.name + " Plane").gameObject.transform;
        planeTransform.Translate(Vector3.back * speed * Time.deltaTime);
        if(planeTransform.position.z < -250f)
        {
            gameOver = true;
            int racePosition = Camera.main.GetComponent<WinnerScript>().position++;
			if(racePosition==1)
				Camera.main.GetComponent<WinnerScript>().idWinner=id;
            transform.Find("Position Flat").gameObject.GetComponent<TextMesh>().text = racePosition.ToString() + "º";
            speed = 0f;
            gameObject.GetComponent<Animation>().Stop();

			if (racePosition == 4) 
			{
				bool[] winners = new bool[4];
				for (int i = 0; i < 4; i++) 
				{
					winners [i] = i == Camera.main.GetComponent<WinnerScript>().idWinner;
				}
				GameObject.FindGameObjectWithTag("InGameMenu").GetComponent<PostGameScript> ().Winner (winners);
			}

        }

    }

    void OnTriggerEnter(Collider collider)
    {
		if(collider.name=="Barrier(Clone)")
		{
	        gameObject.GetComponent<Animation>().Play("Impact", PlayMode.StopAll);
	        speed = 0f;
	        Destroy(collider.gameObject);
	        //GameObject.Find("Plane").gameObject.transform.Translate(Vector3.back * 1.5f);
		}
		if(collider.name=="JumpRange" && comPlayer)
		{
			print (Random.Range (0, 4));
			if (Random.Range (0, 4) != 1) {//80%
				gameObject.GetComponent<Animation> ().Play ("Jump", PlayMode.StopAll);
				GetComponent<AudioSource> ().Play ();
			}
			Destroy (collider.gameObject);
		}
	}
}
