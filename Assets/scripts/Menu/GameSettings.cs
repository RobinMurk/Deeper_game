using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance; // Singleton instance
    public float mouseSensitivity = 2f; // Default sensitivity

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Load the saved sensitivity value or set a default
            mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 2f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            // Save the sensitivity value when the game exits or the object is destroyed
            PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
            PlayerPrefs.Save();
        }
    }

    public static void DestroyGameSettings()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }
    }
}
