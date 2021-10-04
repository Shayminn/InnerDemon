using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFade : MonoBehaviour
{
    public Image image;

    public float fadeSpeed;

    [Space]
    public float minDelayFlicker;
    public float maxDelayFlicker;
    public float minFlicker;
    public float maxFlicker;

    private void Start() {
        int random = Random.Range(0, 2);

        if (random == 0)
            StartCoroutine(Fade());
        else
            StartCoroutine(Flicker());
    }

    IEnumerator Fade() {
        Color color = image.color;
        bool fadeIn = true;

        while (true) {
            if (fadeIn) {
                color.a += Time.deltaTime;
                if (color.a >= 1)
                    fadeIn = false;
            }
            else {
                color.a -= Time.deltaTime;
                if (color.a <= 0)
                    fadeIn = true;
            }
            image.color = color;

            yield return null;
        }
    }

    IEnumerator Flicker() {
        float randomBetween, randomDelay;
        Color full = image.color;
        full.a = 1;
        Color empty = image.color;
        empty.a = 0;

        while (true) {
            randomBetween = Random.Range(minFlicker, maxFlicker);
            randomDelay = Random.Range(minDelayFlicker, maxDelayFlicker);

            image.color = full;
            yield return new WaitForSeconds(randomDelay);
            image.color = empty;

            yield return new WaitForSeconds(randomBetween);
        }
    }
}
