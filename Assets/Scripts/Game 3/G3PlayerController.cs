using UnityEngine;
using System.Collections;

public class G3PlayerController : MonoBehaviour {
    float speed;
    float time;
    bool gameOver;

	public int id;
	public bool comPlayer;

    int difficulty;
    float[] minSpeed = { 5, 7, 9 };
    float[] maxSpeed = { 8, 8.5f, 10 };
    int[] jumpPossibility = { 60, 80, 95 };

    float totalTime;

	// Use this for initialization
	void Start () {
        time = 1;
        speed = 0;
		gameOver = false;
		comPlayer = !GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().players[id];
        difficulty = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().AIDifficulty;
        totalTime = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameOver || Time.timeScale == 0)
            return;
        totalTime += Time.deltaTime;
		time += Time.deltaTime;
		bool hit = gameObject.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Impact");
		bool jump = gameObject.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Jump");
		bool run = gameObject.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run");
        if (hit)
            return;
        /*if (speed != 0 && !jump && !run && !hit)
			gameObject.GetComponentInChildren<Animator>().SetFloat("Speed");
        else if (speed == 0 && run)
			gameObject.GetComponentInChildren<Animator>().Stop("Run");
        else
        {
            gameObject.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        }*/
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
				gameObject.GetComponentInChildren<Animator> ().SetTrigger("Jump");
				GetComponent<AudioSource> ().Play ();
			}
		} 
		else 
		{
			if (!hit) {
				speed = Random.Range(minSpeed[difficulty], maxSpeed[difficulty]);
                time = 0;
			}
		}

        Transform planeTransform = GameObject.Find("Planes").transform.Find(transform.parent.name + " Plane").gameObject.transform;
        planeTransform.Translate(Vector3.back * speed * Time.deltaTime);
        GameObject planeTex = GameObject.Find("Visual").transform.Find(transform.parent.name + " Plane").gameObject;
        Material m = planeTex.GetComponent<MeshRenderer>().material;
        Vector2 currentOffset = m.mainTextureOffset;
        m.mainTextureOffset = new Vector2(0, (currentOffset.y - ((speed * Time.deltaTime) / 250)) % 1);
        //planeTransform.GetComponent<(Vector3.back * speed * Time.deltaTime);
        if (planeTransform.position.z < -250f)
        {
            gameOver = true;
            int racePosition = Camera.main.GetComponent<WinnerScript>().position++;
            if (racePosition == 1)
            {
                Camera.main.GetComponent<WinnerScript>().idWinner = id;
                Camera.main.GetComponent<WinnerScript>().winnerTime = totalTime;
            }
            transform.Find("Position Flat").gameObject.GetComponent<TextMesh>().text = racePosition.ToString() + "º";
            speed = 0f;

			if (racePosition == 4) 
			{
				bool[] winners = new bool[4];
				for (int i = 0; i < 4; i++) 
				{
					winners [i] = i == Camera.main.GetComponent<WinnerScript>().idWinner;
				}
                float winnerTime = Camera.main.GetComponent<WinnerScript>().winnerTime;
                GameObject.FindGameObjectWithTag("InGameMenu").GetComponent<PostGameScript>().Winner(winners, 1, false, winnerTime, 0, 3);
			}

		}
		gameObject.GetComponentInChildren<Animator> ().SetFloat("Speed",speed/5);

    }

    void OnTriggerEnter(Collider collider)
    {
		if(collider.name=="Barrier(Clone)")
		{
			gameObject.GetComponentInChildren<Animator>().SetTrigger("Impact");
	        speed = 0f;
	        Destroy(collider.gameObject);
	        //GameObject.Find("Plane").gameObject.transform.Translate(Vector3.back * 1.5f);
		}
		if(collider.name=="JumpRange" && comPlayer)
		{
			if (Random.Range (0, 100) < jumpPossibility[difficulty] ) {
				gameObject.GetComponentInChildren<Animator> ().SetTrigger("Jump");
				GetComponent<AudioSource> ().Play ();
			}
			Destroy (collider.gameObject);
		}
	}
}
