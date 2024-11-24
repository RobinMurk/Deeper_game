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
    public GameObject Gate;
    private void Awake()
    {
        Instance = this;
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
            OpenDoor();
        }
    }

    public bool IsLevelComplete()
    {
        return isLevelComplete;
    }

    private void OpenDoor()
    {
        Gate.SetActive(false);
        FindObjectOfType<AudioManager>().Play("DoorOpen");
    }

    public void LoadNextLevel()
    {
        WinView.SetActive(true);
        WinManager.Instance.WinLevel();
    }
}
