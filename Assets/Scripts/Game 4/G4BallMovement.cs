using UnityEngine;
using System.Collections;

public class G4BallMovement : MonoBehaviour {
    float totalTime = 0f;
    float angle, speed;
    bool right = false;
    public Material gold;
    public Material white;
    public bool onSide;
    float speedBase = 0;
    int counter = 0;

    // Use this for initialization
    void Start ()
    {
        angle = 2.6f;
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
        //Debug.Log(angle);
        //Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        if (angle < 2.5f || angle > 3.7832f)
        {
            right = !right;
            counter++;
            if (counter == 10)
            {
                counter = 0;
                speedBase += 0.25f;
            }
        }
        transform.localEulerAngles = new Vector3(0f, 0f, (angle * 180 / Mathf.PI) + 180);
    }
}
