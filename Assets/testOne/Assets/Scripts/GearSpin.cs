using UnityEngine;

public class GearSpin : MonoBehaviour
{
    // Public variable to set the spin speed from the Unity Inspector
    public float spinSpeed = 100f;

    public Vector3 SpinAxis = new Vector3(1.0f,0.0f,0.0f);

    //public VarManager varManager;

    private bool animation1;

    // Update is called once per frame
    void Start()
    {
        bool animation1 = GameObject.Find("AxisWholeThing").GetComponent<VarManager>().animationPlay;
    }

    void Update()
    {
        if(animation1 == true){
            // Rotate the GameObject around its Y axis at the specified speed
            transform.Rotate(SpinAxis, spinSpeed * Time.deltaTime);
        }
        animation1 = GameObject.Find("AxisWholeThing").GetComponent<VarManager>().animationPlay;
    }
}
