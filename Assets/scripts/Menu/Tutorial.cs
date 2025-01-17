using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update

    public static Tutorial Instance;

    public TextMeshProUGUI tutorialText;

    private void Awake()
    {
        // Ensure there's only one instance of this script
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DisplayControls();
    }

    public void DisplayControls()
    {
        if (tutorialText != null)
        {
            tutorialText.text = "Use WASD or Arrow Keys to move, Shift to run, and Ctrl to crouch. Press F to interact and E to turn lights on and off!";
        }
    }

    public void FindBook()
    {
        if (tutorialText != null)
        {
            tutorialText.text = "Find the missing book!";
        }
    }

    public void FindMainRoomPillar()
    {
        if (tutorialText != null)
        {
            tutorialText.text = "Locate the main room pillar to place the book.";
        }
    }

    public void FindExit()
    {
        if (tutorialText != null)
        {
            tutorialText.text = "Head towards the exit";
        }
    }

    public void TurnOff()
    {
        tutorialText.gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        tutorialText.gameObject.SetActive(true);
    }
}
