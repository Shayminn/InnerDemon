using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public void PlayAgain() {
        AudioManager.Instance.PlaySFX(SFX.Click);

        SceneChanger.Instance.ChangeScene((int)Scenes.Level1);
    }

    public void Quit() {
        AudioManager.Instance.PlaySFX(SFX.Click);

        Application.Quit();
    }
}
