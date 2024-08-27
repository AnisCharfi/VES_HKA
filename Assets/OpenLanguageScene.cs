using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLanguageScene : MonoBehaviour
{

    public void GoToLanguageScene()
    {
        SceneManager.LoadScene("LanguageScene");
    }
}
