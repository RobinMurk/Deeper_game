using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    int walkingHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        walkingHash = Animator.StringToHash("moving");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool isWalking = animator.GetBool("moving");
        if (!isWalking && forwardPressed){
            animator.SetBool(walkingHash, true);
        }

        if (isWalking && !forwardPressed){
            animator.SetBool(walkingHash, false);
        }
        if (Input.GetKeyDown(KeyCode.E)){
            HandLight.Instance.LightOn = !HandLight.Instance.LightOn;
            HandLight.Instance.TurnOnOff();
        }
    }
}
