using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Graphic : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;

    private void Start()
    {
        // Clear existing options in dropdown
        graphicsDropdown.ClearOptions();

        // Populate dropdown with available quality levels
        string[] qualityLevels = QualitySettings.names;
        graphicsDropdown.AddOptions(new List<string>(qualityLevels));

        // Set initial value of dropdown to current quality level
        int currentQualityLevel = QualitySettings.GetQualityLevel();
        graphicsDropdown.SetValueWithoutNotify(currentQualityLevel);
    }

    public void SetQualityLevel(int qualityLevelIndex)
    {
        QualitySettings.SetQualityLevel(qualityLevelIndex);
    }
}