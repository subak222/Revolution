using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    public float normal = 0.12f;
    public float special = 0.06f;
    private ChoosePlayer choosePlayer;

    private void Start()
    {
        choosePlayer = GameObject.Find("GameController").GetComponent<ChoosePlayer>();    
    }

    public void Update()
    {
        transform.localScale = Vector3.zero;
        transform.localScale = choosePlayer.playerCount == 3 ? new Vector3(special, special, special) : new Vector3(normal,normal,normal);
    }
}
