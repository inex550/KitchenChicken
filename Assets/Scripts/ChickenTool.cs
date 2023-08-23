using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenTool : MonoBehaviour {

    [SerializeField] private ChickenToolSO chickenToolSO;

    private IChickenToolParent chickenToolParent;

    public ChickenToolSO GetChickenToolSO() {
        return chickenToolSO;
    }

    public void SetParent(IChickenToolParent chickenToolParent) {
        if (this.chickenToolParent != null) {
            this.chickenToolParent.ClearChickenTool();
        }

        this.chickenToolParent = chickenToolParent;

        if (chickenToolParent.HasChickenTool()) {
            Debug.LogError($"{chickenToolParent} already has a ChickenTool!");
        }

        chickenToolParent.SetChickenTool(this);

        transform.parent = chickenToolParent.GetChickenToolFollowPoint();
        transform.localPosition = Vector3.zero;
    }

    public IChickenToolParent GetParent() {
        return chickenToolParent;
    }

    public void DestroySelf() {
        GetParent()?.ClearChickenTool();
        Destroy(gameObject);
    }

    public static ChickenTool Spawn(ChickenToolSO chickenToolSO, IChickenToolParent parent) {
        Transform chickenToolTransform = Instantiate(chickenToolSO.prefab);
        ChickenTool chickenTool = chickenToolTransform.GetComponent<ChickenTool>();

        chickenTool.SetParent(parent);

        return chickenTool;
    }

    public bool TryGetPlate(out PlateChickenTool plate) {
        if (this is PlateChickenTool) {
            plate = this as PlateChickenTool;
            return true;
        } else {
            plate = null;
            return false;
        }
    }
}
