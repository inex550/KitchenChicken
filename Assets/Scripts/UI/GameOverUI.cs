using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    [SerializeField] private Button restartButton;

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        restartButton.onClick.AddListener(RestartButton_OnClick);

        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged() {
        gameObject.SetActive(GameManager.Instance.IsGameOver());

        recipesDeliveredText.text = DeliveryManager.Instance.GetDeliveredRecipesCount().ToString();
    }

    private void RestartButton_OnClick() {
        SceneLoader.Load(SceneLoader.GameScene.GameScene);
    }
}