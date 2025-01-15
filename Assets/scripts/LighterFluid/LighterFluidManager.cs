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
            case LighterFluid.FluidType.Yellow:
                Debug.Log("Yellow fluid used: regular!");
                healthBar.color = Color.yellow;
                break;
            case LighterFluid.FluidType.Blue:
                Debug.Log("Blue fluid used: bright but fast burning!");
                healthBar.color = Color.blue;
                break;
            case LighterFluid.FluidType.Green:
                Debug.Log("Green fluid used: dim but slow burn!");
                healthBar.color = Color.green;
                break;
        }
        handLight.ApplyFluidEffect(fluid);  
        AddLighterFluid(fluid.fluidAmount);
    }
}
