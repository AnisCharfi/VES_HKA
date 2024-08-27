using UnityEngine;

public class Spinner : MonoBehaviour
{
    // Public variable to set the spin speed from the Unity Inspector
    public float spinSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the GameObject around its Y axis at the specified speed
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
