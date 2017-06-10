using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionPanel : MonoBehaviour {
	public Sprite p1Texture;
	public Sprite p2Texture;
	public Sprite p3Texture;
	public Sprite p4Texture;
	public Sprite p1ComTexture;
	public Sprite p2ComTexture;
	public Sprite p3ComTexture;
	public Sprite p4ComTexture;

	public Image p1Icon;
	public Image p2Icon;
	public Image p3Icon;
	public Image p4Icon;

	public GameObject p1Text;
	public GameObject p1Nombre;
	public GameObject p2Text;
	public GameObject p2Nombre;
	public GameObject p3Text;
	public GameObject p3Nombre;
	public GameObject p4Text;
	public GameObject p4Nombre;
    
	bool p1Com;
	bool p2Com;
	bool p3Com;
	bool p4Com;

    bool edit = false;
    GameState gameState;

    // Use this for initialization
    void Start ()
	{
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
		p1Com = !gameState.players[0];
		p1Icon.sprite = (p1Com) ? p1ComTexture : p1Texture;
        p1Text.SetActive(p1Com);
        p1Nombre.SetActive(!p1Com);

        p2Com = !gameState.players[1];
		p2Icon.sprite = (p2Com) ? p2ComTexture : p2Texture;
        p2Text.SetActive(p2Com);
        p2Nombre.SetActive(!p2Com);

        p3Com = !gameState.players[2];
		p3Icon.sprite = (p3Com) ? p3ComTexture : p3Texture;
        p3Text.SetActive(p3Com);
        p3Nombre.SetActive(!p3Com);

        p4Com = !gameState.players[3];
		p4Icon.sprite = (p4Com) ? p4ComTexture : p4Texture;
        p4Text.SetActive(p4Com);
        p4Nombre.SetActive(!p4Com);

        string p1String = PlayerPrefs.GetString("Player1", "Player 1");
        PlayerPrefs.SetString("Player1", p1String);
        p1Nombre.GetComponent<InputField>().text = p1String;

        string p2String = PlayerPrefs.GetString("Player2", "Player 2");
        PlayerPrefs.SetString("Player2", p2String);
        p2Nombre.GetComponent<InputField>().text = p2String;

        string p3String = PlayerPrefs.GetString("Player3", "Player 3");
        PlayerPrefs.SetString("Player3", p3String);
        p3Nombre.GetComponent<InputField>().text = p3String;

        string p4String = PlayerPrefs.GetString("Player4", "Player 4");
        PlayerPrefs.SetString("Player4", p4String);
        p4Nombre.GetComponent<InputField>().text = p4String;
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (edit)
            return;
		if (Input.GetButtonDown ("P1 Main")) {
			if (p1Com) 
				p1Icon.sprite = p1Texture;
			else
				p1Icon.sprite = p1ComTexture;
            gameState.players[0]=p1Com;
            p1Com = !p1Com;
			p1Text.SetActive (p1Com);
			p1Nombre.SetActive (!p1Com);
		}
		if (Input.GetButtonDown ("P2 Main")) {
			if (p2Com)
				p2Icon.sprite = p2Texture;
			else
				p2Icon.sprite = p2ComTexture;
            gameState.players[1] = p2Com;
            p2Com = !p2Com;
			p2Text.SetActive (p2Com);
			p2Nombre.SetActive (!p2Com);
		}
		if (Input.GetButtonDown ("P3 Main")) {
			if (p3Com)
				p3Icon.sprite = p3Texture;
			else
				p3Icon.sprite = p3ComTexture;
            gameState.players[2] = p3Com;
            p3Com = !p3Com;
			p3Text.SetActive (p3Com);
			p3Nombre.SetActive (!p3Com);
		}
		if (Input.GetButtonDown ("P4 Main")) {
			if (p4Com)
				p4Icon.sprite = p4Texture;
			else
				p4Icon.sprite = p4ComTexture;
            gameState.players[3] = p4Com;
            p4Com = !p4Com;
			p4Text.SetActive (p4Com);
			p4Nombre.SetActive (!p4Com);
		}
        
        if (Input.GetButtonDown("Continue"))
        {
            Continue();
        }
		if (Input.GetButtonDown ("Cancel"))
			Camera.main.GetComponent<MenuScript> ().GoToModeSelection ();
	}

    public void Continue()
    {
        PlayerPrefs.SetString("Player1", p1Nombre.GetComponent<InputField>().text);
        PlayerPrefs.SetString("Player2", p2Nombre.GetComponent<InputField>().text);
        PlayerPrefs.SetString("Player3", p3Nombre.GetComponent<InputField>().text);
        PlayerPrefs.SetString("Player4", p4Nombre.GetComponent<InputField>().text);
        Camera.main.GetComponent<MenuScript>().GoToGameSelection();
    }

    public void StartEdit()
    {
        edit = true;
    }
    public void EndEdit()
    {
        edit = false;
        Input.GetButtonDown("Continue");//Consumo el buttonDown para que no lo tenga en cuenta hasta que levante y vuelva a pulsar
    }
}
