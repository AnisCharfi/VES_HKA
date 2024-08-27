using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShow : MonoBehaviour
{
    public GameObject Part;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hide()
    {
        Part.SetActive(false);

    }
    public void Show()
    {
        Part.SetActive(true);

    }
}
