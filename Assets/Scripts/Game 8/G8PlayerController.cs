using UnityEngine;
using System.Collections;

public class G8PlayerController : MonoBehaviour {
	public Material goldBody;
	public Material goldEye;
	public Material originalBody;
	public Material originalEye;
	public GameObject kakuna;
    public bool harden;
    public bool dead;
    public const float maxHealth = 100;
    public float currentHealth = maxHealth;
    public RectTransform healthBar;

	public int id;
	public bool comPlayer;

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
			dead = true;
			Material[] mats = new Material[]{originalBody, originalEye};
			kakuna.GetComponent<SkinnedMeshRenderer>().materials = mats;
            string name = transform.parent.name;
            GameObject.Find("RockSpawns").transform.Find(name).gameObject.SetActive(false);
            foreach (GameObject rock in GameObject.FindGameObjectsWithTag("Rock "+name.Substring(name.Length - 1)))
            {
                Destroy(rock);
            }
            gameObject.GetComponent<Animation>().Play("Dead",PlayMode.StopAll);
			GetComponentInChildren<Animator> ().SetBool ("Harden", true);

			int idWinner=-1;
			int playersAlive=0;
			foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
			{
				if (!player.GetComponent<G8PlayerController> ().dead) 
				{
					playersAlive++;
					idWinner = player.GetComponent<G8PlayerController> ().id;
				}
			}
			if (playersAlive==1)
			{
				bool[] winners = new bool[4];
				for (int i = 0; i < 4; i++) 
				{
					winners [i] = i == idWinner;
				}
				GameObject.FindGameObjectWithTag("InGameMenu").GetComponent<PostGameScript> ().Winner (winners);
			}
        }

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }
    // Use this for initialization
    void Start () {
        harden = false;
		dead = false;
		comPlayer = !GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>().players[id];
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (dead || Time.timeScale == 0)
			return;
		if (!comPlayer) {
			if (Input.GetButton (transform.parent.name + " Main")) {
				if (!harden && !gameObject.GetComponent<Animation> ().IsPlaying ("Impact")) {
					Material[] mats = new Material[]{goldBody, goldEye};
					kakuna.GetComponent<SkinnedMeshRenderer>().materials = mats;
					harden = true;
				}
				TakeDamage (Time.deltaTime * 10);
			}
			if (Input.GetButtonUp (transform.parent.name + " Main") && harden) {
				Material[] mats = new Material[]{originalBody, originalEye};
				kakuna.GetComponent<SkinnedMeshRenderer>().materials = mats;
				harden = false;
			}
		} else 
		{
			//IA
		}
		GetComponentInChildren<Animator> ().SetBool ("Harden", harden);
	}
}
