using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenHomePage : MonoBehaviour
{
    public void GoToHomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }
}

