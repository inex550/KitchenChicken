using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour {

    [SerializeField] private PlateChickenTool plateChickenTool;

    [SerializeField] private Transform plateIconPrefab;

    private void Start() {
        plateChickenTool.OnIngredientAdded += OnIngredientAdded;
    }

    private void OnIngredientAdded(ChickenToolSO ingredient) {
        Transform plateIconTransform = Instantiate(plateIconPrefab, transform);
        PlateIconUI plateIcon = plateIconTransform.GetComponent<PlateIconUI>();
        plateIcon.SetChickenTool(ingredient);
    }
}