using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameSettings.DestroyGameSettings();
        SceneManager.LoadScene("MVP");
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
