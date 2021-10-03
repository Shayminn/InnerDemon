using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public static Settings Instance;

    public GameObject settings;

    [SerializeField] private Text leftText;
    [SerializeField] private Text rightText;
    [SerializeField] private Text jumpText;
    [SerializeField] private Text switchText;
    [SerializeField] private Text resetText;

    [SerializeField] private Text errorMSG;

    [SerializeField] private Slider audioSlider;
    [SerializeField] private Text volumeText;

    public GameObject quitButton;
    public float scaleSpeed;

    public static Dictionary<Controls, KeyCode> KeyControls = new Dictionary<Controls, KeyCode> {
        { Controls.Left, KeyCode.A },
        { Controls.Right, KeyCode.D },
        { Controls.Jump, KeyCode.Space },
        { Controls.Switch, KeyCode.W },
        { Controls.Reset, KeyCode.R },
    };

    private string SelectedButtonName = "";
    private bool transitioning = false;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (settings.activeInHierarchy) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                ToggleSettings(false);
            }
        }
    }

    public void ButtonListener() {
        AudioManager.Instance.PlaySFX(SFX.Click);

        GameObject button = EventSystem.current.currentSelectedGameObject;
        SelectedButtonName = button.transform.parent.name;

        StartCoroutine(CheckInput());
    }

    public void ToggleSettings(bool toggle) {
        AudioManager.Instance.PlaySFX(SFX.Click);

        if (!transitioning) {
            if (!toggle) {
                EventSystem.current.SetSelectedGameObject(null);

#if !UNITY_WEBGL
                quitButton.SetActive(false);
#endif

                StartCoroutine(CloseSettingsEffect());
            }
            else {
                StartCoroutine(OpenSettingsEffect());
            }
        }
    }

    public IEnumerator OpenSettingsEffect() {
        settings.SetActive(true);
        settings.transform.localScale = Vector3.zero;

        transitioning = true;
        float scale = 0;
        while (settings.transform.localScale.x < 1) {
            scale += Time.deltaTime * scaleSpeed;
            settings.transform.localScale = new Vector3(scale, scale, scale);

            yield return null;
        }
        transitioning = false;

        settings.transform.localScale = Vector3.one;
    }

    public IEnumerator CloseSettingsEffect() {
        settings.transform.localScale = Vector3.one;

        transitioning = true;
        float scale = 1;
        while (settings.transform.localScale.x > 0) {
            scale -= Time.deltaTime * scaleSpeed;
            settings.transform.localScale = new Vector3(scale, scale, scale);

            yield return null;
        }
        transitioning = false;

        settings.transform.localScale = Vector3.zero;
        settings.SetActive(false);
    }

    public void OnSliderClicked() {
        AudioManager.Instance.PlaySFX(SFX.Click);
    }

    public void OnSliderValueChanged() {
        volumeText.text = audioSlider.value.ToString();

        AudioManager.Instance.AdjustVolume(audioSlider.value / 100);
    }

    public void Quit() {
        AudioManager.Instance.PlaySFX(SFX.Click);

        Application.Quit();
    }

    IEnumerator CheckInput() {
        bool inputSent = false;

        while (!inputSent) {

            foreach (KeyCode kc in Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown(kc)) {
                    inputSent = true;

                    ChangeKeybind(kc);

                    yield break;
                }
            }
            yield return null;
        }
    }

    void ChangeKeybind(KeyCode kc) {
        errorMSG.gameObject.SetActive(false);
        KeyValuePair<Controls, KeyCode> foundKV = KeyControls.FirstOrDefault(keyControl => keyControl.Value == kc);

        if (foundKV.Value == KeyCode.None) {
            foreach (KeyValuePair<Controls, KeyCode> kv in KeyControls) {
                if (SelectedButtonName.Equals(kv.Key.ToString())) {
                    KeyControls[kv.Key] = kc;

                    OnChangedKeybinds();
                    break;
                }
            }
        }
        else {
            errorMSG.gameObject.SetActive(true);
            errorMSG.text = kc + " is already assigned to " + foundKV.Key + "!";
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnChangedKeybinds() {
        PlayerControls.left = KeyControls[Controls.Left];
        leftText.text = KeyControls[Controls.Left].ToString();

        PlayerControls.right = KeyControls[Controls.Right];
        rightText.text = KeyControls[Controls.Right].ToString();

        PlayerControls.jump = KeyControls[Controls.Jump];
        jumpText.text = KeyControls[Controls.Jump].ToString();

        PlayerControls._switch = KeyControls[Controls.Switch];
        switchText.text = KeyControls[Controls.Switch].ToString();

        PlayerControls.reset = KeyControls[Controls.Reset];
        resetText.text = KeyControls[Controls.Reset].ToString();
    }
}
