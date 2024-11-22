using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject FireParent;

    private void Awake() {

    }

    public void TurnOnOff(){
        FireParent.gameObject.SetActive(!FireParent.gameObject.activeSelf);
    }
}
