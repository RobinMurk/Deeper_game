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
            DontDestroyOnLoad(gameObject); // Make persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Avoid duplicates
        }
    }
}

