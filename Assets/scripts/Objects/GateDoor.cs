using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GateDoor : MonoBehaviour
{
    public static GateDoor Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenGate(){
        gameObject.GetComponent<Animator>().SetBool("openGate", true);
        FindObjectOfType<AudioManager>().Play("GateOpening");
    }
}
