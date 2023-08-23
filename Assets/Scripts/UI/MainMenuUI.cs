using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

    [SerializeField] private Button playButton;

    [SerializeField] private Button quitButton;

    private void Start() {
        playButton.onClick.AddListener(PlayButton_OnClick);
        quitButton.onClick.AddListener(QuitButton_OnClick);
    }

    private void PlayButton_OnClick() {
        SceneLoader.Load(SceneLoader.GameScene.GameScene);
    }

    private void QuitButton_OnClick() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}