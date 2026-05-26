using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Yarn.Unity;

public class DiscoDialoguePresenter : DialoguePresenterBase
{
    [Header("Referencias UI")]
    [SerializeField] private Transform contentContainer;
    [SerializeField] private GameObject lineEntryPrefab; 
    [SerializeField] private ScrollRect scrollRect;

    [Header("Opciones")]
    [SerializeField] private float charactersPerSecond = 35f;
    [SerializeField] private bool autoScrollToBottom = true;

    private bool _hasClicked;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _hasClicked = true;
        }
    }

    public override async YarnTask OnDialogueStartedAsync()
    {
        ClearLog();
        await YarnTask.Delay(1);
    }

    public override async YarnTask RunLineAsync(LocalizedLine dialogueLine, LineCancellationToken cancellationToken)
    {
        GameObject newLineObj = Instantiate(lineEntryPrefab, contentContainer);
        StackingTextDisco lineEntry = newLineObj.GetComponent<StackingTextDisco>();
        
        if (lineEntry != null)
        {
            lineEntry.Setup(dialogueLine.CharacterName, dialogueLine.TextWithoutCharacterName.Text);
        }

        UpdateScroll();
        _hasClicked = false;

        if (lineEntry == null) return;
        TextMeshProUGUI textComponent = lineEntry.DialogueText;
        if (textComponent == null) return;

        textComponent.ForceMeshUpdate();
        int totalCharacters = textComponent.textInfo.characterCount;

        float elapsed = 0f;

        while (textComponent != null && textComponent.maxVisibleCharacters < totalCharacters)
        {
            if (cancellationToken.IsNextContentRequested) return;

            if (_hasClicked || cancellationToken.IsHurryUpRequested)
            {
                _hasClicked = false; 
                break;
            }

            elapsed += Time.deltaTime;
            textComponent.maxVisibleCharacters = Mathf.FloorToInt(elapsed * charactersPerSecond);
            UpdateScroll();

            await YarnTask.Delay(16);
        }

        if (textComponent == null) return;
        textComponent.maxVisibleCharacters = totalCharacters;
        UpdateScroll();

        await YarnTask.Delay(100);
        _hasClicked = false; 

        while (!_hasClicked)
        {
            if (textComponent == null) return;
            
            if (cancellationToken.IsNextContentRequested) return;

            await YarnTask.Delay(16);
        }

        _hasClicked = false;
    }

    public override async YarnTask OnDialogueCompleteAsync()
    {
        await YarnTask.Delay(1);
    }

    private void UpdateScroll()
    {
        if (autoScrollToBottom && scrollRect != null)
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f; 
        }
    }

    public void ClearLog()
    {
        if (contentContainer == null) return;

        foreach (Transform child in contentContainer)
        {
            Destroy(child.gameObject);
        }
    }
}