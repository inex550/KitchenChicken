using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {

    public static DeliveryCounter Instance { get; private set; }

    [SerializeField] private DeliveryManager deliveryManager;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public override void Interact(Player player) {
        if (player.HasChickenTool() && player.GetChickenTool().TryGetPlate(out PlateChickenTool plate)) {
            DeliveryManager.Instance.Deliver(plate.GetChickenToolList());
            player.GetChickenTool().DestroySelf();
        }
    }
}