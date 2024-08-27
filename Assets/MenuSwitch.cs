using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSwitch : MonoBehaviour
{

    // This method will load the scene based on the scene name
    public void SwitchToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // These methods are optional and could be used if you want to switch using predefined methods for each button
    public void LoadMachineOverview()
    {
        SceneManager.LoadScene("MachineOverviewPage");
    }

    public void LoadMachineFunctions()
    {
        SceneManager.LoadScene("MachineFunctionsPage");
    }

    public void LoadWorkingMachine()
    {
        SceneManager.LoadScene("WorkingMachinePage");
    }
}
