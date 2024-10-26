using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 4f;
    public float smoothing = 1.5f;

    private float xMousePos;
    private float smoothedMousePos;

    private float currentLookingPos;

    /*private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }*/

    void Update()
    {
        GetInput();
        ModifyInput();
        MovePlayer();
    }

    void GetInput()
    {
        xMousePos = Input.GetAxisRaw("Mouse X");
    }

    void ModifyInput()
    {
        xMousePos *= sensitivity * smoothing;
        smoothedMousePos = Mathf.Lerp(smoothedMousePos, xMousePos, 1f / smoothing);
    }

    void MovePlayer()
    {
        currentLookingPos += smoothedMousePos;
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);
    }
}
