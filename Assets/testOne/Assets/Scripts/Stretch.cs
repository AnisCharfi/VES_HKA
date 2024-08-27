//using System.Numerics;
using UnityEngine;

public class Stretch : MonoBehaviour
{
    public VarManager varManager;

    private bool animation1;

    public float amplitude = 1.0f;  // The maximum scale factor

    public float amplitude2 = 1.0f;  // The maximum scale factor

    public float frequency = 1.0f;  // How fast the sine wave oscillates

    public float fase = 0.0f;

    // Private variable to keep track of time

    private Vector3 initialPosition;

    private float CurrentTime = 0.0f;

    private Vector3 currentScale2;

    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        bool animation1 = GameObject.Find("AxisWholeThing").GetComponent<VarManager>().animationPlay;
        initialPosition = transform.position;
        currentScale2 = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(animation1 == true){
            float newPos = amplitude * Mathf.Sin(frequency*CurrentTime + fase);

            float newYScale = amplitude2 * Mathf.Sin(frequency * CurrentTime + fase + 3.14f);


            transform.position = new Vector3(transform.position.x, initialPosition.y + newPos, transform.position.z);

            // Get the current scale of the GameObject
            Vector3 currentScale = transform.localScale;

            // Apply the new scale to the Z-axis, keeping X and Y the same
            transform.localScale = new Vector3(currentScale.x, currentScale2.y + newYScale, currentScale.z);

            CurrentTime+= Time.deltaTime;
        }
        animation1 = GameObject.Find("AxisWholeThing").GetComponent<VarManager>().animationPlay;
    }
}
