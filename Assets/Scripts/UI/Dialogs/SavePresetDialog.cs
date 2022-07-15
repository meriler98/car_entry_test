using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SavePresetDialog : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_InputField nameInput;

    private Action<string> _onSavePressedCallback;

    public string Name => nameInput.text;

    private void Awake()
    {
        saveButton.onClick.AddListener(SaveButton_OnClick);
        backButton.onClick.AddListener(BackButton_OnClick);
    }

    private void OnDestroy()
    {
        saveButton.onClick.RemoveListener(SaveButton_OnClick);
        saveButton.onClick.RemoveListener(BackButton_OnClick);
    }

    public void Show(Action<string> onSaveCallback = null)
    {
        gameObject.SetActive(true);
        nameInput.text = "";
        _onSavePressedCallback = onSaveCallback;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _onSavePressedCallback = null;
    }


    private void SaveButton_OnClick()
    {
        _onSavePressedCallback?.Invoke(Name);
        Hide();
    }

    private void BackButton_OnClick()
    {
        Hide();
    }
}
