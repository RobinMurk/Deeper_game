using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public static EventListener Instance;
    private bool _stalking = false;
    private bool _attack = false;
    public bool Stalk
    {
        get { return _stalking; } 
        private set { _stalking = value; }
    }
    
    public bool Attack
    {
        get { return _attack; } 
        private set { _attack = value; }
    }

    public void Interact()
    {
        if (Stalk)
        {
            Attack = true;
            Stalk = false;
            return;
        }
        Stalk = true;
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
