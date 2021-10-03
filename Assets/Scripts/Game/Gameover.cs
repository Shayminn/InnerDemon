using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    public static Gameover Instance;

    public Text title;
    public float fadeSpeed;

    public GameObject restartButton;

    public GameObject extraText;
    public GameObject restartFromLastLevelButton;
    public GameObject continueToCredits;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        HideAll();
    }

    public void DisplayGameOver(bool ending) {
        StartCoroutine(FadeIn(ending));
    }

    private IEnumerator FadeIn(bool ending) {
        Color color = title.color;
        color.a = 0;
        title.color = color;
        title.gameObject.SetActive(true);
        while (color.a < 1) {
            color.a += Time.deltaTime * fadeSpeed;
            title.color = color;

            yield return null;
        }

        yield return new WaitForSeconds(1);

        restartButton.SetActive(!ending);
        extraText.SetActive(ending);
        restartFromLastLevelButton.SetActive(ending);
        continueToCredits.SetActive(ending);
    }

    public void Restart() {
        SceneChanger.Instance.ReloadScene();
        HideAll();
    }

    public void RestartLastLevel() {
        SceneChanger.Instance.ChangeScene((int)Scenes.Level5);
        HideAll();
    }

    public void ContinueToCredits() {
        SceneChanger.Instance.ChangeScene((int)Scenes.Credits);
        HideAll();
    }

    void HideAll() {
        title.gameObject.SetActive(false);
        restartButton.SetActive(false);
        extraText.SetActive(false);
        restartFromLastLevelButton.SetActive(false);
        continueToCredits.SetActive(false);
    }
}
