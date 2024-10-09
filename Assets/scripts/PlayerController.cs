using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDistancePerKeyDown = 2f;
    public float moveSpeed = 1f; 
    public float rotateSpeedInMilliseconds = 100f;
    public float stepCount = 10;
    private bool moving = false;

    void Start()
    {
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation(){
        if(moving) return;

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
        /* Smooth movement
        while(elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        */
        int steps = 10;
        // move speed 1 = 1 unit in 1 second
        float delay = moveSpeed / steps;
        for (int i = 0; i < steps; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * delay);

            // Short pause after each step
            yield return new WaitForSeconds(delay); // Pause between increments
        }
        moving = false;
    }

    private IEnumerator RotatePlayer(float totalRotation)
    {
        moving = true;
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
        moving = false;
    }
}
