using UnityEngine; // Import Unity engine namespace
using UnityEngine.EventSystems; // Import EventSystem namespace

public class Rotatable : MonoBehaviour // Script to Rotate 3D Mixer Object in Explore Mixer Scene
{
    private float rotationSpeed = 0.8f; // Adjust rotation speed as needed
    private bool rotationActive; // Flag to control rotation activity
    private Vector2 lastTouchPosition; // Store the last touch position
    private Quaternion initialRotation; // Store the initial rotation of the object
    private Quaternion currentRotation; // Store the current rotation of the object
    private Vector3 initialPosition; // Store the initial position of the object

    private void Start() // Start method called on object initialization
    {
        initialRotation = transform.rotation; // Set initialRotation to the object's current rotation
        currentRotation = initialRotation; // Set currentRotation to the initial rotation
        initialPosition = transform.position; // Set initialPosition to the object's current position
    }

    private void Update() // Update method called once per frame
    {
        // Check if there's exactly one touch and it's not over a UI element
        if (Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            Touch touch = Input.GetTouch(0); // Get the touch input
            if (touch.phase == TouchPhase.Began) // Check if touch has just begun
            {
                lastTouchPosition = touch.position; // Store the initial touch position
            }
            else if (touch.phase == TouchPhase.Moved) // Check if touch is in the moved phase
            {
                float deltaX = touch.position.x - lastTouchPosition.x; // Calculate horizontal movement
                float deltaY = touch.position.y - lastTouchPosition.y; // Calculate vertical movement

                float horizontalRotation = deltaX * rotationSpeed; // Calculate rotation based on horizontal movement

                currentRotation *= Quaternion.Euler(0, -horizontalRotation, 0); // Adjust current rotation based on horizontal movement
                transform.rotation = currentRotation; // Apply the new rotation to the object

                lastTouchPosition = touch.position; // Update the last touch position
            }
        }
    }

            private void OnEnable() // OnEnable method called when the object is enabled
            {
                rotationActive = true; // Set rotationActive flag to true
            }

            private void OnDisable() // OnDisable method called when the object is disabled
            {
                rotationActive = false; // Set rotationActive flag to false
            }

            private void LateUpdate() // LateUpdate method called after all Update methods have been called
            {
                // Ensure object stays at its initial position
                transform.position = initialPosition; // Set the object's position to its initial position
            }
        }
    