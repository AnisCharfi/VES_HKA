using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed = 10f; // Speed at which the object rotates
    private float _startingPositionX; // X position where the touch began

    void Update()
    {
        // Check if there is at least one touch
        if (Input.touchCount > 0)
        {
            // Get the first touch detected
            Touch touch = Input.GetTouch(0);

            // Check the phase of the touch
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Record the starting x position of the touch
                    _startingPositionX = touch.position.x;
                    Debug.Log("Touch Phase Began: StartingPositionX = " + _startingPositionX);
                    break;

                case TouchPhase.Moved:
                    // Calculate the difference in the x position from the starting position
                    float deltaX = touch.position.x - _startingPositionX;
                    Debug.Log("Touch Phase Moved: DeltaX = " + deltaX);

                    // Rotate the object around its local z-axis based on the direction of the touch movement
                    if (deltaX > 0)
                    {
                        // Rotate clockwise (viewed from the front) around z-axis
                        transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime, Space.Self);
                        Debug.Log("Rotating Clockwise: -RotateSpeed * Time.deltaTime = " + (-rotateSpeed * Time.deltaTime));
                    }
                    else if (deltaX < 0)
                    {
                        // Rotate counter-clockwise (viewed from the front) around z-axis
                        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime, Space.Self);
                        Debug.Log("Rotating Counter-Clockwise: RotateSpeed * Time.deltaTime = " + (rotateSpeed * Time.deltaTime));
                    }
                    break;

                case TouchPhase.Ended:
                    // Optional: Log touch end or perform other actions when touch ends
                    Debug.Log("Touch Phase Ended.");
                    break;
            }
        }
    }
}
