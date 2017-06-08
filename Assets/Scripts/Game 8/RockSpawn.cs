using UnityEngine;
using System.Collections;

public class RockSpawn : MonoBehaviour {
    float time;
    public GameObject rock;
    public Transform[] players;
    // Use this for initialization
    void Start () {
        time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            time = Random.Range(0.5f, 5f);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    GameObject rockObject = Instantiate(rock, transform.GetChild(i).gameObject.transform.position,
                        transform.GetChild(i).gameObject.transform.rotation) as GameObject;
                    rockObject.transform.tag = "Rock " + (i + 1).ToString();
                    rockObject.transform.parent = players[i];
                }
            }
        }
	}
}
