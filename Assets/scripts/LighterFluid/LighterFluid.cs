using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterFluid : MonoBehaviour
{
    public enum FluidType { Yellow, Blue, Green }
    public FluidType fluidType; // Type of fluid
    public float fluidAmount = 2f; // Amount of fluid the pickup will restore

    private void Start()
    {
        // Apply color based on the fluid type
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            switch (fluidType)
            {
                case FluidType.Yellow:
                    renderer.material.color = Color.yellow;
                    break;
                case FluidType.Blue:
                    renderer.material.color = Color.blue;
                    break;
                case FluidType.Green:
                    renderer.material.color = Color.green;
                    break;
            }
        }
    }
}

