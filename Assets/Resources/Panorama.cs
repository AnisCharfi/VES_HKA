using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanoramaView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Rotation()
    {
        transform.Rotate(0f, 0f, 100f * Time.deltaTime, Space.Self);
    }
}
