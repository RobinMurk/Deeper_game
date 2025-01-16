using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterFluid : MonoBehaviour
{
    public enum FluidType {
        Regular, BlueSpark, GreenWhisper, SolarFlame, CrystalBlaze, VioletEmber, GoldenFire, LunarGlow, SilverTorch, AzureRadiance, // Regular Fluids
        TrapCrimsonLeak, TrapDarkMist, TrapShadowSpark, TrapBlackFlame, TrapInfernalEmber, TrapRottenGlow, TrapVoidEssence, TrapCorrosiveLight, TrapBurntWisp, TrapPhantomFlame, // Trapped Fluids
        Random
    }
    public bool isTrap;
    public FluidType fluidType; // Type of fluid
    public float fluidAmount = 2f; // Amount of fluid the pickup will restore

    private void Start()
    {
        // Apply color based on the fluid type
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            if (fluidType == FluidType.Random)
            {
                // Pick a random fluid type from the FluidType enum excluding FluidType.Random
                fluidType = (FluidType)Random.Range(0, System.Enum.GetValues(typeof(FluidType)).Length - 1);
            }

            switch (fluidType)
            {
                case FluidType.Regular:
                    renderer.material.color = Color.yellow;
                    isTrap = false;
                    break;
                case FluidType.BlueSpark:
                    renderer.material.color = new Color(0.53f, 0.81f, 0.92f); // Light Blue
                    isTrap = false;
                    break;
                case FluidType.GreenWhisper:
                    renderer.material.color = new Color(0.2f, 0.8f, 0.2f); // Lime Green
                    isTrap = false;
                    break;
                case FluidType.SolarFlame:
                    renderer.material.color = new Color(1.0f, 0.84f, 0.0f); // Golden Yellow
                    isTrap = false;
                    break;
                case FluidType.CrystalBlaze:
                    renderer.material.color = Color.cyan; // Cyan
                    isTrap = false;
                    break;
                case FluidType.VioletEmber:
                    renderer.material.color = new Color(0.54f, 0.17f, 0.89f); // Purple
                    isTrap = false;
                    break;
                case FluidType.GoldenFire:
                    renderer.material.color = new Color(1.0f, 0.84f, 0.0f); // Bright Gold
                    isTrap = false;
                    break;
                case FluidType.LunarGlow:
                    renderer.material.color = new Color(0.96f, 0.96f, 0.96f); // Soft White
                    isTrap = false;
                    break;
                case FluidType.SilverTorch:
                    renderer.material.color = new Color(0.75f, 0.75f, 0.75f); // Silver
                    isTrap = false;
                    break;
                case FluidType.AzureRadiance:
                    renderer.material.color = new Color(0.27f, 0.51f, 0.71f); // Deep Blue
                    isTrap = false;
                    break;

                // Trapped Fluids
                case FluidType.TrapCrimsonLeak:
                    renderer.material.color = new Color(0.86f, 0.08f, 0.24f); // Crimson Red
                    isTrap = true;
                    break;
                case FluidType.TrapDarkMist:
                    renderer.material.color = new Color(0.18f, 0.31f, 0.31f); // Dark Gray
                    isTrap = true;
                    break;
                case FluidType.TrapShadowSpark:
                    renderer.material.color = Color.black; // Black
                    isTrap = true;
                    break;
                case FluidType.TrapBlackFlame:
                    renderer.material.color = new Color(0.11f, 0.11f, 0.11f); // Charcoal
                    isTrap = true;
                    break;
                case FluidType.TrapInfernalEmber:
                    renderer.material.color = new Color(0.55f, 0.0f, 0.0f); // Deep Red
                    isTrap = true;
                    break;
                case FluidType.TrapRottenGlow:
                    renderer.material.color = new Color(0.33f, 0.42f, 0.18f); // Muddy Green
                    isTrap = true;
                    break;
                case FluidType.TrapVoidEssence:
                    renderer.material.color = new Color(0.29f, 0.0f, 0.51f); // Void Purple
                    isTrap = true;
                    break;
                case FluidType.TrapCorrosiveLight:
                    renderer.material.color = new Color(0.6f, 0.8f, 0.2f); // Acidic Yellow
                    isTrap = true;
                    break;
                case FluidType.TrapBurntWisp:
                    renderer.material.color = new Color(0.41f, 0.41f, 0.41f); // Smoky Gray
                    isTrap = true;
                    break;
                case FluidType.TrapPhantomFlame:
                    renderer.material.color = new Color(0.86f, 0.86f, 0.86f); // Ghostly Pale
                    isTrap = true;
                    break;
            }
        }
    }
}

