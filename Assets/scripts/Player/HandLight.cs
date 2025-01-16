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
        IntensityDelay = 0.1f;   //0.5 seconds
        IntensityAmmountToRemove = 0.001f;
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
            switch (fluid.fluidType)
            {
                case LighterFluid.FluidType.Yellow:
                    IntensityAmmountToRemove = 0.001f;
                    lightSource.intensity = 2f;
                    break;

                case LighterFluid.FluidType.Blue:
                    IntensityAmmountToRemove = 0.002f;
                    lightSource.intensity = 4f;
                    break;

                case LighterFluid.FluidType.Green:
                    IntensityAmmountToRemove = 0.0005f;
                    lightSource.intensity = 1f;
                    break;
            }
        }
    }


}
