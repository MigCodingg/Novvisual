using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Fade")]
    [SerializeField] private float fadeDuration = 1f;

    private Coroutine musicRoutine;

    private void Awake()
    {
        Instance = this;
    }

    // ---------------- MUSIC ----------------
    public void PlayMusic(AudioClip clip)
    {
        if (musicRoutine != null)
            StopCoroutine(musicRoutine);

        musicRoutine = StartCoroutine(FadeMusic(clip));
    }

    public void StopMusic()
    {
        if (musicRoutine != null)
            StopCoroutine(musicRoutine);

        musicRoutine = StartCoroutine(FadeOutMusic());
    }

    private IEnumerator FadeMusic(AudioClip newClip)
    {
        // fade out current
        yield return Fade(0);

        musicSource.clip = newClip;
        musicSource.loop = true;
        musicSource.Play();

        // fade in
        yield return Fade(1);
    }

    private IEnumerator FadeOutMusic()
    {
        yield return Fade(0);
        musicSource.Stop();
    }

    private IEnumerator Fade(float targetVolume)
    {
        float start = musicSource.volume;
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(start, targetVolume, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = targetVolume;
    }

    // ---------------- SFX ----------------
    public void PlaySFX(AudioClip clip, float volume = 1f)
{
    sfxSource.PlayOneShot(clip, volume);
}
}