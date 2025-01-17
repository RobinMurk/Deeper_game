using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  EventListener : MonoBehaviour
{
    public static EventListener Instance;
    private bool _stalking = false;
    private bool _attack = false;
    private bool _investigate = false;
    private bool _investigateArea = false;
    private float _investigationTime = 0f;
    private float _timeLimit = 5f;
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

    public bool Investigate
    {
        get { return _investigate; }
        private set { _investigate = value; }
    }

    public bool InvestigateArea
    {
        get { return _investigateArea; }
        private set { _investigateArea = value; }
    }

    public void HeardNoise()
    {
        MyCustomAi.agent.speed = 2f;
        Investigate = true;
    }

    public void CheckArea()
    {
        InvestigateArea = true;
    }

    public bool BackToPatrol(float deltaTime)
    {
        _investigationTime += deltaTime;
        if (_investigationTime < _timeLimit) return false;
        MyCustomAi.agent.speed = 1f;
        Investigate = false;
        InvestigateArea = false;
        return true;
    }
    
    public void Interact()
    {
        if (levelMaster.Instance.booksCount() > 1)
        {
            MyCustomAi.agent.stoppingDistance = 0f;
            MyCustomAi.animator.SetBool("stalk", false);
            MyCustomAi.animator.SetBool("hunt", true);
            Attack = true;
            Stalk = false;
            return;
        }
        MyCustomAi.animator.SetBool("stalk", true);
        MyCustomAi.agent.stoppingDistance = 3f;
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
