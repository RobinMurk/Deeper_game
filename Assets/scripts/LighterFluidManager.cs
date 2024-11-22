using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LighterFluidManager : MonoBehaviour
{
    public static LighterFluidManager Instance;
    public Image healthBar;
    public float healthAmount = 2f;

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

    public void AddLighterFluid(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 2f);
        healthBar.fillAmount = healthAmount / 2f;
    }
}
