using System.Collections;
using UnityEngine;

public class Ending : MonoBehaviour {
    public static Ending Instance;

    public bool alternateEnding = true;

    public SpriteRenderer playerSprite;
    public SpriteRenderer player2Sprite;

    public Material unlitMaterial;
    public Material defaultMaterial;

    public GameObject pointLight;
    public float flickTime;
    public float delayBetweenFlicks;
    public float delayBeforeGameover;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameControl.ending = true;
    }

    public void StartEnding() {
        if (!alternateEnding) {
            StartCoroutine(FlickerLight());
        }
        else {
            Gameover.Instance.DisplayGameOver(true);
        }
    }

    private IEnumerator FlickerLight() {
        player2Sprite.material = unlitMaterial;
        playerSprite.material = unlitMaterial;

        pointLight.SetActive(false);
        yield return new WaitForSeconds(flickTime);
        player2Sprite.gameObject.SetActive(false);
        pointLight.SetActive(true);

        yield return new WaitForSeconds(delayBetweenFlicks);

        pointLight.SetActive(false);
        yield return new WaitForSeconds(flickTime);
        playerSprite.gameObject.SetActive(false);
        player2Sprite.transform.position = playerSprite.transform.position;
        pointLight.SetActive(true);

        yield return new WaitForSeconds(delayBetweenFlicks);

        pointLight.SetActive(false);
        yield return new WaitForSeconds(flickTime);
        player2Sprite.material = defaultMaterial;
        player2Sprite.gameObject.SetActive(true);
        pointLight.SetActive(true);

        yield return new WaitForSeconds(delayBeforeGameover);

        Gameover.Instance.DisplayGameOver(true);
    }
}
