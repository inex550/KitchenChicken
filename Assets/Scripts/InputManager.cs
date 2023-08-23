using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

    public static InputManager Instance { get; private set; }

    public event Action OnInteractPerformed;
    public event Action OnInteractAlternativePerformed;
    public event Action OnPausePerformed;
    
    public event Action OnBindingRebind;

    public enum Binding {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternative,
        Pause,
    }

    private InputActions inputActions;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        inputActions = new InputActions();
        inputActions.Player.Enable();

        inputActions.Player.Interact.performed += Interact_Performed;
        inputActions.Player.InteractAlternative.performed += InteractAlternative_Performed;
        inputActions.Player.Pause.performed += Pause_Performed;

        if (PlayerPrefs.HasKey(Strings.pref_bindingOverrides)) {
            inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(Strings.pref_bindingOverrides));
        }
    }

    public Vector3 GetMovementVector() {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        return new Vector3(inputVector.x, 0.0f, inputVector.y);
    }

    private void Interact_Performed(InputAction.CallbackContext callback) {
        OnInteractPerformed?.Invoke();
    }

    private void InteractAlternative_Performed(InputAction.CallbackContext callback) {
        OnInteractAlternativePerformed?.Invoke();
    }

    private void Pause_Performed(InputAction.CallbackContext callback) {
        OnPausePerformed?.Invoke();
    }

    public string GetBindingText(Binding binding) {
        switch (binding) {
            case Binding.MoveUp:
                return inputActions.Player.Move.bindings[1].ToDisplayString();
            
            case Binding.MoveDown:
                return inputActions.Player.Move.bindings[2].ToDisplayString();

            case Binding.MoveLeft:
                return inputActions.Player.Move.bindings[3].ToDisplayString();

            case Binding.MoveRight:
                return inputActions.Player.Move.bindings[4].ToDisplayString();

            case Binding.Interact:
                return inputActions.Player.Interact.bindings[0].ToDisplayString();
            
            case Binding.InteractAlternative:
                return inputActions.Player.InteractAlternative.bindings[0].ToDisplayString();

            case Binding.Pause:
                return inputActions.Player.Pause.bindings[0].ToDisplayString();
        }
        return string.Empty;
    }

    public void RebindBinding(Binding binding, Action onCompleteAction) {
        inputActions.Player.Disable();

        InputAction inputAction = null;
        int bindingIndex = 0;

        switch (binding) {
            case Binding.MoveUp:
                inputAction = inputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = inputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = inputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = inputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = inputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternative:
                inputAction = inputActions.Player.InteractAlternative;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = inputActions.Player.Pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                callback.Dispose();
                onCompleteAction.Invoke();
                inputActions.Player.Enable();

                PlayerPrefs.SetString(
                    Strings.pref_bindingOverrides,
                    inputActions.SaveBindingOverridesAsJson()
                );
                PlayerPrefs.Save();

                OnBindingRebind?.Invoke();
            })
            .Start();
    }
}
