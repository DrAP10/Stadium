using UnityEngine;
using System.Collections;

public class G4PlayerController : MonoBehaviour {
    int health = 10;
    bool hypno = false;
    bool dead = false;
    bool onSide = false;
    public GameObject zObject;
    public GameObject hypnoObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (dead)
            return;
        if (Input.GetButtonDown(transform.parent.name+" Main"))
        {
            if (onSide)
            {
                hypno = true;
                GameObject o = Instantiate(hypnoObject, transform.Find("Hypno Spawn").position, transform.Find("Hypno Spawn").rotation) as GameObject;
                Destroy(o, 0.2f);
            }
            else
                TakeDemage();
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
            dead = true;
        }
        GameObject o=Instantiate(zObject,transform.Find("Z Spawn").position, transform.Find("Z Spawn").rotation) as GameObject;
        Destroy(o, 0.2f);
        Debug.Log(health);
    }
}
