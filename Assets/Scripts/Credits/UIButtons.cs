using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public void PlayAgain() {
        AudioManager.Instance.PlaySFX(SFX.Click);

        SceneChanger.Instance.ChangeScene(Scenes.Level1.ToString());
    }

    public void Quit() {
        AudioManager.Instance.PlaySFX(SFX.Click);

        Application.Quit();
    }
}
