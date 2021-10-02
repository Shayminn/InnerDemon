using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChooseScene : MonoBehaviour
{
    public Scenes selectedScene = Scenes.None;

    void Start() {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ChangeScene);
    }

    void ChangeScene() {
        SceneChanger.Instance.ChangeScene((int)selectedScene);
    }
}
