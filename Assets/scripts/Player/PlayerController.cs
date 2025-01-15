using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    int walkingHash;
    private SphereCollider sphereCollider;
    AudioManager audioManager;
    private bool crouching;
    
    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = Player.Instance.Detection.GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        walkingHash = Animator.StringToHash("moving");
    }
    
    void Footstep(int whichFoot)
    {
        audioManager.Play("WalkingStone");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            crouching = !crouching;
        
        bool forwardPressed = Input.GetKey("w")
            | Input.GetKey("s")
            | Input.GetKey("a")
            | Input.GetKey("d");
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool isWalking = animator.GetBool("moving");
        if(!isWalking && forwardPressed){
            animator.SetBool(walkingHash, true);
            isWalking = true;
        }
        
        if (isWalking && crouching)
        {
            Player.Instance.playerSpeed = 0.5f;
            sphereCollider.radius = 3f;
        }
        else if (isWalking && forwardPressed && shiftPressed)
        {
            Player.Instance.playerSpeed = 6f;
            sphereCollider.radius = 40f;
        } else if (isWalking && forwardPressed)
        {
            Player.Instance.playerSpeed = 3f;
            sphereCollider.radius = 20f;
        }
        else {
            sphereCollider.radius = 1f;
            animator.SetBool(walkingHash, false);
        }

        if (Input.GetKeyDown(KeyCode.E)){
            HandLight.Instance.TurnOnOff();
        }
    }
    
}
