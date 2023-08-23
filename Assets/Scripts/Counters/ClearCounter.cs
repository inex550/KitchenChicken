using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IChickenToolParent {

    public override void Interact(Player player) {
        if (HasChickenTool()) {
            if (player.HasChickenTool()) {
                if (player.GetChickenTool().TryGetPlate(out PlateChickenTool plate)) {
                    if (plate.TryAddIngredient(GetChickenTool().GetChickenToolSO())) {
                        GetChickenTool().DestroySelf();
                    }
                } else if (GetChickenTool().TryGetPlate(out plate)) {
                    if (plate.TryAddIngredient(player.GetChickenTool().GetChickenToolSO())) {
                        player.GetChickenTool().DestroySelf();
                    }
                }
            } else {
                GetChickenTool().SetParent(player);
            }
        } else {
            if (player.HasChickenTool()) player.GetChickenTool().SetParent(this);
        }
    }
}
