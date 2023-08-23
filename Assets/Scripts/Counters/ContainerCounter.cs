using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IChickenToolParent {

    [SerializeField] private ChickenToolSO chickenToolSO;
    [SerializeField] private SpriteRenderer chickenToolSpriteRenderer;

    [SerializeField] private Animator containerCounterAnimator;

    private void Start() {
        chickenToolSpriteRenderer.sprite = chickenToolSO.sprite;
    }

    public override void Interact(Player player) {
        if (player.HasChickenTool()) {
            if (player.GetChickenTool().TryGetPlate(out PlateChickenTool plate)) {
                if (plate.TryAddIngredient(chickenToolSO)) {
                    containerCounterAnimator.SetTrigger(Strings.containerCounter_OpenClose);   
                }
            } else if (chickenToolSO == player.GetChickenTool().GetChickenToolSO()) {
                containerCounterAnimator.SetTrigger(Strings.containerCounter_OpenClose);
                
                player.GetChickenTool().SetParent(this);
                GetChickenTool().DestroySelf();
            }
        } else {
            containerCounterAnimator.SetTrigger(Strings.containerCounter_OpenClose);

            ChickenTool.Spawn(chickenToolSO, player);
        }
    }

    private void OnValidate() {
        if (chickenToolSpriteRenderer != null && chickenToolSO != null) {
            chickenToolSpriteRenderer.sprite = chickenToolSO.sprite;
        }
    }
}
