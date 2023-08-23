using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;

    private void Start() {
        GameManager.Instance.OnGamePauseStateChanged += GameManager_OnGamePauseStateChanged;

        resumeButton.onClick.AddListener(ResumeButton_OnClick);
        mainMenuButton.onClick.AddListener(MainMenuButton_OnClick);
        optionsButton.onClick.AddListener(OptionsButton_OnClick);

        Hide();
    }

    private void OnDestroy() {
        GameManager.Instance.OnGamePauseStateChanged -= GameManager_OnGamePauseStateChanged;
    }

    private void GameManager_OnGamePauseStateChanged(bool isGamePaused) {
        if (isGamePaused) {
            Show();
        } else {
            Hide();
        }
    }

    private void ResumeButton_OnClick() {
        GameManager.Instance.ResumeGame();
    }

    private void OptionsButton_OnClick() {
        Hide();
        OptionsUI.Instance.Show(Show);
    }

    private void MainMenuButton_OnClick() {
        GameManager.Instance.ResumeGame();
        SceneLoader.Load(SceneLoader.GameScene.MainMenuScene);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
