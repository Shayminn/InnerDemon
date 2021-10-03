using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour
{
    private GameObject settings;

    private void Start() {
        settings = FindObjectOfType<Settings>().settings;  
    }

    public void Settings(bool toggle) {
        AudioManager.Instance.PlaySFX(SFX.Click);

        settings.SetActive(toggle);

        if (!toggle) {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void Quit() {
        AudioManager.Instance.PlaySFX(SFX.Click);

        Application.Quit();
    }
}
