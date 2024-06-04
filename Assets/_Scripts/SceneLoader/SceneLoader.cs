using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        FindObjectOfType<ApplicationManager>().SceneChange.Raise();
        SceneManager.LoadScene("MagicHouse"); // doing straightfoward scene load for test. 
    }

    internal void LoadCookingScene()
    {
        FindObjectOfType<ApplicationManager>().SceneChange.Raise();
        SceneManager.LoadScene("CookingScene");
    }
}
