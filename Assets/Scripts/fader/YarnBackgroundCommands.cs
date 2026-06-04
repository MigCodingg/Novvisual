using UnityEngine;
using Yarn.Unity;

public class YarnBackgroundCommands : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private BackgroundDatabase database;

    private void Awake()
    {
        dialogueRunner.AddCommandHandler<string>(
            "background",
            ChangeBackground
        );
    }

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
}