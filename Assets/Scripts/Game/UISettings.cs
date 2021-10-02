using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public static UISettings Instance;
    public Settings settings;

    public Image key;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        settings = Settings.Instance;
    }

    public void ToggleSettings() {
        settings.quitButton.SetActive(true);
        settings.settings.SetActive(!settings.settings.activeSelf);
    }

    public void GetKey() {
        key.enabled = true;
        GameControl.hasKey = true;
    }
}
