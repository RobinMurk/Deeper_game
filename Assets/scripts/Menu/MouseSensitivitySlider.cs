using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivitySlider : MonoBehaviour
{
    public Slider sensitivitySlider; // Reference to the slider

    void Start()
    {
        // Initialize the slider with the saved sensitivity value
        if (GameSettings.Instance != null)
        {
            sensitivitySlider.value = GameSettings.Instance.mouseSensitivity;
        }

        // Add a listener to update sensitivity when the slider is adjusted
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
    }

    void UpdateSensitivity(float value)
    {
        if (GameSettings.Instance != null)
        {
            GameSettings.Instance.mouseSensitivity = value; // Save to GameSettings
        }
    }
}
