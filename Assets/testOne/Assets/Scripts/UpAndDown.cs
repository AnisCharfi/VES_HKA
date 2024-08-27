using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public VarManager varManager;

    private bool animation1;

    public float amplitude = 1.0f;  // The maximum scale factor
    public float frequency = 1.0f;  // How fast the sine wave oscillates

    public float fase = 0.0f;

    // Private variable to keep track of time

    private Vector3 initialPosition;

    private float CurrentTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        bool animation1 = GameObject.Find("AxisWholeThing").GetComponent<VarManager>().animationPlay;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(animation1 == true){
            float newPos = amplitude * Mathf.Sin(frequency*CurrentTime + fase);  
            transform.position = new Vector3(transform.position.x, initialPosition.y + newPos, transform.position.z);
            CurrentTime+= Time.deltaTime;
        }
        animation1 = GameObject.Find("AxisWholeThing").GetComponent<VarManager>().animationPlay;
    }
}
