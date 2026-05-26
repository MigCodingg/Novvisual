using UnityEngine;
using Yarn.Unity;

public class YarnPortraitCommands : MonoBehaviour
{
    public PortraitManager portraitManager;

    public void ShowPortrait(string characterName)
    {
        portraitManager.ShowPortrait(characterName);
    }

    public void HidePortrait()
    {
        portraitManager.HidePortrait();
    }

    private void Start()
    {
        DialogueRunner runner = FindObjectOfType<DialogueRunner>();

        runner.AddCommandHandler<string>("portrait", ShowPortrait);

        runner.AddCommandHandler("hideportrait", HidePortrait);
    }
}