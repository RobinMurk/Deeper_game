using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public static EventListener Instance;
    private bool _triggered = false;
    private bool _attack = false;
    public bool Triggered
    {
        get { return _triggered; } 
        private set { _triggered = value; }
    }
    
    public bool Attack
    {
        get { return _attack; } 
        private set { _attack = value; }
    }

    public void Interact()
    {
        Debug.Log("interact");
        if (Triggered)
        {
            Attack = true;
            Triggered = false;
            return;
        }
        Triggered = true;
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
