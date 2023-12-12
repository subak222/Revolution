using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSprite : MonoBehaviour
{
	private GameObject      player;

	private ChoosePlayer	choosePlayer;

    private void Start()
	{
        choosePlayer = GameObject.Find("GameController").GetComponent<ChoosePlayer>();
	}

    public void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = choosePlayer.character[choosePlayer.playerCount];
    }
}
