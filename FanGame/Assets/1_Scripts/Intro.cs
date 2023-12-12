using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private void Start()
    {
        Invoke("MainScence", 6f);
    }

    public void MainScence()
    {
        SceneManager.LoadScene("Game");
    }
}
