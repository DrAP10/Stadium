using UnityEngine;
using System.Collections;

public class Game1Jump : MonoBehaviour {
    public bool floor = true;
    float acceleration;
    float time;
    public float speed;
    float position;
    public bool goingUp;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update ()
    {
        if (Time.deltaTime == 0)
            return;
        if (Input.GetButtonDown(transform.parent.name+" Main")&&floor)
        {
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
            if (Input.GetButton(transform.parent.name + " Main") &&goingUp)
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
            floor = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.name.Equals("Plane"))
            floor = false;
    }
}
