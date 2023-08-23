using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IProgressEventsSender {

    public event Action<bool> OnTurnStateChanged;

    public event Action<float> OnProgressChanged;

    public enum FryingState {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipes;

    private float fryingTimer = 0.0f;

    private FryingRecipeSO currentFryingRecipe;

    private FryingState fryingState;

    public override void Interact(Player player) {
        if (HasChickenTool()) {
            // Has a chicken tool on the counter
            if (player.HasChickenTool()) {
                // Player has chicken tool
                if (player.GetChickenTool().TryGetPlate(out PlateChickenTool plate)) {
                    // Chicken tool on the counter is a plate
                    if (plate.TryAddIngredient(GetChickenTool().GetChickenToolSO())) {
                        GetChickenTool().DestroySelf();
                        fryingState = FryingState.Idle;
                    }
                }
            } else {
                // Player has no chicken tool
                GetChickenTool().SetParent(player);
                fryingState = FryingState.Idle;
            }

            if (!HasChickenTool()) {
                // If there no chicken tool now
                ClearFryingTimer();
                currentFryingRecipe = null;

                OnTurnStateChanged?.Invoke(false);
            }
        } else {
            currentFryingRecipe = FindFryingRecipe(player.GetChickenTool()?.GetChickenToolSO());

            if (currentFryingRecipe != null) {
                player.GetChickenTool().SetParent(this);
                ClearFryingTimer();

                fryingState = currentFryingRecipe.state;

                OnTurnStateChanged?.Invoke(true);
            }
        }
    }

    private void Update() {
        if (HasChickenTool() && currentFryingRecipe != null) {
            SetFryingTimer(fryingTimer + Time.deltaTime, currentFryingRecipe);

            if (fryingTimer > currentFryingRecipe.fryingTimerMax) {
                ClearFryingTimer();
                NextFryingState();
            }
        }
    }

    private void NextFryingState() {
        GetChickenTool().DestroySelf();
        ChickenTool.Spawn(currentFryingRecipe.fried, this);
        currentFryingRecipe = FindFryingRecipe(GetChickenTool().GetChickenToolSO());

        OnTurnStateChanged?.Invoke(currentFryingRecipe != null);

        if (currentFryingRecipe != null) {
            fryingState = currentFryingRecipe.state;
        } else {
            // If currentFryingRecipe is null, it means that only burned meat can be on the counter
            fryingState = FryingState.Burned;
        }
    }

    private bool HasFryingRecipe(ChickenToolSO origin) {
        return FindFryingRecipe(origin) != null;
    }

    private FryingRecipeSO FindFryingRecipe(ChickenToolSO origin) {
        foreach (FryingRecipeSO recipe in fryingRecipes) {
            if (recipe.origin == origin) return recipe;
        }
        return null;
    }

    private void SetFryingTimer(float fryingTimer, FryingRecipeSO recipe) {
        this.fryingTimer = fryingTimer;
        OnProgressChanged?.Invoke(fryingTimer / recipe.fryingTimerMax);
    }

    private void ClearFryingTimer() {
        fryingTimer = 0.0f;
        OnProgressChanged?.Invoke(fryingTimer);
    }

    public bool IsFried() {
        return fryingState == FryingState.Fried;
    }
}