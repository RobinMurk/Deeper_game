using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;
    public GameObject LighterFluidBar;
    public GameObject Inventory;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void WinLevel()
    {
        LighterFluidBar.SetActive(false);
        Inventory.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; // Show the cursor
    }
}
