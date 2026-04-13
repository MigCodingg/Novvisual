using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class VisualNovelController : MonoBehaviour
{
    [SerializeField] private Image charImage;

    private DialogueRunner runner;

    void Awake()
    {
        runner = GetComponent<DialogueRunner>();
    }
    void Start()
    {
        runner.AddCommandHandler<float>("fade_character", FadeCharacter);
    }
    public IEnumerator FadeCharacter(float tiempo)
    {
        charImage.gameObject.SetActive(true);
        Color colorImage = charImage.color;
        colorImage = charImage.color;
        colorImage.a = 0;
        charImage.color = colorImage;

        float t = 0;

        while(t < tiempo)
        {
            t += Time.deltaTime;
            colorImage.a = t /tiempo;
            charImage.color = colorImage;
            yield return null;
        }
    }
}
