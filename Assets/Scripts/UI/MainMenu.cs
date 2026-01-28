using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public TMP_InputField inputField;


    public void Play()
    {
        string userInput = inputField.text;
        if (userInput != "")
        {
            PlayerPrefs.SetString("PlayerName", userInput);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


}
