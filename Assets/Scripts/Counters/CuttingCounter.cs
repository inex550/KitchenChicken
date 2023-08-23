using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CuttingCounter : BaseCounter, IProgressEventsSender {

    public event Action<float> OnProgressChanged;
    public static event EventHandler OnAnyCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;

    [Header(Strings.presetFields)]
    [SerializeField] private Animator animator;

    private int cuttingProgress;

    new public static void ResetStaticData() {
        OnAnyCut = null;
    }

    public override void Interact(Player player) {
        if (HasChickenTool()) {
            if (player.HasChickenTool()) {
                if (player.GetChickenTool().TryGetPlate(out PlateChickenTool plate)) {
                    if (plate.TryAddIngredient(GetChickenTool().GetChickenToolSO())) {
                        GetChickenTool().DestroySelf();
                    }
                }
            } else {
                GetChickenTool().SetParent(player);
                ClearCuttingProgress();
            }
        } else {
            if (player.HasChickenTool() && HasCuttingRecipe(player.GetChickenTool().GetChickenToolSO())) {
                player.GetChickenTool().SetParent(this);
                ClearCuttingProgress();
            }
        }
    }

    public override void InteractAlternative(Player player) {
        if (HasChickenTool() && HasCuttingRecipe(GetChickenTool().GetChickenToolSO())) {
            CuttingRecipeSO slicedChickenToolSO = FindCuttingRecipe(GetChickenTool().GetChickenToolSO());

            SetCuttingProgress(cuttingProgress + 1, slicedChickenToolSO);
            animator.SetTrigger(Strings.cuttingCounter_Cut);

            OnAnyCut?.Invoke(this, EventArgs.Empty);

            if (slicedChickenToolSO != null && cuttingProgress >= slicedChickenToolSO.cuttingProgressMax) {
                GetChickenTool().DestroySelf();
                ChickenTool.Spawn(slicedChickenToolSO.sliced, this);

                ClearCuttingProgress();
            }
        }
    }

    private bool HasCuttingRecipe(ChickenToolSO origin) {
        return FindCuttingRecipe(origin) != null;
    }

    private CuttingRecipeSO FindCuttingRecipe(ChickenToolSO origin) {
        foreach (CuttingRecipeSO recipe in cuttingRecipes) {
            if (recipe.origin == origin) return recipe;
        }
        return null;
    }

    private void SetCuttingProgress(int cuttingProgress, CuttingRecipeSO recipe) {
        this.cuttingProgress = cuttingProgress;
        OnProgressChanged?.Invoke((float)cuttingProgress / recipe.cuttingProgressMax);
    }

    private void ClearCuttingProgress() {
        cuttingProgress = 0;
        OnProgressChanged?.Invoke(0.0f);
    }
}