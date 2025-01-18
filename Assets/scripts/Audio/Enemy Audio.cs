using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAudio : MonoBehaviour
{
    public AudioSource[] sources;


    public void StepSound(int whatFoot){
        int numbr = UnityEngine.Random.Range(1,4);
        sources[numbr-1].Play();
        
    }
}
