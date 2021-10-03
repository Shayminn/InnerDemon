using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public static UISettings Instance;

    public Image key;

    private void Awake() {
        Instance = this;
    }

    public void ToggleSettings() {
        AudioManager.Instance.PlaySFX(SFX.Click);

#if !UNITY_WEBGL
        Settings.Instance.quitButton.SetActive(true);
#endif
        Settings.Instance.ToggleSettings(!Settings.Instance.settings.activeInHierarchy);
    }

    public void GetKey() {
        key.enabled = true;
        GameControl.hasKey = true;
    }
}
