using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public int Health;
    public int Exp;

    public TMP_Text HealthText;
    public TMP_Text ExpText;

    // Max values for health and experience
    public int MaxHealth = 100;
    public int MaxExp = 100;

    public void IncreaseHealth(int value)
    {
        Health += value;

        // Ensure health does not exceed the maximum
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

        HealthText.text = $"HP: {Health}";
    }

    public void IncreaseExp(int value)
    {
        Exp += value;

        // Ensure experience does not exceed the maximum
        if (Exp > MaxExp)
        {
            Exp = MaxExp;
        }

        ExpText.text = $"EXP: {Exp}";
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        // Ensure health doesn't go below 0
        if (Health < 0)
        {
            Health = 0;
        }

        HealthText.text = $"HP: {Health}";
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        HealthText.text = $"HP: {Health}";
        ExpText.text = $"EXP: {Exp}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
