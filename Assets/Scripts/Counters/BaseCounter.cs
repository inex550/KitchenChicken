using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IChickenToolParent {

    public static event EventHandler OnChickenToolDropedHere;

    [SerializeField] private Transform counterTopPoint;

    private ChickenTool chickenTool;

    public abstract void Interact(Player player);

    public virtual void InteractAlternative(Player player) {}

    public static void ResetStaticData() {
        OnChickenToolDropedHere = null;
    }

    public ChickenTool GetChickenTool() {
        return chickenTool;
    }

    public void SetChickenTool(ChickenTool chickenTool) {
        this.chickenTool = chickenTool;
        
        if (chickenTool != null) {
            OnChickenToolDropedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearChickenTool() {
        chickenTool = null;
    }

    public bool HasChickenTool() {
        return chickenTool != null;
    }

    public Transform GetChickenToolFollowPoint() {
        return counterTopPoint;
    }
}
