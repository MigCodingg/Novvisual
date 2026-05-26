using TMPro;
using UnityEngine;

public class StackingTextDisco : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    public TextMeshProUGUI DialogueText => dialogueText;

    public void Setup(string characterName, string text)
    {
        if (string.IsNullOrEmpty(characterName))
        {
            characterNameText.gameObject.SetActive(false);
        }
        else
        {
            characterNameText.gameObject.SetActive(true);
            characterNameText.text = characterName;
        }

        dialogueText.text = text;
        dialogueText.maxVisibleCharacters = 0; 
    }
}