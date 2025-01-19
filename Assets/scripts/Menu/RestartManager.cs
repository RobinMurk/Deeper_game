using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    public void ResetScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        SceneManager.LoadScene("level1");
    }

    public void ResetScene2()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        SceneManager.LoadScene("level2");
    }

    public void MainMenuScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
