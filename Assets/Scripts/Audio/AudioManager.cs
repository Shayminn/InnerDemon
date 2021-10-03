using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioSource sfxAudio;

    [SerializeField] List<AudioClip> backgroundAudioClips;
    private List<AudioClip> tempBGAudioClips = new List<AudioClip>();

    [SerializeField] List<AudioClip> sfxAudioClips;

    public float DelayBetweenEachBGClip = 15f;

    public static bool FirstRun = true;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(PlayBackground());
    }

    public IEnumerator PlayBackground() {
        if (tempBGAudioClips.Count == 0) {
            ResetList();
        }

        int random = UnityEngine.Random.Range(0, tempBGAudioClips.Count);
        backgroundAudio.clip = tempBGAudioClips[random];
        backgroundAudio.Play();

        tempBGAudioClips.RemoveAt(random);

        while (backgroundAudio.clip.length != backgroundAudio.time) {
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
