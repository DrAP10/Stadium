using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIconScript : MonoBehaviour
{
    public SpriteRenderer P1Sprite;
    public SpriteRenderer P2Sprite;
    public SpriteRenderer P3Sprite;
    public SpriteRenderer P4Sprite;

    public Sprite P1Tex;
    public Sprite P2Tex;
    public Sprite P3Tex;
    public Sprite P4Tex;

    public Sprite P1ComTex;
    public Sprite P2ComTex;
    public Sprite P3ComTex;
    public Sprite P4ComTex;

    // Use this for initialization
    void Start ()
    {
        GameState gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        P1Sprite.sprite = (gameState.players[0]) ? P1Tex : P1ComTex;
        P2Sprite.sprite = (gameState.players[1]) ? P2Tex : P2ComTex;
        P3Sprite.sprite = (gameState.players[2]) ? P3Tex : P3ComTex;
        P4Sprite.sprite = (gameState.players[3]) ? P4Tex : P4ComTex;
    }
}
