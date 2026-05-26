using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PortraitManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterPortrait
    {
        public string characterName;
        public Sprite portraitSprite;
    }

    public Image portraitImage;
    public List<CharacterPortrait> characterPortraits;

    private Dictionary<string, Sprite> portraitDictionary;

    void Awake()
    {
        portraitDictionary = new Dictionary<string, Sprite>();

        foreach (CharacterPortrait cp in characterPortraits)
        {
            portraitDictionary[cp.characterName] = cp.portraitSprite;
        }

        portraitImage.enabled = false;
    }

    public void ShowPortrait(string characterName)
    {
        if (portraitDictionary.ContainsKey(characterName))
        {
            portraitImage.sprite = portraitDictionary[characterName];
            portraitImage.enabled = true;
        }
        else
        {
            Debug.LogWarning("No portrait found for " + characterName);
        }
    }

    public void HidePortrait()
    {
        portraitImage.enabled = false;
    }
}