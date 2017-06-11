using UnityEngine;
using System.Collections;

public class Game1Jump : MonoBehaviour {
    public bool floor = true;
    float acceleration;
    float time;
    public float speed;
    float position;
    public bool goingUp;

	public int id;
	public bool comPlayer;

    int difficulty;
    float[] minRange = { 0.25f, 0.1f, 0 };
    float[] maxRange = { 0.5f, 0.25f, 0.1f };
    float nextJump;

    // Use this for initialization
    void Start () {
		comPlayer = !GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().players[id];
        difficulty = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().AIDifficulty;
    }

	// Update is called once per frame
	void Update ()
    {
        if (Time.deltaTime == 0)
            return;
        nextJump -= Time.deltaTime;
		if ((!comPlayer && Input.GetButtonDown(transform.parent.name+" Main")&&floor)
			||(comPlayer && nextJump<=0 && floor))
        {
			GetComponent<AudioSource> ().Play ();
            GetComponentInChildren<Animator>().SetTrigger("Jump");
            GetComponentInChildren<Animator>().ResetTrigger("FloorImpact");
            GetComponentInChildren<Animator>().ResetTrigger("CounterImpact");
            position = 0.15f;
            time = 0f;
            speed = 50f;
            acceleration = -5f;
            floor = false;
            goingUp = true;
        }
        if(!floor)
        {
            time += Time.deltaTime;
			if ((!comPlayer && Input.GetButton(transform.parent.name + " Main") && goingUp)
				||(comPlayer && goingUp))
            {
                position = position + speed * time;
            }
            else
            {
                goingUp = false;
                speed = speed + (acceleration * time);
                position = position + (speed * time) + (0.5f * acceleration * Mathf.Pow(time, 2));
            }
            //transform.position = new Vector3(transform.position.x, position/100f, transform.position.z);
            gameObject.GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x, position / 100f, transform.position.z));
            if (transform.position.y < 0.15f)
            {
                //transform.position = new Vector3(transform.position.x, 0.15f, transform.position.z);
                gameObject.GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x, 0.15f, transform.position.z));
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Equals("Plane"))
        {
            //if(!floor)
              //  GetComponentInChildren<Animator>().SetTrigger("FloorImpact");
            floor = true;
            nextJump = Random.Range(minRange[difficulty], maxRange[difficulty]);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.name.Equals("Floor collision"))
        {
            if (!floor)
            {
                GetComponentInChildren<Animator>().SetTrigger("FloorImpact");
            }
        }
        if (collider.transform.name.Equals("Counter collision"))
        {
            GetComponentInChildren<Animator>().SetTrigger("CounterImpact");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.name.Equals("Plane"))
            floor = false;
    }
}
