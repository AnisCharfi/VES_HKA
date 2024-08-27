using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMachineOverViewPage : MonoBehaviour
{
    public void GoToMachineOverViewPage()
    {
        SceneManager.LoadScene("MachineOverviewPage");
    }
}

