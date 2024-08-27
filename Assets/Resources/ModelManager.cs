using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    public Transform[] parts; // Array to hold references to each part
    public Vector3[] explosionVectors; // Specify the explosion direction and distance for each part
    public float explosionDuration = 2f; // Duration for the explosion in seconds
    public float rebuildDuration = 2f; // Duration for the rebuild in seconds
    public float rotateSpeed = 10f; // Speed at which the object rotates
    public Material highlightMaterial; // Material used for highlighting
    private Material[] originalMaterials; // Array to store original materials of parts
    private bool isExploded = false; // To check if the model is currently exploded
    private Vector3[] originalPositions; // Original positions of the parts
    private Quaternion[] originalRotations; // Original rotations of the parts
    private Transform currentRotatingPart = null; // Currently selected part to rotate
    public Transform guillocheTestMachine; // Reference to the parent object

    void Start()
    {
        if (parts.Length != explosionVectors.Length)
        {
            Debug.LogError("The number of parts must match the number of explosion vectors.");
            return;
        }

        // Initialize original positions and rotations
        originalPositions = new Vector3[parts.Length];
        originalRotations = new Quaternion[parts.Length];
        originalMaterials = new Material[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            originalPositions[i] = parts[i].localPosition;
            originalRotations[i] = parts[i].localRotation;

            // Store original materials
            Renderer renderer = parts[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                originalMaterials[i] = renderer.material;
            }
        }
    }

    void Update()
    {
        if (Input.touchCount > 0) // Check if there is at least one touch
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Moved) // When the touch moves
            {
                float deltaX = touch.deltaPosition.x; // Get the horizontal movement of the touch

                if (!isExploded)
                {
                    // Rotate the entire GuillocheTestMachine object around the Z-axis
                    guillocheTestMachine.Rotate(Vector3.forward, -deltaX * rotateSpeed * Time.deltaTime);
                }
                else if (currentRotatingPart != null)
                {
                    // Rotate the selected part around its center on the Z-axis if exploded
                    currentRotatingPart.Rotate(Vector3.forward, -deltaX * rotateSpeed * Time.deltaTime);
                }
            }

            if (touch.phase == TouchPhase.Began && isExploded) // When the touch begins and model is exploded
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the touched object is one of the model's parts
                    foreach (var part in parts)
                    {
                        if (hit.transform == part)
                        {
                            HighlightPart(part); // Highlight the selected part
                            currentRotatingPart = part; // Set the touched part as the current rotating part
                            break;
                        }
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended) // When the touch ends
            {
                DeselectPart(); // Deselect the part and remove highlighting
            }
        }
    }

    private void HighlightPart(Transform part)
    {
        Renderer renderer = part.GetComponent<Renderer>();
        if (renderer != null && highlightMaterial != null)
        {
            renderer.material = highlightMaterial; // Change material to highlight the part
        }
    }

    private void DeselectPart()
    {
        if (currentRotatingPart != null)
        {
            Renderer renderer = currentRotatingPart.GetComponent<Renderer>();
            if (renderer != null)
            {
                int index = System.Array.IndexOf(parts, currentRotatingPart);
                if (index >= 0 && originalMaterials[index] != null)
                {
                    renderer.material = originalMaterials[index]; // Revert to original material
                }
            }
            currentRotatingPart = null; // Deselect the part
        }
    }

    public void Explode()
    {
        StopAllCoroutines(); // Stop any ongoing coroutines
        StartCoroutine(ExplodeCoroutine());
    }

    private IEnumerator ExplodeCoroutine()
    {
        float elapsedTime = 0f;
        Vector3[] targetPositions = new Vector3[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            targetPositions[i] = originalPositions[i] + explosionVectors[i];
        }

        while (elapsedTime < explosionDuration)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].localPosition = Vector3.Lerp(originalPositions[i], targetPositions[i], elapsedTime / explosionDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].localPosition = targetPositions[i]; // Ensure final position
        }

        isExploded = true;
    }

    public void Rebuild()
    {
        StopAllCoroutines(); // Stop any ongoing coroutines
        StartCoroutine(RebuildCoroutine());
    }

    private IEnumerator RebuildCoroutine()
    {
        float elapsedTime = 0f;
        Vector3[] currentPositions = new Vector3[parts.Length];
        Quaternion[] currentRotations = new Quaternion[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            currentPositions[i] = parts[i].localPosition;
            currentRotations[i] = parts[i].localRotation;
        }

        while (elapsedTime < rebuildDuration)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].localPosition = Vector3.Lerp(currentPositions[i], originalPositions[i], elapsedTime / rebuildDuration);
                parts[i].localRotation = Quaternion.Lerp(currentRotations[i], originalRotations[i], elapsedTime / rebuildDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].localPosition = originalPositions[i]; // Ensure final position
            parts[i].localRotation = originalRotations[i]; // Ensure final rotation
        }

        isExploded = false;
    }
}
