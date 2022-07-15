using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationDialog : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private Action _onConfirmCallback;

    private void Awake()
    {
        yesButton.onClick.AddListener(YesButton_OnClick);
        noButton.onClick.AddListener(NoButton_OnClick);
    }

    private void OnDestroy()
    {
        yesButton.onClick.RemoveListener(YesButton_OnClick);
        noButton.onClick.RemoveListener(NoButton_OnClick);
    }

    public void Show(Action onConfirm)
    {
        _onConfirmCallback = onConfirm;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _onConfirmCallback = null;
        gameObject.SetActive(false);
    }

    
    private void YesButton_OnClick()
    {
        _onConfirmCallback?.Invoke();
        Hide();
    }

    private void NoButton_OnClick()
    {
        Hide();
    }
}