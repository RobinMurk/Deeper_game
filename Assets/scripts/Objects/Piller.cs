using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Piller : MonoBehaviour
{
    public GameObject book;
    public event Action OnBookPlaced;

    public bool bookPlaced = false;

    private void Start()
    {
        book.SetActive(false); // Ensure the book is hidden initially
    }

    public void PlaceBook()
    {
        if (!bookPlaced)
        {
            Debug.Log("placed");
            FindObjectOfType<AudioManager>().Play("PickupSound");
            book.SetActive(true); // Show the book on the pillar
            bookPlaced = true; // Mark the book as placed to prevent multiple triggers
            OnBookPlaced?.Invoke(); // Notify listeners
        }
    }

}
