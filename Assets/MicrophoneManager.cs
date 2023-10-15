using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneManager : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;

    void Start()
    {
        // Get available microphones
        var devices = Microphone.devices;
        if (devices.Length == 0)
        {
            Debug.LogError("No microphones detected!");
            return;
        }

        // Fill dropdown options
        dropdown.options = devices.Select(d => new Dropdown.OptionData(d)).ToList();

        // Add listener for Dropdown's Value Changed event
        dropdown.onValueChanged.AddListener(HandleValueChanged);

        // Set the selected microphone
        var index = PlayerPrefs.GetInt("user-mic-device-index", 0);
        if (index >= dropdown.options.Count)
        {
            Debug.LogError("Saved microphone index is out of range!");
            index = 0; // Reset to first available microphone
        }

        // Set initial microphone without invoking event
        dropdown.SetValueWithoutNotify(index);

        // Manually invoke value changed event to setup initial microphone
        HandleValueChanged(index);
    }

    private void HandleValueChanged(int selectedIndex)
    {
        // Save selected microphone
        PlayerPrefs.SetInt("user-mic-device-index", selectedIndex);

        // Handle microphone change here...
        Debug.Log("Selected Microphone: " + dropdown.options[selectedIndex].text);
    }
}
