using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TranscriptController : MonoBehaviour
{
    public Transform content;
    public GameObject linePrefab;
    public ScrollRect scrollRect;

    public void AddLine(string speaker, string text)
    {
        GameObject line = Instantiate(linePrefab, content);

        TMP_Text[] texts = line.GetComponentsInChildren<TMP_Text>();

        texts[0].text = speaker;
        texts[1].text = text;

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
    void Start()
{
    AddLine("KIM", "The air smells wrong.");
    AddLine("YOU", "Look around.");
    AddLine("KIM", "Stay alert.");
}
}