using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject FireParent;
    private bool isActive;

    private void Awake() {
        isActive = false;
    }

    public void TurnOnOff(){
        FireParent.gameObject.SetActive(isActive);
        isActive = !isActive;
    }
}
