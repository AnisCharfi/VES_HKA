using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMuseumPage : MonoBehaviour
{
    public void GoToMuseumPage()
    {
        SceneManager.LoadScene("MuseumScene");
    }
}
