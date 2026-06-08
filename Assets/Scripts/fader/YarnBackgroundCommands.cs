using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Yarn.Unity;

public class YarnBackgroundCommands : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private BackgroundDatabase database;
    [SerializeField] private AudioDatabase audioDatabase;

    private void Awake()
    {
        // Background
        dialogueRunner.AddCommandHandler<string>(
            "background",
            ChangeBackground
        );

        // Music
        dialogueRunner.AddCommandHandler<string>(
            "music",
            PlayMusic
        );

        // SFX
        dialogueRunner.AddCommandHandler<string>(
            "sfx",
            PlaySFX
        );

        // Stop music
        dialogueRunner.AddCommandHandler(
            "stop_music",
            StopMusic
        );

        // Change scene
        dialogueRunner.AddCommandHandler<string>(
            "scene",
            ChangeScene
        );
    }

    // ---------------- BACKGROUND ----------------
    private void ChangeBackground(string id)
    {
        Sprite sprite = database.Get(id);

        if (sprite == null)
        {
            Debug.LogWarning("Missing background id: " + id);
            return;
        }

        BackgroundManager.Instance.SetBackground(sprite);
    }

    // ---------------- MUSIC ----------------
    private void PlayMusic(string id)
    {
        AudioClip clip = audioDatabase.GetMusic(id);

        if (clip == null)
        {
            Debug.LogWarning("Missing music id: " + id);
            return;
        }

        AudioManager.Instance.PlayMusic(clip);
    }

    // ---------------- SFX ----------------
    private void PlaySFX(string id)
    {
        AudioClip clip = audioDatabase.GetSFX(id);

        if (clip == null)
        {
            Debug.LogWarning("Missing sfx id: " + id);
            return;
        }

        AudioManager.Instance.PlaySFX(clip);
    }

    // ---------------- STOP MUSIC ----------------
    private void StopMusic()
    {
        AudioManager.Instance.StopMusic();
    }

    // ---------------- SCENE ----------------
    private void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangeSceneRoutine(sceneName));
    }

    private IEnumerator ChangeSceneRoutine(string sceneName)
    {
        yield return FadeManager.Instance.FadeOut();
        SceneManager.LoadScene(sceneName);
    }
}