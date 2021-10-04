using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StoryProgression : MonoBehaviour {
    public static bool storySeen = false;

    public PlayerText player1;
    public GameObject player1Spotlight;

    [Space]
    public PlayerText player2;
    public GameObject player2Lights;
    public BoxCollider2D player2Collider;
    public Rigidbody2D player2RB;
    public Text player2StoryText;
    public Text player2GameText;
    public float fadeSpeed;

    [Space]
    public List<DictionaryText> dictionaryTexts;

    private void Start() {
        GameControl.flipped = false;
        GameControl.hasKey = false;

        if (!storySeen) {
            StartCoroutine(PlayStory());
        }
        else {
            InitializePlayersForGame();
        }
    }

    public IEnumerator PlayStory() {
        GameControl.storyTime = true;
        InitializePlayersForStory();
        FollowPlayer.Instance.Zoom(true);

        yield return StartCoroutine(FadeIn());

        while (dictionaryTexts.Count > 0) {
            // player1
            if (dictionaryTexts[0].player == 0) {
                player1.WriteText(dictionaryTexts[0].text, dictionaryTexts[0].fadeText);
                player1.text.color = dictionaryTexts[0].color;
            }
            // player2
            else {
                if (dictionaryTexts[0].color == Color.white) {
                    player2.text.color = new Color(0.6f, 0.6f, 0.6f, 1);
                }
                else {
                    player2.text.color = dictionaryTexts[0].color;
                }

                player2.WriteText(dictionaryTexts[0].text, dictionaryTexts[0].fadeText);
            }

            yield return new WaitForSeconds(dictionaryTexts[0].delay);
            dictionaryTexts.RemoveAt(0);
        }

        if (!GameControl.ending) {
            yield return StartCoroutine(FadeOut());
            FollowPlayer.Instance.Zoom(false);

            if (!GameControl.tutorialCompleted) {
                ActivateTutorialTriggers();
                GameControl.tutorialCompleted = true;
            }

            InitializePlayersForGame();
            storySeen = true;
        }
        else {
            Ending.Instance.StartEnding();
        }
        GameControl.storyTime = false;
    }

    public IEnumerator FadeIn() {
        if (player2 == null)
            yield break;

        player2.transform.position = player1.transform.position;
        Vector3 targetPosition = new Vector3(player2.transform.position.x + 4, player2.transform.position.y, player2.transform.position.z);

        float scale = 0;

        while (player2.transform.position.x != player1.transform.position.x + 4 || player2.transform.localScale.x >= 1) {
            player2.transform.position = Vector3.MoveTowards(player2.transform.position, targetPosition, fadeSpeed * Time.deltaTime);

            scale += Time.deltaTime;
            player2.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        player2.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator FadeOut() {
        if (player2 == null)
            yield break;

        Vector3 targetPosition = player1.transform.position;

        float scale = 1;

        while (player2.transform.position.x != targetPosition.x || player2.transform.localScale.x <= 0) {
            player2.transform.position = Vector3.MoveTowards(player2.transform.position, targetPosition, fadeSpeed * Time.deltaTime);

            scale -= Time.deltaTime;
            player2.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        player2.transform.localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(0.5f);
    }

    public void InitializePlayersForStory() {
        player1Spotlight.SetActive(false);

        if (player2 != null) {
            player2.transform.localScale = new Vector3(0, 0, 0);
            player2.transform.localEulerAngles = new Vector3(0, 0, 0);
            player2.text = player2StoryText;
            player2Lights.SetActive(false);
            player2Collider.enabled = false;
            player2RB.gravityScale = 0;
            player2.gameObject.SetActive(true);
        }
    }

    public void InitializePlayersForGame() {
        player1Spotlight.SetActive(true);

        if (player2 != null) {
            player2.gameObject.SetActive(false);
            player2.transform.localScale = new Vector3(1, 1, 1);
            player2.transform.localEulerAngles = new Vector3(0, 0, 180);
            player2.text = player2GameText;
            player2StoryText.gameObject.SetActive(false);
            player2Lights.SetActive(true);
            player2Collider.enabled = true;
            player2RB.gravityScale = -1;
        }
    }

    private void ActivateTutorialTriggers() {
        foreach (Tutorial tutorial in FindObjectsOfType<Tutorial>().ToList()) {
            tutorial.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}

[System.Serializable]
public class DictionaryText {
    public string text;
    public bool fadeText;
    public Color color = Color.white;
    public int player = 0;
    public float delay = 1;
}
