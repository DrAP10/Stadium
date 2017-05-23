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

	// Use this for initialization
	void Start ()
	{
		p1Com = false;
		p1Icon.sprite = p1Texture;
		p2Com = true;
		p2Icon.sprite = p2ComTexture;
		p3Com = true;
		p3Icon.sprite = p3ComTexture;
		p4Com = true;
		p4Icon.sprite = p4ComTexture;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("P1 Main")) {
			if (p1Com) 
				p1Icon.sprite = p1Texture;
			else
				p1Icon.sprite = p1ComTexture;
			p1Com = !p1Com;
			p1Text.SetActive (p1Com);
			p1Nombre.SetActive (!p1Com);
		}
		if (Input.GetButtonDown ("P2 Main")) {
			if (p2Com)
				p2Icon.sprite = p2Texture;
			else
				p2Icon.sprite = p2ComTexture;
			p2Com = !p2Com;
			p2Text.SetActive (p2Com);
			p2Nombre.SetActive (!p2Com);
		}
		if (Input.GetButtonDown ("P3 Main")) {
			if (p3Com)
				p3Icon.sprite = p3Texture;
			else
				p3Icon.sprite = p3ComTexture;
			p3Com = !p3Com;
			p3Text.SetActive (p3Com);
			p3Nombre.SetActive (!p3Com);
		}
		if (Input.GetButtonDown ("P4 Main")) {
			if (p4Com)
				p4Icon.sprite = p4Texture;
			else
				p4Icon.sprite = p4ComTexture;
			p4Com = !p4Com;
			p4Text.SetActive (p4Com);
			p4Nombre.SetActive (!p4Com);
		}

		if (Input.GetButtonDown ("Submit"))
			Camera.main.GetComponent<MenuScript> ().GoToGameSelection ();
		if (Input.GetButtonDown ("Cancel"))
			Camera.main.GetComponent<MenuScript> ().GoToModeSelection ();
	}
}
