using UnityEngine;

public class StoveCounter_Visual : MonoBehaviour {

    public const float warningShowProgressAmount = 0.5f;

    [SerializeField] private StoveCounter stoveCounter;

    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    [SerializeField] private Animator stoveBurnFlashingBarAnimator;
    [SerializeField] private GameObject stoveBurnWarningUiGameObject;

    private void Start() {
        stoveCounter.OnTurnStateChanged += StoveCounter_OnTurnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        stoveBurnWarningUiGameObject.SetActive(false);
    }
    
    private void StoveCounter_OnProgressChanged (float progress) {
        bool show = progress > warningShowProgressAmount && stoveCounter.IsFried();
        
        stoveBurnFlashingBarAnimator.SetBool(Strings.trigger_isFlashing, show);
        stoveBurnWarningUiGameObject.SetActive(show);
    }

    private void StoveCounter_OnTurnStateChanged(bool isTurned) {
        stoveOnGameObject.SetActive(isTurned);
        particlesGameObject.SetActive(isTurned);
    }
}