using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IChickenToolParent {

    public event Action OnPickedSomething;

    public static Player Instance { get; private set; }

    public event EventHandler<SelectedCounterEventArgs> OnSelectedCounterChanged;

    public class SelectedCounterEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float interactDistance = 2.0f;
    [SerializeField] private Transform playerHand;

    [SerializeField] private float stepsTimerMax = 0.1f;
    private float stepsTimer;

    private bool isWalking = false;
    public bool IsWalking => isWalking;

    private Vector3 lookingDir;

    private BaseCounter selectedCounter;

    private ChickenTool chickenTool;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start() {
        InputManager.Instance.OnInteractPerformed += InputManager_OnInteractPerformed;
        InputManager.Instance.OnInteractAlternativePerformed += InputManager_OnInteractAlternativePerformed;
    }

    private void Update() {
        Move();
        CheckInteractions();
        TimerSteps();
    }

    private void CheckInteractions() {
        if (
            GameManager.Instance.IsGamePlaying() &&
            Physics.Raycast(transform.position, lookingDir, out RaycastHit hit, interactDistance)
        ) {
            if (hit.transform.TryGetComponent(out BaseCounter counter)) {
                ChangeSelectedCounter(counter);
            } else {
                ChangeSelectedCounter(null);
            }
        } else {
            ChangeSelectedCounter(null);
        }
    }

    private void InputManager_OnInteractPerformed() {
        if (GameManager.Instance.IsGamePlaying()) {
            selectedCounter?.Interact(this);
        }
    }

    private void InputManager_OnInteractAlternativePerformed() {
        if (GameManager.Instance.IsGamePlaying()) {
            selectedCounter?.InteractAlternative(this);
        }
    }

    private void Move() {
        Vector3 moveVector = InputManager.Instance.GetMovementVector();

        float moveDistance = speed * Time.deltaTime;
        bool canMove = CanMove(moveVector, moveDistance);

        if (canMove) {
            transform.position += moveVector * moveDistance;
        } else {
            Vector3 moveX = new Vector3(moveVector.x, 0.0f, 0.0f);
            canMove = CanMove(moveX, moveDistance);

            if (canMove) {
                transform.position += moveX * moveDistance;
            } else {
                Vector3 moveZ = new Vector3(0.0f, 0.0f, moveVector.z);
                canMove = CanMove(moveZ, moveDistance);

                if (canMove) {
                    transform.position += moveZ * moveDistance;
                }
            }
        }

        isWalking = moveVector != Vector3.zero;

        if (isWalking) {
            lookingDir = moveVector;
            transform.forward = Vector3.Slerp(transform.forward, moveVector, rotationSpeed * Time.deltaTime);
        }
    }

    private bool CanMove(Vector3 moveVector, float moveDistance) {
        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up, playerRadius, moveVector, moveDistance);
    }

    private void TimerSteps() {
        stepsTimer += Time.deltaTime;
        if (stepsTimer >= stepsTimerMax) {
            stepsTimer = 0.0f;

            if (isWalking) {
                SoundManager.Instance.PlayFootstepSound(transform.position);
            }
        }
    }

    private void ChangeSelectedCounter(BaseCounter counter) {
        if (selectedCounter != counter) {
            selectedCounter = counter;
            OnSelectedCounterChanged?.Invoke(this, new SelectedCounterEventArgs {
                selectedCounter = counter
            });
        }
    }

    public ChickenTool GetChickenTool() {
        return chickenTool;
    }

    public void SetChickenTool(ChickenTool chickenTool) {
        this.chickenTool = chickenTool;
        if (chickenTool != null) {
            OnPickedSomething?.Invoke();
        }
    }

    public void ClearChickenTool() {
        SetChickenTool(null);
    }

    public bool HasChickenTool() {
        return chickenTool != null;
    }

    public Transform GetChickenToolFollowPoint() {
        return playerHand;
    }
}
