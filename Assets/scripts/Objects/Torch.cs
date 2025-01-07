using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject FireParent;
    private bool isActive;

    private void Awake() {
        isActive = false;
        FireParent.gameObject.SetActive(false);
    }

    public void TurnOnOff(float fluidCost)
    {
        if (!isActive) // If the torch is being turned on
        {
            // Check if there's enough fluid to light the torch
            if (LighterFluidManager.Instance.healthAmount > fluidCost)
            {
                FireParent.gameObject.SetActive(true); // Turn on the fire
                LighterFluidManager.Instance.UseLighterFluid(fluidCost); // Consume fluid
                isActive = true;
            }
            else
            {
                Debug.Log("Not enough lighter fluid to light the torch!");
            }
        }
        else // If the torch is being turned off
        {
            FireParent.gameObject.SetActive(false); // Turn off the fire
            isActive = false;
        }
    }
}
