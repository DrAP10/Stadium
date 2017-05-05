using UnityEngine;
using System.Collections;

public class G4BallMovement : MonoBehaviour {
    float totalTime = 0f;
    float angle, speed, radius;
    Vector3 origen;
    bool right = false;
    public Material gold;
    public Material white;
    public bool onSide;
    float speedBase = 0;
    int counter = 0;

    // Use this for initialization
    void Start ()
    {
        origen = new Vector3(0f, 3f, 0f);
        angle = 2.6f;
        radius = 2;
        speed = Mathf.PI / 2;
    }
	
	// Update is called once per frame
	void Update ()
    {
        totalTime += Time.deltaTime;
        if (totalTime < 1f)
            return;
        if(right)
            angle -= speed * Time.deltaTime;
        else
            angle += speed * Time.deltaTime;
        //Debug.Log(angle);
        if (angle < 2.6f || angle > 3.6832f)
            speed = speedBase + Mathf.PI / 4;
        else
            speed = speedBase + Mathf.PI / 2;
        //Debug.Log(speed);
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        if (angle < 2.5f || angle > 3.7832f)
        {
            right = !right;
            counter++;
            if (counter == 10)
            {
                counter = 0;
                speedBase += 0.25f;
                if (speedBase > 1f)
                    speedBase = 1f;
            }
        }
        float y = (Mathf.Cos(angle) * radius) + origen.y;
        float x = (Mathf.Sin(angle) * radius) + origen.x;
        //transform.position=new Vector3(x, y, transform.position.z);
        rigidbody.MovePosition(new Vector3(x, y, rigidbody.position.z));
    }

    void OnTriggerEnter(Collider collider)
    {
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<G4PlayerController>().BallChange(0);
        }
        gameObject.GetComponent<Renderer>().material = white;
        onSide = true;
    }

    void OnTriggerExit(Collider collider)
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<G4PlayerController>().BallChange(1);
        }
        gameObject.GetComponent<Renderer>().material = gold;
        onSide = false;
    }
}
