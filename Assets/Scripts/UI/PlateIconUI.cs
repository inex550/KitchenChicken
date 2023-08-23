using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour {
    
    [SerializeField] private Image image;
    [SerializeField] private Image background;
    [SerializeField] bool backgroundActive = true;

    private void Start() {
        SetBackgroundActive(backgroundActive);
    }

    public void SetChickenTool(ChickenToolSO chickenTool) {
        image.sprite = chickenTool.sprite;
    }

    public void SetBackgroundActive(bool isActive) {
        backgroundActive = isActive;
        background.gameObject.SetActive(isActive);
    }
}
