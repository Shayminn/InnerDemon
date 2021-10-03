using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioSource sfxAudio;

    [SerializeField] List<AudioClip> backgroundAudioClips;
    private List<AudioClip> tempBGAudioClips;

    [SerializeField] List<AudioClip> sfxAudioClips;

    public float DelayBetweenEachBGClip = 15f;

    public static bool FirstRun = true;

    // Start is called before the first frame update
    void Start() {
        if (FirstRun) {
            FirstRun = false;
            ResetList();

            Instance = this;

            StartCoroutine(PlayBackground());
        }
    }

    public IEnumerator PlayBackground() {

        int random = UnityEngine.Random.Range(0, tempBGAudioClips.Count);
        backgroundAudio.clip = tempBGAudioClips[random];
        backgroundAudio.Play();

        tempBGAudioClips.RemoveAt(random);
        if (tempBGAudioClips.Count == 0) {
            ResetList();
        }

        while (backgroundAudio.isPlaying) {
            yield return null;
        }

        yield return new WaitForSeconds(DelayBetweenEachBGClip);

        StartCoroutine(PlayBackground());
    }

    public void ResetList() {
        tempBGAudioClips = new List<AudioClip>(backgroundAudioClips);
    }

    public void PlaySFX(SFX sfx) {
        sfxAudio.clip = sfxAudioClips.FirstOrDefault(clip => clip.name.Equals(sfx.ToString()));

        sfxAudio.Play();
    }

    public void AdjustVolume(float vol) {
        backgroundAudio.volume = vol;
        sfxAudio.volume = vol;
    }
}
