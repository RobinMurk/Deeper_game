using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelMaster : MonoBehaviour
{
    public static levelMaster Instance;
    private Piller[] pillars;         // Array to hold all pillars in the level
    private int booksPlacedCount = 0; // Counter for books placed on pillars
    private bool isLevelComplete = false;

    public GameObject WinView;

    private void Awake()
    {
        Instance = this;
    }

    public int booksCount(){
        return booksPlacedCount;
    }

    private void Start()
    {
        // Find all Pillar objects in the scene
        pillars = FindObjectsOfType<Piller>();

        // Subscribe to each pillar's event for book placement
        foreach (Piller pillar in pillars)
        {
            pillar.OnBookPlaced += HandleBookPlaced;
        }
    }

    // This method is called whenever a book is placed on a pillar
    // FIXME: Üks piller kutsub seda funktsiooni kaks korda
    private void HandleBookPlaced()
    {
        if (isLevelComplete) return;

        booksPlacedCount++;
        EventListener.Instance.Interact();
        CheckLevelCompletion();
    }

    // Check if all books have been placed
    private void CheckLevelCompletion()
    {
        if (booksPlacedCount >= pillars.Length)
        {
            isLevelComplete = true;
            Debug.Log("Level Complete! All books have been placed.");
            GateDoor.Instance.OpenGate();
        }
    }

    public bool IsLevelComplete()
    {
        return isLevelComplete;
    }

    public void LoadNextLevel()
    {
        WinView.SetActive(true);
        WinManager.Instance.WinLevel();
    }

    public void Lose()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Show the cursor
        GameObject.Find("UI").transform.Find("DeathView").gameObject.SetActive(true);
        GameObject.Find("UI").transform.Find("LighterFluidBar ").gameObject.SetActive(false);
        //GameObject.Find("UI").transform.Find("Inventory ").gameObject.SetActive(false);

    }
}
