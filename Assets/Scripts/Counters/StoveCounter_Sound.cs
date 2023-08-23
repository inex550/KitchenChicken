using UnityEngine;

public class StoveCounter_Sound : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;

    private float warningSoundTimerMax = 0.2f;
    private float warningSoundTimer = 0.0f;

    bool playWarningSound = false;

    private void Start() {
        audioSource = GetComponent<AudioSource>();

        stoveCounter.OnTurnStateChanged += StoveCounter_OnTurnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged (float progress) {
        playWarningSound = progress > StoveCounter_Visual.warningShowProgressAmount && stoveCounter.IsFried();
    }

    private void StoveCounter_OnTurnStateChanged(bool isTurnedOn) {
        if (isTurnedOn) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }

    private void Update() {
        if (playWarningSound) {
            warningSoundTimer += Time.deltaTime;
            if (warningSoundTimer > warningSoundTimerMax) {
                warningSoundTimer = 0.0f;
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}