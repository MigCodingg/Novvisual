using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class YarnSpriteCommands : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private Image storyImage;

    private void Awake()
    {
        dialogueRunner.AddCommandHandler(
            "show_image",
            ShowImage
        );

        dialogueRunner.AddCommandHandler(
            "hide_image",
            HideImage
        );
    }

   private void ShowImage()
{
    Debug.Log("SHOW IMAGE CALLED");

    storyImage.gameObject.SetActive(true);

    Color c = storyImage.color;
    c.a = 1f;
    storyImage.color = c;
}
    private void HideImage()
    {
        Debug.Log("HIDE IMAGE CALLED");
        storyImage.gameObject.SetActive(false);
    }
}