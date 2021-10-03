using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour
{
    public void ToggleSettings(bool toggle) {
        Settings.Instance.ToggleSettings(toggle);
    }

    public void Quit() {
        AudioManager.Instance.PlaySFX(SFX.Click);

        Application.Quit();
    }
}
