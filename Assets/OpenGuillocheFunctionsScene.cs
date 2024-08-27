using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGuillocheFunctionsScene : MonoBehaviour
{ 
    public void GoToMachineFunctionsScene()
    {
        SceneManager.LoadScene("MachineFunctionsPage");
    }
}
