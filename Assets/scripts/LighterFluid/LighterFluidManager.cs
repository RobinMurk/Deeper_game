using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LighterFluidManager : MonoBehaviour
{
    public static LighterFluidManager Instance;
    public Image healthBar;
    public float healthAmount = 2f;
    public HandLight handLight;

     private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UseLighterFluid(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 2f;
    }

    public void AddLighterFluid(float fluidAmount)
    {
        healthAmount += fluidAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 2f);
        healthBar.fillAmount = healthAmount / 2f;
    }

    public void UseFluid(LighterFluid fluid)
    {
        switch (fluid.fluidType)
        {
            // Regular Fluids
            case LighterFluid.FluidType.Regular:
                Debug.Log("Regular!");
                healthBar.color = Color.yellow;
                break;
            case LighterFluid.FluidType.BlueSpark:
                Debug.Log("BlueSpark used: Fast burning but bright!");
                healthBar.color = new Color(0.53f, 0.81f, 0.92f); // Light Blue
                break;
            case LighterFluid.FluidType.GreenWhisper:
                Debug.Log("GreenWhisper used: Balanced with a hint of nature!");
                healthBar.color = new Color(0.2f, 0.8f, 0.2f); // Lime Green
                break;
            case LighterFluid.FluidType.SolarFlame:
                Debug.Log("SolarFlame used: Extra bright and long-lasting!");
                healthBar.color = new Color(1.0f, 0.84f, 0.0f); // Golden Yellow
                break;
            case LighterFluid.FluidType.CrystalBlaze:
                Debug.Log("CrystalBlaze used: Clean and radiant flame!");
                healthBar.color = Color.cyan; // Cyan
                break;
            case LighterFluid.FluidType.VioletEmber:
                Debug.Log("VioletEmber used: Elegant and moderately bright!");
                healthBar.color = new Color(0.54f, 0.17f, 0.89f); // Purple
                break;
            case LighterFluid.FluidType.GoldenFire:
                Debug.Log("GoldenFire used: High performance and luxurious!");
                healthBar.color = new Color(1.0f, 0.84f, 0.0f); // Bright Gold
                break;
            case LighterFluid.FluidType.LunarGlow:
                Debug.Log("LunarGlow used: Subtle glow with moderate duration!");
                healthBar.color = new Color(0.96f, 0.96f, 0.96f); // Soft White
                break;
            case LighterFluid.FluidType.SilverTorch:
                Debug.Log("SilverTorch used: Sharp and reliable light!");
                healthBar.color = new Color(0.75f, 0.75f, 0.75f); // Silver
                break;
            case LighterFluid.FluidType.AzureRadiance:
                Debug.Log("AzureRadiance used: Calming and steady flame!");
                healthBar.color = new Color(0.27f, 0.51f, 0.71f); // Deep Blue
                break;

            // Trapped Fluids
            case LighterFluid.FluidType.TrapCrimsonLeak:
                Debug.Log("TrapCrimsonLeak used: Flame flickers erratically!");
                healthBar.color = new Color(0.86f, 0.08f, 0.24f); // Crimson Red
                break;
            case LighterFluid.FluidType.TrapDarkMist:
                Debug.Log("TrapDarkMist used: Weak and dim flame!");
                healthBar.color = new Color(0.18f, 0.31f, 0.31f); // Dark Gray
                break;
            case LighterFluid.FluidType.TrapShadowSpark:
                Debug.Log("TrapShadowSpark used: Quick burnout with barely any light!");
                healthBar.color = Color.black; // Black
                break;
            case LighterFluid.FluidType.TrapBlackFlame:
                Debug.Log("TrapBlackFlame used: Dangerous with very low visibility!");
                healthBar.color = new Color(0.11f, 0.11f, 0.11f); // Charcoal
                break;
            case LighterFluid.FluidType.TrapInfernalEmber:
                Debug.Log("TrapInfernalEmber used: Intense but chaotic flame!");
                healthBar.color = new Color(0.55f, 0.0f, 0.0f); // Deep Red
                break;
            case LighterFluid.FluidType.TrapRottenGlow:
                Debug.Log("TrapRottenGlow used: Emits unpleasant fumes!");
                healthBar.color = new Color(0.33f, 0.42f, 0.18f); // Muddy Green
                break;
            case LighterFluid.FluidType.TrapVoidEssence:
                Debug.Log("TrapVoidEssence used: Almost invisible flame!");
                healthBar.color = new Color(0.29f, 0.0f, 0.51f); // Void Purple
                break;
            case LighterFluid.FluidType.TrapCorrosiveLight:
                Debug.Log("TrapCorrosiveLight used: Weak but corrosive flame!");
                healthBar.color = new Color(0.6f, 0.8f, 0.2f); // Acidic Yellow
                break;
            case LighterFluid.FluidType.TrapBurntWisp:
                Debug.Log("TrapBurntWisp used: Fades out quickly!");
                healthBar.color = new Color(0.41f, 0.41f, 0.41f); // Smoky Gray
                break;
            case LighterFluid.FluidType.TrapPhantomFlame:
                Debug.Log("TrapPhantomFlame used: Creates a ghostly glow!");
                healthBar.color = new Color(0.86f, 0.86f, 0.86f); // Ghostly Pale
                break;
        }
        handLight.ApplyFluidEffect(fluid);  
        AddLighterFluid(fluid.fluidAmount);
    }
}
