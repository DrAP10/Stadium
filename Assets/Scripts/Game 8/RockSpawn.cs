using UnityEngine;
using System.Collections;

public class RockSpawn : MonoBehaviour {
    float time;
    public GameObject rock;
    public Transform[] players;

    int difficulty;
    float[] minRange = { 0.5f, 0.3f, 0.1f };
    float[] maxRange = { 5f, 3f, 1f };

    // Use this for initialization
    void Start () {
        time = 0f;
        difficulty = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().AIDifficulty;
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            time = Random.Range(minRange[difficulty], maxRange[difficulty]);
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
