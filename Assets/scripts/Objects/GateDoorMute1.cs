using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GateDoorMute1 : MonoBehaviour
{
    public static GateDoorMute1 Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenGateMute(){
        gameObject.SetActive(false);
        gameObject.GetComponent<Animator>().SetBool("openGate", true);
    }
}
