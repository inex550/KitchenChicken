using UnityEngine;
using UnityEngine.UI;

public class SceneLoadingUI : MonoBehaviour, ISceneLoadingProgressHandler {

    [SerializeField] private Image progressImage;

    private int updatesCount = 0;

    private void Update() {
        updatesCount += 1;
        if (updatesCount == 2) {
            StartCoroutine(SceneLoader.TargetSceneLoadingCoroutine(this));
        }
    }

    public void HandleProgress(float progress) {
        progressImage.fillAmount = progress;
    }
}