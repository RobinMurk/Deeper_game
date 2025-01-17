using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public GameObject pauseMenuUI; // Optional reference to the Pause Menu Canvas
    public GameObject LighterFluidBar;
    public GameObject Inventory;
    private bool isPaused = false; // Tracks whether the game is paused

    void Update()
    {
        // Toggle pause when the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PauseGame()
    {
        // Return early if no Pause Menu UI is assigned
        if (pauseMenuUI == null) return;

        Tutorial.Instance.TurnOff();

        LighterFluidBar.SetActive(false);
        Inventory.SetActive(false);
        Time.timeScale = 0; // Freeze the game
        isPaused = true;

        pauseMenuUI.SetActive(true); // Show the Pause Menu
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Show the cursor
    }

    public void ResumeGame()
    {
        // Return early if no Pause Menu UI is assigned
        if (pauseMenuUI == null) return;

        Tutorial.Instance.TurnOn();

        LighterFluidBar.SetActive(true);
        Inventory.SetActive(true);
        Time.timeScale = 1; // Resume the game
        isPaused = false;

        pauseMenuUI.SetActive(false); // Hide the Pause Menu
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); // Quit the application
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
