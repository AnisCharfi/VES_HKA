using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebuildModel : MonoBehaviour
{
    public Transform[] parts; // Array to hold references to each part
    public float rebuildDuration = 2f; // Duration for the rebuild in seconds

    private Vector3[] originalPositions;

    void Start()
    {
        // Store original positions
        originalPositions = new Vector3[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            originalPositions[i] = parts[i].localPosition;
        }
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

