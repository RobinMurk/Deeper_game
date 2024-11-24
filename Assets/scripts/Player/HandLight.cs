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
        IntensityDelay = 10f;   //10 seconds
        IntensityAmmountToRemove = 0.1f;
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
        if(Time.time >= InternalTime && LightOn){
            DecreaseIntensity(IntensityAmmountToRemove);
        }
        if(!LightOn){
            TurnOnOff();
        }
    }

    public void TurnOnOff(){
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void DecreaseIntensity(float amount)
    {
        Light lightSource = GetLight();
        if (lightSource != null && lightSource.intensity > 0)
        {
            // Check if enough lighter fluid is available
            if (LighterFluidManager.Instance.healthAmount > 0)
            {
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

    public void ApplyFluidEffect(LighterFluid fluid)
    {
        Light lightSource = GetLight();

        if (lightSource != null)
        {
            switch (fluid.fluidType)
            {
                case LighterFluid.FluidType.Yellow:
                    IntensityDelay = 10f;
                    lightSource.intensity = 2f;
                    break;

                case LighterFluid.FluidType.Blue:
                    IntensityDelay = 5f;
                    lightSource.intensity = 4f;
                    break;

                case LighterFluid.FluidType.Green:
                    IntensityDelay = 15;
                    lightSource.intensity = 1f;
                    break;
            }
        }
    }


}
