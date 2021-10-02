using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerText : MonoBehaviour {
    public static PlayerText Instance;

    public Text text;
    public float fadeDelay = 3;
    public float fadeSpeed = 0.5f;

    private void Awake() {
        Instance = this;
    }

    public void WriteText(string txt) {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        text.text = txt;

        StartCoroutine(FadeText(fadeDelay));
    }

    private IEnumerator FadeText(float delay) {
        yield return new WaitForSeconds(fadeDelay);

        Color color = text.color;
        while (text.color.a > 0) {
            color.a -= Time.deltaTime * fadeSpeed;
            text.color = color;

            yield return null;
        }
    }
}
