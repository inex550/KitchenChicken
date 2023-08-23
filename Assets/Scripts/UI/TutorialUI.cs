using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternativeText;
    [SerializeField] private TextMeshProUGUI pauseText;

    private void Start() {
        UpdateVisual();

        InputManager.Instance.OnBindingRebind += InputManager_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void OnDestroy() {
        InputManager.Instance.OnBindingRebind -= InputManager_OnBindingRebind;
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void InputManager_OnBindingRebind() {
        UpdateVisual();
    }

    private void GameManager_OnStateChanged() {
        if (GameManager.Instance.IsWaitingToStart()) {
            Show();
        } else {
            Hide();
        }
    }

    private void UpdateVisual() {
        moveUpText.text = InputManager.Instance.GetBindingText(InputManager.Binding.MoveUp);
        moveDownText.text = InputManager.Instance.GetBindingText(InputManager.Binding.MoveDown);
        moveLeftText.text = InputManager.Instance.GetBindingText(InputManager.Binding.MoveLeft);
        moveRightText.text = InputManager.Instance.GetBindingText(InputManager.Binding.MoveRight);
        interactText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Interact);
        interactAlternativeText.text = InputManager.Instance.GetBindingText(InputManager.Binding.InteractAlternative);
        pauseText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Pause);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}