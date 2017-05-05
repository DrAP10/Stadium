using UnityEngine;
using System.Collections;

public class G8PlayerController : MonoBehaviour {
    public Material gold;
    public Material color;
    public bool harden;
    bool dead;
    public const float maxHealth = 100;
    public float currentHealth = maxHealth;
    public RectTransform healthBar;

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
            dead = true;
            gameObject.GetComponent<Renderer>().material = color;
            string name = transform.parent.name;
            GameObject.Find("RockSpawns").transform.Find(name).gameObject.SetActive(false);
            foreach (GameObject rock in GameObject.FindGameObjectsWithTag("Rock "+name.Substring(name.Length - 1)))
            {
                Destroy(rock);
            }
            gameObject.GetComponent<Animation>().Play("Dead",PlayMode.StopAll);
        }

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }
    // Use this for initialization
    void Start () {
        harden = false;
        dead = false;
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetButtonDown("Fire1")&&!gameObject.GetComponent<Animation>().IsPlaying("Impact"))
        {
            gameObject.GetComponent<Renderer>().material = gold;
            fortaleza = true;
        }*/
        if (dead)
            return;
        if (Input.GetButton(transform.parent.name+" harden"))
        {
            if(!harden && !gameObject.GetComponent<Animation>().IsPlaying("Impact"))
            {
                gameObject.GetComponent<Renderer>().material = gold;
                harden = true;
            }
            TakeDamage(Time.deltaTime * 10);
        }
        if (Input.GetButtonUp(transform.parent.name + " harden") &&harden)
        {
            gameObject.GetComponent<Renderer>().material = color;
            harden = false;
        }
    }
}
