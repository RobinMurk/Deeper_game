using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    private int movingHash;
    AudioManager audioManager;

    void Footstep(int whichFoot)
    {
        audioManager.Play("WalkingStone");
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movingHash = Animator.StringToHash("moving");
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bool moving = animator.GetBool(movingHash);
        bool forwardPressed = Input.GetKey(KeyCode.W);
        if (!moving && forwardPressed)
        {
            animator.SetBool(movingHash, true);
        }

        if (moving && !forwardPressed)
        {
            animator.SetBool(movingHash, false);
        }
    }
}
