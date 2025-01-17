using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public static AnimationStateController Instance;
    private Animator animator;

    private int isWalkingHash;
    private int isRunningHash;
    private int isCrouchedHash;
    private int lanternHash;
    
    AudioManager audioManager;
    private SphereCollider sphereCollider;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

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
        lanternHash = Animator.StringToHash("lanternOn");

        audioManager.Play("BreathingSlow");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool lantern = animator.GetBool(lanternHash);
        bool wasdPressed = Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");

        if (Input.GetKeyDown("e"))
        {   
            //HandLight.Instance.TurnOnOff();
            if (!lantern && HandLight.Instance.LightOn)
            {
                HandLight.Instance.TurnOff();
                animator.SetBool(lanternHash, true);
            }
            else
            {
                HandLight.Instance.TurnOn();
                animator.SetBool(lanternHash, false);
            }
        }
        
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
            audioManager.Stop("BreathingSlow");
            audioManager.Play("Running");
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!wasdPressed || !runPressed))
        {
            audioManager.Stop("Running");
            audioManager.Play("BreathingSlow");
            animator.SetBool(isRunningHash, false);
        }
        
        SetPlayerConfigs(isWalking, isRunning, crouchToggeled);
    }

    // NB: Movement on 1 frame delay-ga
    // S.t nt kui crouchid, siis alles järgmine frame kükitamise
    // loogika lööb sisse
    private void SetPlayerConfigs(bool isWalking, bool isRunning, bool isCrouched)
    {
        if (isWalking && !isRunning && !isCrouched) //walking
        {
            Player.Instance.playerSpeed = 4f;
            sphereCollider.radius = 20f;
        } else if (isRunning && !isCrouched) //running
        {
            Player.Instance.playerSpeed = 8f;
            sphereCollider.radius = 40f;
        } else if (isWalking && !isRunning) // crouch walking
        {
            Player.Instance.playerSpeed = 2f;
            sphereCollider.radius = 3f;
        } else if (!isWalking && !isRunning) //idle
        {
           sphereCollider.radius = 0.5f;
        } 
    }

    public void armDown()
    {
        animator.SetBool(lanternHash, true);
    }
}
