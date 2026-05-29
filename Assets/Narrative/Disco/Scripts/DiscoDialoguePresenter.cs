using System.Collections.Generic;
using TMPro;
using TypeChecker;
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
    [SerializeField] private GameObject optionEntryPrefab;
    [SerializeField] private Color defaultColor = Color.orange;
    [SerializeField] private Color selectedColor = Color.white;

    [Header("Configuración")]
    [SerializeField] private bool useTypeWritter = true;
    [SerializeField] private float charactersPerSecond = 35f;
    [SerializeField] private bool autoScrollToBottom = true;

    [Header("Characters")]
    [SerializeField] private Image portraitObject;
    [SerializeField] private List<CharacterData> _charactersList = new List<CharacterData>();

    private bool _hasClicked;
    private bool _hasChosen;
    private bool _pressedUp;
    private bool _pressedDown;
    private bool _pressedEnter;



    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _hasClicked = true;
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            _pressedEnter = true;
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            _pressedUp = true;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            _pressedDown = true;
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
            Sprite portrait = SetCharacterPortrait(dialogueLine.CharacterName);
            if (portrait != null) portraitObject.sprite = portrait;
        }

        UpdateScroll();
        _hasClicked = false;

        if (lineEntry == null) return;
        TextMeshProUGUI textComponent = lineEntry.DialogueText;
        if (textComponent == null) return;

        textComponent.ForceMeshUpdate();
        int totalCharacters = textComponent.textInfo.characterCount;

        float elapsed = 0f;

        while (textComponent != null && textComponent.maxVisibleCharacters < totalCharacters && useTypeWritter)
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

    public override async YarnTask<DialogueOption> RunOptionsAsync(DialogueOption[] dialogueOptions, LineCancellationToken cancellationToken)
    {
        _hasChosen = false;
        int _selectedIndex = 0;
        List<OptionEntryDisco> _currentOptions = new List<OptionEntryDisco>();
        for (int i = 0; i < dialogueOptions.Length; i++)
        {
            DialogueOption child = dialogueOptions[i];
            {
                GameObject newOptionObj = Instantiate(optionEntryPrefab, contentContainer);
                OptionEntryDisco optionEntry = newOptionObj.GetComponent<OptionEntryDisco>();

                if (optionEntry != null)
                {
                    optionEntry.SetUp(i + 1, child.Line.TextWithoutCharacterName.Text, defaultColor, selectedColor);
                    _currentOptions.Add(optionEntry);
                }
            }
        }
        _currentOptions[0].SetColor(true);

        while (!_hasChosen)
        {
            int _currentIndex = _selectedIndex;
            if (_pressedEnter)
            {
                _hasChosen = true;
                _pressedEnter = false;

                GameObject newLineObj = Instantiate(lineEntryPrefab, contentContainer);
                StackingTextDisco lineEntry = newLineObj.GetComponent<StackingTextDisco>();
                lineEntry.Setup(dialogueOptions[_selectedIndex].Line.CharacterName,
                    dialogueOptions[_selectedIndex].Line.TextWithoutCharacterName.Text);
                lineEntry.DialogueText.ForceMeshUpdate();
                lineEntry.DialogueText.maxVisibleCharacters = lineEntry.DialogueText.textInfo.characterCount;

                for (int i = 0; i < _currentOptions.Count; i++)
                {
                    Destroy(_currentOptions[i].gameObject);
                }
                return dialogueOptions[_selectedIndex];
            }
            else if (_pressedUp)
            {
                if (_selectedIndex != 0)
                {
                    --_selectedIndex;
                }
            }
            else if (_pressedDown)
            {
                if (_selectedIndex != dialogueOptions.Length - 1)
                {
                    ++_selectedIndex;
                }
            }
            _pressedUp = false;
            _pressedDown = false;
            if (_currentIndex != _selectedIndex)
            {
                for (int i = 0; i < dialogueOptions.Length; i++)
                {
                    if (_selectedIndex == i)
                    {
                        _currentOptions[i].SetColor(true);
                    }
                    else
                    {
                        _currentOptions[i].SetColor(false);
                    }
                }
            }
            await YarnTask.Delay(16);
        }
        return dialogueOptions[0];
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


    public Sprite SetCharacterPortrait(string characterName)
    {
        if (string.IsNullOrEmpty(characterName)) return null;
        if (_charactersList == null || _charactersList.Count == 0)
        {
            Debug.LogError("La lista de personajes esta empty :((((");
            return null;
        }

        for (int i = 0; i < _charactersList.Count; i++)
        {
            if (characterName == _charactersList[i].characterName)
            {
                return _charactersList[i].portrait;
            }
        }
        Debug.LogError("No hay nombre de personaje que conicida con los de la lista :/");
        return null;
    }
}