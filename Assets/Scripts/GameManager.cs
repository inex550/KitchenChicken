using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public event Action OnStateChanged;
    public event Action<bool> OnGamePauseStateChanged;

    public static GameManager Instance { get; private set; }

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaing,
        GameOver,
    }

    private State state;

    private float countdownToStartTimerMax = 3.0f;
    private float gamePlatingTimerMax = 120.0f;

    private float waitingToStartTimer;
    private float countdownToStartTimer;
    private float gamePlayingTimer;

    private bool isGamePaused = false;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        state = State.WaitingToStart;
    }

    private void Start() {
        InputManager.Instance.OnPausePerformed += InputManager_OnPausePerformed;
        InputManager.Instance.OnInteractPerformed += InputManager_OnInteractPerformed;
    }

    private void OnDestroy() {
        InputManager.Instance.OnPausePerformed -= InputManager_OnPausePerformed;
        InputManager.Instance.OnInteractPerformed -= InputManager_OnInteractPerformed;
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                State_WaitingToStart();
                break;

            case State.CountdownToStart:
                State_CountdownToStart();
                break;
            
            case State.GamePlaing:
                State_GamePlaying();
                break;

            case State.GameOver:
                State_GameOver();
                break;
        }
    }

    private void InputManager_OnPausePerformed() {
        TogglePauseGame();
    }

    private void InputManager_OnInteractPerformed() {
        if (state == State.WaitingToStart) {
            ChangeState(State.CountdownToStart);
        }
    }

    private void TogglePauseGame() {
        if (isGamePaused) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }

    public void ResumeGame() {
        isGamePaused = false;
        Time.timeScale = 1.0f;
        OnGamePauseStateChanged?.Invoke(isGamePaused);
    }

    private void PauseGame() {
        isGamePaused = true;
        Time.timeScale = 0.0f;
        OnGamePauseStateChanged?.Invoke(isGamePaused);
    }

    private void State_WaitingToStart() {

    }

    private void State_CountdownToStart() {
        countdownToStartTimer += Time.deltaTime;
        if (countdownToStartTimer >= countdownToStartTimerMax) {
            countdownToStartTimer = 0.0f;
            ChangeState(State.GamePlaing);
        }
    }

    private void State_GamePlaying() {
        gamePlayingTimer += Time.deltaTime;
        if (gamePlayingTimer >= gamePlatingTimerMax) {
            gamePlayingTimer = 0.0f;
            ChangeState(State.GameOver);
        }
    }

    private void State_GameOver() {}

    private void ChangeState(State newState) {
        state = newState;
        OnStateChanged?.Invoke();
    }

    public bool IsWaitingToStart() {
        return state == State.WaitingToStart;
    }

    public bool IsCountdownToStartActive() {
        return state == State.CountdownToStart;
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaing;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetPlayingTimerNormalized() {
        return 1 - (gamePlayingTimer / gamePlatingTimerMax);
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimerMax - countdownToStartTimer;
    }
}