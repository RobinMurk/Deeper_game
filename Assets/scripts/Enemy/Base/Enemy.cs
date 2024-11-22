using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemyMovable
{

    public Rigidbody RB {get; set;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        
    }
    public enum AnimationTriggerType
    {
        PlayFootstepSound
    }
}
