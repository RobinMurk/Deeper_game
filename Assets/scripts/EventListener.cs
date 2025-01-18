using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  EventListener : MonoBehaviour
{
    public static EventListener Instance;
    private int isWalkingHash;
    private int isRunningHash;
    private int isSearchingHash;
    private bool _stalking = false;
    private bool _attack = false;
    private bool _investigate = false;
    private bool _investigateArea = false;
    private float _investigationTime = 0f;
    private float _timeLimit = 5f;
    public bool isAgrovated;
    public bool isDocile;
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
        isAgrovated = true;
        MyCustomAi.animator.SetBool(isRunningHash, true);
        MyCustomAi.agent.speed = 4f;
        Investigate = true;
    }

    public void CheckArea()
    {
        MyCustomAi.animator.SetBool(isRunningHash, false);
        MyCustomAi.animator.SetBool(isSearchingHash, true);
        InvestigateArea = true;
    }

    public bool BackToPatrol(float deltaTime)
    {
        _investigationTime += deltaTime;
        if (_investigationTime < _timeLimit) return false;
        MyCustomAi.animator.SetBool(isSearchingHash, false);
        MyCustomAi.animator.SetBool(isWalkingHash, true);
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
        isDocile = true;
        isDocile = false;
        Instance = this;
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isSearchingHash = Animator.StringToHash("isSearching");
    }

}
