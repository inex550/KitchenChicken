using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateChickenTool : ChickenTool {

    public event Action<ChickenToolSO> OnIngredientAdded;

    [Serializable]
    private struct ChickenToolObject {
        public ChickenToolSO chickenTool;
        public GameObject gameObject;
    }

    [SerializeField] private List<ChickenToolSO> availableChickenTools;
    [SerializeField] private List<ChickenToolObject> chickenToolObjects;
    [SerializeField] private Transform plateCompleteVisual;

    private List<ChickenToolSO> addedChickenTools;

    private bool hasBread = false;

    private float offsetIfNoBread = -0.2f;

    private void Awake() {
        addedChickenTools = new List<ChickenToolSO>();
    }

    private void Start() {
        foreach (ChickenToolObject chickenToolObject in chickenToolObjects) {
            chickenToolObject.gameObject.SetActive(false);
        }
    }

    public bool TryAddIngredient(ChickenToolSO chickenTool) {
        bool canBeAdded = !addedChickenTools.Contains(chickenTool) && availableChickenTools.Contains(chickenTool);

        if (canBeAdded) {
            if (!hasBread && chickenTool.toolName == Strings.chickenTool_Bread) hasBread = true;

            plateCompleteVisual.localPosition = new Vector3(0f, hasBread ? 0 : offsetIfNoBread, 0);

            foreach (ChickenToolObject chickenToolObject in chickenToolObjects) {
                if (chickenToolObject.chickenTool == chickenTool) {
                    chickenToolObject.gameObject.SetActive(true);
                }
            }

            addedChickenTools.Add(chickenTool);

            OnIngredientAdded?.Invoke(chickenTool);
        }

        return canBeAdded;
    }

    public List<ChickenToolSO> GetChickenToolList() {
        return addedChickenTools;
    }
}