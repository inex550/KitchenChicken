using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;

    int previousCountdownNumber;

    private void Start() {
        animator = GetComponent<Animator>();

        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged() {
        gameObject.SetActive(GameManager.Instance.IsCountdownToStartActive());
    }

    private void Update () {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();

        if (countdownNumber != previousCountdownNumber) {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(Strings.trigger_popupNumber);
            SoundManager.Instance.PlayCountdownSound();
        }

    }
}