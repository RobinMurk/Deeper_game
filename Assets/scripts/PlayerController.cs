using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDistancePerKeyDown = 1f;
    public float moveSpeed = 100f; 
    public float rotateSpeedInMilliseconds = 100f;
    public float stepCount = 3;
    private Rigidbody rb;
    private bool moving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation(){
        // Rotate the player
        // Check if the "A" key is pressed (rotate left)
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(RotatePlayer(-90));
        }

        // Check if the "D" key is pressed (rotate right)
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(RotatePlayer(90));
        }
    }

    private void HandleMovement()
    {
        if(moving) return;
        
        // Check if the "W" key is pressed (move forward)
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(MovePlayer(moveDistancePerKeyDown, moveDistancePerKeyDown));
        }

        // Check if the "S" key is pressed (move backward)
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(MovePlayer(-moveDistancePerKeyDown, moveDistancePerKeyDown));
        }

    }

    private IEnumerator MovePlayer(float distance, float speed)
    {
        moving = true;
        float elapsedTime = 0f;
        float duration = moveSpeed;
        Vector3 targetPosition = transform.position + distance * transform.forward;
        while(elapsedTime < duration){
            Debug.Log(elapsedTime);
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        moving = false;
        /*// Lerp from the current position to the target position over time
        float elapsedTime = 0f;
        Vector3 startingPosition = rb.position;
        Vector3 targetPosition = rb.position + distance * transform.forward;

        while (elapsedTime < moveSpeed)
        {
            // Calculate the fraction of movement completed
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveSpeed;

            // Smoothly interpolate to the target position
            rb.MovePosition(Vector3.Lerp(startingPosition, targetPosition, t));

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final position is set to the target position
        rb.MovePosition(targetPosition);*/
    }

    private IEnumerator RotatePlayer(float totalRotation)
    {
        // Calculate the number of steps and the angle per step
        float anglePerStep = totalRotation / stepCount;

        // Calculate how long to pause after each step
        float rotationPauseDuration = rotateSpeedInMilliseconds / 1000;

        for (int i = 0; i < stepCount; i++)
        {
            // Calculate the target rotation for this step
            Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + anglePerStep, 0);

            // Snap to the target rotation immediately
            transform.rotation = targetRotation;

            // Short pause after each step
            yield return new WaitForSeconds(rotationPauseDuration); // Pause between increments
        }
    }
}
