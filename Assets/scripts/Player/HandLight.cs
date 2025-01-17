using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HandLight : MonoBehaviour
{
    public static HandLight Instance;

    public float IntensityDelay;
    [Range(0, 0.3f)]
    public float IntensityAmmountToRemove;
    private float InternalTime;
    public float currentIntensity;
    public bool LightOn;

    private void Awake() {
        Instance = this;
        IntensityDelay = 0.1f;
        IntensityAmmountToRemove = 0.001f;
        SetLightColor(Color.yellow);
    }
    // Start is called before the first frame update
    void Start()
    {

        if(GetLight() != null){
            GetLight().intensity = 2;
            currentIntensity = GetLight().intensity;
        }
        LightOn = true;
        InternalTime = Time.time + IntensityDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= InternalTime && LightOn){
            DecreaseIntensity(IntensityAmmountToRemove);
        }
    }

    public void TurnOnOff(){
        
        if(gameObject.activeSelf) // Turn Off
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        else // Turn On
        {
            gameObject.SetActive(!gameObject.activeSelf);
            InternalTime = Time.time + IntensityDelay;
        }
        
    }

    public void DecreaseIntensity(float amount)
    {
        Light lightSource = GetLight();
        if (lightSource != null && lightSource.intensity > 0)
        {
            // Check if enough lighter fluid is available
            if (LighterFluidManager.Instance.healthAmount > 0)
            {
                gameObject.SetActive(true);
                lightSource.intensity -= amount;
                LighterFluidManager.Instance.UseLighterFluid(amount);
                InternalTime += IntensityDelay;
                currentIntensity = lightSource.intensity;
            }
            else
            {
                // If no fluid is left, turn off the light
                lightSource.intensity = 0;
                currentIntensity = 0;
                gameObject.SetActive(false); // Turn off the light object
            }
        }
    }
    public Light GetLight(){
        return gameObject.GetComponentInChildren<Light>();
    }

    public void SetLightColor(Color color)
    {
        Light lightSource = GetLight();
        lightSource.color = color;
    }

    public void enemyApproaching(bool close)
    {
        Light lightSource = GetLight();
        if(close)
            lightSource.color = new Color(0.6f, 0, 1f, 1);
        else
            lightSource.color = new Color(0.95f, 0.67f, 0.33f, 1);
    }

    public void ApplyFluidEffect(LighterFluid fluid)
    {
        Light lightSource = GetLight();

        if (lightSource != null)
        {
            if (fluid.isTrap)
            {
                switch (fluid.fluidType)
                {
                    // Trapped Fluids
                    case LighterFluid.FluidType.TrapCrimsonLeak:
                        IntensityAmmountToRemove = 0.0032f;
                        lightSource.intensity = 1.5f;
                        SetLightColor(new Color(0.86f, 0.08f, 0.24f)); // Crimson Red
                        break;

                    case LighterFluid.FluidType.TrapDarkMist:
                        IntensityAmmountToRemove = 0.004f;
                        lightSource.intensity = 1f;
                        SetLightColor(new Color(0.18f, 0.31f, 0.31f)); // Dark Gray
                        break;

                    case LighterFluid.FluidType.TrapShadowSpark:
                        IntensityAmmountToRemove = 0.0048f;
                        lightSource.intensity = 0.8f;
                        SetLightColor(Color.black); // Black
                        break;

                    case LighterFluid.FluidType.TrapBlackFlame:
                        IntensityAmmountToRemove = 0.00448f;
                        lightSource.intensity = 1.2f;
                        SetLightColor(new Color(0.11f, 0.11f, 0.11f)); // Charcoal
                        break;

                    case LighterFluid.FluidType.TrapInfernalEmber:
                        IntensityAmmountToRemove = 0.00352f;
                        lightSource.intensity = 2.8f;
                        SetLightColor(new Color(0.55f, 0.0f, 0.0f)); // Deep Red
                        break;

                    case LighterFluid.FluidType.TrapRottenGlow:
                        IntensityAmmountToRemove = 0.00432f;
                        lightSource.intensity = 1.3f;
                        SetLightColor(new Color(0.33f, 0.42f, 0.18f)); // Muddy Green
                        break;

                    case LighterFluid.FluidType.TrapVoidEssence:
                        IntensityAmmountToRemove = 0.0048f;
                        lightSource.intensity = 0.9f;
                        SetLightColor(new Color(0.29f, 0.0f, 0.51f)); // Void Purple
                        break;

                    case LighterFluid.FluidType.TrapCorrosiveLight:
                        IntensityAmmountToRemove = 0.0056f;
                        lightSource.intensity = 1.1f;
                        SetLightColor(new Color(0.6f, 0.8f, 0.2f)); // Acidic Yellow
                        break;

                    case LighterFluid.FluidType.TrapBurntWisp:
                        IntensityAmmountToRemove = 0.00448f;
                        lightSource.intensity = 0.7f;
                        SetLightColor(new Color(0.41f, 0.41f, 0.41f)); // Smoky Gray
                        break;

                    case LighterFluid.FluidType.TrapPhantomFlame:
                        IntensityAmmountToRemove = 0.004f;
                        lightSource.intensity = 1.8f;
                        SetLightColor(new Color(0.86f, 0.86f, 0.86f)); // Ghostly Pale
                        break;
                    }

                    Debug.Log("Triger enemy here!!!");
            }
            else
            {
                switch (fluid.fluidType)
                {
                    // Regular Fluids
                    case LighterFluid.FluidType.Regular:
                        IntensityAmmountToRemove = 0.0016f;
                        lightSource.intensity = 2f;
                        SetLightColor(Color.yellow);
                        break;

                    case LighterFluid.FluidType.BlueSpark:
                        IntensityAmmountToRemove = 0.0032f;
                        lightSource.intensity = 4f;
                        SetLightColor(new Color(0.53f, 0.81f, 0.92f)); // Light Blue
                        break;

                    case LighterFluid.FluidType.GreenWhisper:
                        IntensityAmmountToRemove = 0.0024f;
                        lightSource.intensity = 2f;
                        SetLightColor(new Color(0.2f, 0.8f, 0.2f)); // Lime Green
                        break;

                    case LighterFluid.FluidType.SolarFlame:
                        IntensityAmmountToRemove = 0.0016f;
                        lightSource.intensity = 3f;
                        SetLightColor(new Color(1.0f, 0.84f, 0.0f)); // Golden Yellow
                        break;

                    case LighterFluid.FluidType.CrystalBlaze:
                        IntensityAmmountToRemove = 0.00128f;
                        lightSource.intensity = 4.5f;
                        SetLightColor(Color.cyan); // Cyan
                        break;

                    case LighterFluid.FluidType.VioletEmber:
                        IntensityAmmountToRemove = 0.0024f;
                        lightSource.intensity = 2.5f;
                        SetLightColor(new Color(0.54f, 0.17f, 0.89f)); // Purple
                        break;

                    case LighterFluid.FluidType.GoldenFire:
                        IntensityAmmountToRemove = 0.00192f;
                        lightSource.intensity = 3.5f;
                        SetLightColor(new Color(1.0f, 0.84f, 0.0f)); // Bright Gold
                        break;

                    case LighterFluid.FluidType.LunarGlow:
                        IntensityAmmountToRemove = 0.0024f;
                        lightSource.intensity = 2.2f;
                        SetLightColor(new Color(0.96f, 0.96f, 0.96f)); // Soft White
                        break;

                    case LighterFluid.FluidType.SilverTorch:
                        IntensityAmmountToRemove = 0.0016f;
                        lightSource.intensity = 3f;
                        SetLightColor(new Color(0.75f, 0.75f, 0.75f)); // Silver
                        break;

                    case LighterFluid.FluidType.AzureRadiance:
                        IntensityAmmountToRemove = 0.00144f;
                        lightSource.intensity = 3.2f;
                        SetLightColor(new Color(0.27f, 0.51f, 0.71f)); // Deep Blue
                        break;
                }
            }
        }
    }


}
