using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button explodeButton;
    public Button buildButton;
    public ModelManager modelManager; // Reference to your ModelManager script

    void Start()
    {
        if (explodeButton != null)
            explodeButton.onClick.AddListener(OnExplodeButtonClick);

        if (buildButton != null)
            buildButton.onClick.AddListener(OnBuildButtonClick);

        // Initial state: Build button active, Explode button inactive
        buildButton.interactable = false;
        explodeButton.interactable = true;
    }

    void OnExplodeButtonClick()
    {
        if (modelManager != null)
        {
            modelManager.Explode();
            explodeButton.interactable = false;
            buildButton.interactable = true;
        }
    }

    void OnBuildButtonClick()
    {
        if (modelManager != null)
        {
            modelManager.Rebuild();
            explodeButton.interactable = true;
            buildButton.interactable = false;
        }
    }
}
