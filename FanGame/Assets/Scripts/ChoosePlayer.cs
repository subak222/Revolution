using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePlayer : MonoBehaviour
{
	[SerializeField]
	private Sprite[]		character;

    private GameObject      player;
    private int             playerCount;
    
    private void Awake()
    {
        playerCount = character.Length;
        player = GameObject.Find("ChoosePlayer");
        player.GetComponent<Image>().sprite = character[0];   
    }

    private void ChangeCharacter()
    {
        player.GetComponent<Image>().sprite = character[playerCount];
        Debug.Log(playerCount);
    }

    public void ClickLeft()
    {
        if (playerCount - 1 < 0)
        {
            playerCount = character.Length-1;
            ChangeCharacter();
        }
        else
        {
            playerCount--;
            ChangeCharacter();
        }
    }

    public void ClickRight()
    {
        if (playerCount < character.Length - 1)
        {
            ++playerCount;
            ChangeCharacter();
        }
        else
        {
            playerCount = 0;
            ChangeCharacter();
        }
    }
}
