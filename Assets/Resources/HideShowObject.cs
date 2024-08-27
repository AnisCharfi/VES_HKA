using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideShowObject : MonoBehaviour
{
    public GameObject objectToToggle;

    public void ToggleVisibility()
    {
        if (objectToToggle != null)
        {
            // Check if the object is currently active
            bool isActive = objectToToggle.activeSelf;
            Debug.Log("Object Active: " + isActive);

            // Toggle the active state
            objectToToggle.SetActive(!isActive);

            Debug.Log("Object Toggled. New Active State: " + !isActive);
        }
        else
        {
            Debug.LogError("No object assigned to toggle.");
        }
    }
}
