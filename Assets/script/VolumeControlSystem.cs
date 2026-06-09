// 2025/10/21 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControlSystem : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Exposed Parameter Names")]
    public string masterVolumeParam = "VolumeMaster";
    public string bgmVolumeParam = "VolumeBGM";
    public string sfxVolumeParam = "VolumeSFX";

    private void Start()
    {
        // Initialize slider values based on the audio mixer settings
        InitializeSliderValue(masterVolumeSlider, masterVolumeParam);
        InitializeSliderValue(bgmVolumeSlider, bgmVolumeParam);
        InitializeSliderValue(sfxVolumeSlider, sfxVolumeParam);

        // Add listeners to sliders to update the audio mixer values
        masterVolumeSlider.onValueChanged.AddListener(value => SetVolume(masterVolumeParam, value));
        bgmVolumeSlider.onValueChanged.AddListener(value => SetVolume(bgmVolumeParam, value));
        sfxVolumeSlider.onValueChanged.AddListener(value => SetVolume(sfxVolumeParam, value));
    }

    private void InitializeSliderValue(Slider slider, string parameterName)
    {
        if (audioMixer.GetFloat(parameterName, out float value))
        {
            slider.value = value;
        }
        else
        {
            Debug.LogWarning($"Failed to get the value for parameter '{parameterName}' from the Audio Mixer.");
        }
    }

    private void SetVolume(string parameterName, float value)
    {
        audioMixer.SetFloat(parameterName, value);
    }
}