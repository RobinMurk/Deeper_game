using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the slider
    private const string VolumePref = "GameVolume"; // Key for saving volume settings

    void Start()
    {
        // Load saved volume or set default value
        float savedVolume = PlayerPrefs.GetFloat(VolumePref, 0.5f); // Default is 50% volume
        AudioListener.volume = savedVolume; // Set the initial volume
        volumeSlider.value = savedVolume; // Update the slider position

        // Add a listener to detect changes in the slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; // Set the global volume
        PlayerPrefs.SetFloat(VolumePref, volume); // Save the volume setting
        PlayerPrefs.Save(); // Ensure it's written to disk
    }
}
