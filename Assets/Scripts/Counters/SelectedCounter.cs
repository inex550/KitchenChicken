using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour {

    [SerializeField] private BaseCounter counter;
    [SerializeField] private List<GameObject> visualGameObjects;

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
    }

    private void OnDestroy() {
        Player.Instance.OnSelectedCounterChanged -= OnSelectedCounterChanged;
    }

    private void OnSelectedCounterChanged(object sender, Player.SelectedCounterEventArgs args) {
        SetShow(args.selectedCounter == counter);
    }

    private void SetShow(bool isShow) {
        foreach (GameObject visualGameObject in visualGameObjects) {
            visualGameObject.SetActive(isShow);
        }
    }
}
