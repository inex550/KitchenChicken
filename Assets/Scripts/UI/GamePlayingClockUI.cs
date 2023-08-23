using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour {

    [SerializeField] private Image clockImage;

    [SerializeField] private Color clockColorOnTimerMax;
    [SerializeField] private Color clockColorOnTimerMin;

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void Update() {
        float progress = GameManager.Instance.GetPlayingTimerNormalized();
        clockImage.fillAmount = progress;
        clockImage.color = Color.Lerp(clockColorOnTimerMin, clockColorOnTimerMax, progress);
    }

    private void GameManager_OnStateChanged() {

    }
}