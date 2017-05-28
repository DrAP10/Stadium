using UnityEngine;
using System.Collections;

public class G5PlayerController : MonoBehaviour {
    int progress = 0;
    public bool currentBlue;
    public RectTransform progressBar;
    float time = 0;
    // Use this for initialization
    void Start () {
        progressBar.sizeDelta = new Vector2(progress, progressBar.sizeDelta.y);
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
            return;
        if (Input.GetButtonDown(transform.name+" Main")&&currentBlue)
        {
            TakeDamage(2);
            time = 0;
        }
        if (Input.GetButtonDown(transform.name + " Secondary") && !currentBlue)
        {
            TakeDamage(2);
            time = 0;
        }
        time += Time.deltaTime;
        if (time > 0.5f)
        {
            TakeDamage(-2);
            time = 0;
        }
    }

    public void TakeDamage(int amount)
    {
        progress += amount;
        if (progress < 0)
            progress = 0;
        if (progress >= 100)
        {
            progress = 100;
            Debug.Log("Win!");
            //GameObject.Find("ColorBall").SetActive(false);
            //gameObject.GetComponent<Animation>().Play("Dead", PlayMode.StopAll);
        }

        progressBar.sizeDelta = new Vector2(progress, progressBar.sizeDelta.y);
    }
}
