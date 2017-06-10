using UnityEngine;
using System.Collections;

public class G5PlayerController : MonoBehaviour {
    int progress = 0;
    public bool currentBlue;
    public RectTransform progressBar;
    float time = 0;
    bool loading;

    public int id;
	public bool comPlayer;

    float totalTime;

	//AI
	float next=0;

    // Use this for initialization
    void Start () {
		progressBar.sizeDelta = new Vector2(progress, progressBar.sizeDelta.y);
		comPlayer = !GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().players[id];
        totalTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
            return;
        totalTime += Time.deltaTime;
		if (!comPlayer)
        {
            if (Input.GetButtonDown (transform.name + " Main") && currentBlue) {
				TakeDamage (2);
				loading = true;
				time = 0;
			}
			if (Input.GetButtonDown (transform.name + " Secondary") && !currentBlue) {
				TakeDamage (2);
				loading = true;
				time = 0;
			}
			time += Time.deltaTime;
			if (time > 0.5f) {
				TakeDamage (-2);
				time = 0;
                loading = false;
			}
		} 
		else //AI
		{
			next-=Time.deltaTime;
			if(next<=0)
			{
				if (Random.Range (0, 10) != 1)//90%
				{
					TakeDamage (2);
					loading = true;
				} 
				else 
				{
					TakeDamage (-2);
                    loading = false;
				}
				next = Random.Range (0.1f, 0.2f);
			}
		}
		GetComponentInChildren<Animator> ().SetBool ("Loading", loading);
    }

    public void TakeDamage(int amount)
    {
        progress += amount;
        if (progress < 0)
            progress = 0;
        if (progress >= 100)
        {
            progress = 100;
			GetComponentInChildren<Animator> ().SetBool ("Loading", false);
            Debug.Log("Win!");
			bool[] winners = new bool[4];
			for (int i = 0; i < 4; i++) 
			{
				winners [i] = i == id;
			}
			GameObject.FindGameObjectWithTag("InGameMenu").GetComponent<PostGameScript> ().Winner (winners, 1, false, totalTime, 0, 5);
            //GameObject.Find("ColorBall").SetActive(false);
            //gameObject.GetComponent<Animation>().Play("Dead", PlayMode.StopAll);
        }

        progressBar.sizeDelta = new Vector2(progress, progressBar.sizeDelta.y);
    }
}
