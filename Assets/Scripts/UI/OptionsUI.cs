using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject pressKeyToRebindUiObject;

    [Header("Sound Effects Button")]
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private TextMeshProUGUI soundEffectsButtonText;

    [Header("Music Button")]
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicButtonText;

    [Header("Binding Buttons")]
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternativeButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private TextMeshProUGUI moveUpButtonText;
    [SerializeField] private TextMeshProUGUI moveDownButtonText;
    [SerializeField] private TextMeshProUGUI moveLeftButtonText;
    [SerializeField] private TextMeshProUGUI moveRightButtonText;
    [SerializeField] private TextMeshProUGUI interactButtonText;
    [SerializeField] private TextMeshProUGUI interactAlternativeButtonText;
    [SerializeField] private TextMeshProUGUI pauseButtonText;

    private Action onCloseAction;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start() {
        GameManager.Instance.OnGamePauseStateChanged += GameManager_OnGamePauseStateChanged;

        soundEffectsButton.onClick.AddListener(SoundEffectsButton_OnClick);
        musicButton.onClick.AddListener(MusicButton_OnClick);
        closeButton.onClick.AddListener(CloseButton_OnClick);

        moveUpButton.onClick.AddListener(() => RebindBinding(InputManager.Binding.MoveUp));
        moveDownButton.onClick.AddListener(() => RebindBinding(InputManager.Binding.MoveDown));
        moveLeftButton.onClick.AddListener(() => RebindBinding(InputManager.Binding.MoveLeft));
        moveRightButton.onClick.AddListener(() => RebindBinding(InputManager.Binding.MoveRight));
        interactButton.onClick.AddListener(() => RebindBinding(InputManager.Binding.Interact));
        interactAlternativeButton.onClick.AddListener(() => RebindBinding(InputManager.Binding.InteractAlternative));
        pauseButton.onClick.AddListener(() => RebindBinding(InputManager.Binding.Pause));

        UpdateVisual();

        Hide();
    }

    private void SoundEffectsButton_OnClick() {
        SoundManager.Instance.ChangeVolume();
        UpdateVisual();
    }

    private void MusicButton_OnClick() {
        MusicManager.Instance.ChangeVolume();
        UpdateVisual();
    }

    private void CloseButton_OnClick() {
        Hide();
        onCloseAction?.Invoke();
    }

    private void RebindBinding(InputManager.Binding binding) {
        ShowPressKeyToRebindUI();
        InputManager.Instance.RebindBinding(binding, () => {
            UpdateVisual();
            HidePressKeyToRebindUI();
        });
    }

    private void GameManager_OnGamePauseStateChanged(bool isPaused) {
        if (!isPaused) Hide();
    }

    private void UpdateVisual() {
        soundEffectsButtonText.text = string.Format(
            Strings.options_soundEffects,
            Mathf.Round(SoundManager.Instance.GetVolume() * 10.0f)
        );

        musicButtonText.text = string.Format(
            Strings.options_music,
            Mathf.Round(MusicManager.Instance.GetVolume() * 10.0f)
        );

        moveUpButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.MoveUp);
        moveDownButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.MoveDown);
        moveLeftButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.MoveLeft);
        moveRightButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.MoveRight);
        interactButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Interact);
        interactAlternativeButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.InteractAlternative);
        pauseButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Pause);
    }

    public void Show(Action onCloseAction = null) {
        this.onCloseAction = onCloseAction;
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void ShowPressKeyToRebindUI() {
        pressKeyToRebindUiObject.SetActive(true);
    }

    private void HidePressKeyToRebindUI() {
        pressKeyToRebindUiObject.SetActive(false);
    }
}