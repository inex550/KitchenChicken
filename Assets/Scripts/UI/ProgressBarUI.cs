using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {

    public class ProgressEventArgs: EventArgs {
        public float progress;
    }

    [SerializeField] private GameObject progressEventsSenderObject;
    [SerializeField] private Image image;

    private IProgressEventsSender progressEventsSender;

    private void Start() {
        progressEventsSender = progressEventsSenderObject.GetComponent<IProgressEventsSender>();

        progressEventsSender.OnProgressChanged += OnProgressChanged;
        image.fillAmount = 0.0f;

        gameObject.SetActive(false);
    }

    private void OnProgressChanged(float progress) {
        image.fillAmount = progress;

        gameObject.SetActive(progress != 0.0f && progress != 1.0f);
    }
}