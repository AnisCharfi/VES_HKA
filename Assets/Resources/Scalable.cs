using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scalable : MonoBehaviour
{
    public float scaleSpeed = 1f; // Adjust scale speed as needed
    public float minScale = 0.5f; // Minimum scale limit, adjust based on your need
    public float maxScale = 10f; // Maximum scale limit, adjust based on your need

    private Vector3 originalScale; // Store the original scale of the object
    private bool scaleEnable = false; // Flag to enable scaling

    private void Start()
    {
        originalScale = transform.localScale; // Remember the original scale when the script starts
        scaleEnable = true; // Enable scaling
    }

    private void OnEnable() { scaleEnable = true; } // Enable scaling when the object is enabled
    private void OnDisable() { scaleEnable = false; } // Disable scaling when the object is disabled

    void Update()
    {
        // Adjust scale based on mouse wheel for desktop testing
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scaleChange = Input.GetAxis("Mouse ScrollWheel") * scaleSpeed; // Calculate scale change
            ScaleObject(scaleChange); // Scale the object
        }

        // Adjust scale based on pinch for touch devices
        if (Input.touchCount == 2 && scaleEnable)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Adjust scaling factor for pinch and scale the object
            ScaleObject(-deltaMagnitudeDiff * scaleSpeed * 0.01f); // The negative sign is used to invert the scale
        }
    }

    // Function to scale the object based on the given scale change
    private void ScaleObject(float scaleChange)
    {
        Vector3 scale = transform.localScale + Vector3.one * scaleChange; // Calculate new scale based on change

        // Ensure new scale is within limits
        scale = new Vector3(
            Mathf.Clamp(scale.x, originalScale.x * minScale, originalScale.x * maxScale),
            Mathf.Clamp(scale.y, originalScale.y * minScale, originalScale.y * maxScale),
            Mathf.Clamp(scale.z, originalScale.z * minScale, originalScale.z * maxScale)
        );

        transform.localScale = scale; // Apply the new scale
    }
}
