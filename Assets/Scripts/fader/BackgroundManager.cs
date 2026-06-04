using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager Instance;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private float fadeDuration = 1f;

    private Coroutine routine;

    private void Awake()
    {
        Instance = this;
        
    }
        private void Start()
    {
        Color c = backgroundImage.color;
        c.a = 0f;
        backgroundImage.color = c;
    }

    public void SetBackground(Sprite sprite)
    {
        if (sprite == null) return;

        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(FadeTo(sprite));
    }

    private IEnumerator FadeTo(Sprite newSprite)
    {
        Color c = backgroundImage.color;

        // FADE OUT
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            c.a = Mathf.Lerp(1, 0, t / fadeDuration);
            backgroundImage.color = c;
            yield return null;
        }

        backgroundImage.sprite = newSprite;

        // FADE IN
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            c.a = Mathf.Lerp(0, 1, t / fadeDuration);
            backgroundImage.color = c;
            yield return null;
        }
    }
    
}