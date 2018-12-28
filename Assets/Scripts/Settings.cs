using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public TMP_Dropdown inputDropdown;
    public Toggle compactViewToggle;

    public GameObject normalView;
    public GameObject compactView;
    //Called on the First Frame before Update()
    private void Start()
    {
        AddInputDropdownValues();
        compactViewToggle.isOn = PlayerPrefs.HasKey("usingCompactView") ? PlayerPrefs.GetInt("usingCompactView") != 0 : false;
    }

    private void AddInputDropdownValues()
    {
        inputDropdown.ClearOptions();

        List<string> devices = new List<string>();
        int selectedDeviceIndex = 0;

        foreach (WebCamDevice device in WebCamTexture.devices)
        {
            devices.Add(device.name);

            if (PlayerPrefs.HasKey("selectedDevice"))
            {
                if (PlayerPrefs.GetString("selectedDevice") == device.name)
                {
                    selectedDeviceIndex = devices.IndexOf(device.name);
                }
            }
        }

        inputDropdown.AddOptions(devices);
        inputDropdown.value = selectedDeviceIndex;
        inputDropdown.RefreshShownValue();
    }
    //Called when the GameObject the script is attached to is disabled
    private void OnDisable()
    {
        PlayerPrefs.SetString("selectedDevice", inputDropdown.options[inputDropdown.value].text);
        PlayerPrefs.SetInt("usingCompactView", compactViewToggle.isOn ? 1 : 0);

        PlayerPrefs.Save();
    }

}
