using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAndRebuildModel : MonoBehaviour
{
    public Transform[] parts; // Array to hold references to each part
    public Vector3[] explosionVectors; // Specify the explosion direction and distance for each part
    public float explosionDuration = 2f; // Duration for the explosion in seconds
    public float rebuildDuration = 2f; // Duration for the rebuild in seconds

    private Vector3[] originalPositions;

    void Start()
    {
        if (parts.Length != explosionVectors.Length)
        {
            Debug.LogError("The number of parts must match the number of explosion vectors.");
            return;
        }

        // Store original positions
        originalPositions = new Vector3[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            originalPositions[i] = parts[i].localPosition;
        }
    }

    public void Explode()
    {
        StopAllCoroutines(); // Stop any ongoing coroutines
        StartCoroutine(ExplodeCoroutine());
    }

    public void Rebuild()
    {
        StopAllCoroutines(); // Stop any ongoing coroutines
        StartCoroutine(RebuildCoroutine());
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
    }

    private IEnumerator RebuildCoroutine()
    {
        float elapsedTime = 0f;
        Vector3[] currentPositions = new Vector3[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            currentPositions[i] = parts[i].localPosition;
        }

        while (elapsedTime < rebuildDuration)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].localPosition = Vector3.Lerp(currentPositions[i], originalPositions[i], elapsedTime / rebuildDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].localPosition = originalPositions[i]; // Ensure final position
        }
    }
}
