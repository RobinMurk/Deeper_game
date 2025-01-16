using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;

    private int isWalkingHash;
    private int isRunningHash;
    private int isCrouchedHash;
    
    AudioManager audioManager;
    private SphereCollider sphereCollider;
    
    void Footstep(int whichFoot)
    {
        audioManager.Play("WalkingStone");
    }

    private bool crouchToggeled;
    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = Player.Instance.Detection.GetComponent<SphereCollider>();
        audioManager = FindObjectOfType<AudioManager>();
        
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isCrouchedHash = Animator.StringToHash("isCrouched");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool wasdPressed = Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouchToggeled = !crouchToggeled;
            if (crouchToggeled)
            {
                animator.SetBool(isCrouchedHash, true);
            }
            else
            {
                animator.SetBool(isCrouchedHash, false);
            }
        }
        
        if (!isWalking && wasdPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !wasdPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && wasdPressed && runPressed)
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!wasdPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
        
        SetPlayerConfigs(isWalking, isRunning, crouchToggeled);
    }

    // NB: Movement on 1 frame delay-ga
    // S.t nt kui crouchid, siis alles järgmine frame kükitamise
    // loogika lööb sisse
    private void SetPlayerConfigs(bool isWalking, bool isRunning, bool isCrouched)
    {
        if (isWalking && !isRunning && !isCrouched)
        {
            Player.Instance.playerSpeed = 4f;
            sphereCollider.radius = 20f;
        } else if (isRunning && !isCrouched)
        {
            Player.Instance.playerSpeed = 8f;
            sphereCollider.radius = 40f;
        } else if (isWalking && !isRunning) // crouch walking
        {
            Player.Instance.playerSpeed = 2f;
            sphereCollider.radius = 3f;
        } else if (!isWalking && !isRunning)
        {
            sphereCollider.radius = 0.5f;
        } 
    }
}
