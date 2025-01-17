using UnityEngine;
using System;

public class PlayerAudio : MonoBehaviour
{

    public AudioManager manager;

    public void StepSound(int whatFoot){
        int numbr = UnityEngine.Random.Range(1,4);
        String audioName = "Footstep" + numbr.ToString();
        manager.Play(audioName);
    }
}
